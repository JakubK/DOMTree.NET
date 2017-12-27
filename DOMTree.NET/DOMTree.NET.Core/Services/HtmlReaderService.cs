using DOMTree.NET.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DOMTree.NET.Core.Models.DOM;
using MvvmCross.Platform;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;

namespace DOMTree.NET.Core.Services
{
    public class HtmlReaderService : IHtmlReaderService
    {
        private int DocumentLength;

        public Node Read(string Code)
        {
            Node result = new Node();
            result.Name = "document";

            char Character = '\0';
            int ReaderIndex = -1;

            DocumentLength = Code.Length;

            while (true) //skip first line
            {
                ReaderIndex++;
                if (ReaderIndex > DocumentLength - 1)
                    return null;
                Character = Code[ReaderIndex];
                if (Character == '\n')
                    break;
            }
            ReadChildNodes(result, ref Character, ref ReaderIndex, Code, DocumentLength - 1);
            return result;
        }

        public void ReadChildNodes(Node parent, ref char Character, ref int index, string Code, int LastIndex,bool RegularStructure = true) //if type RegularStructure to false if you are dealing with <style> or <script>
        {
            string Text = "";
            if (RegularStructure)
            {
                while (true) //until end of doc
                {
                    index++;
                    if (index > LastIndex)
                        return;
                    Character = Code[index];

                    Node node;
                    string NodeName = "";

                    if (Character != '<')
                    {
                        Text += Character;
                        if ((index + 1) > LastIndex && Text.Length > 0)
                        {
                            Text = Text.Replace("\r", "");
                            Text = Text.Replace("\n", "");
                            Text = Text.Replace("\t", "");
                            Text = Text.Trim();

                            if (!string.IsNullOrWhiteSpace(Text) && !string.IsNullOrEmpty(Text))
                            {

                                parent.Children.Add(new TextContent(Text, parent));
                                Text = "";
                            }
                        }
                    }
                    else //Found Node
                    {
                        Text = Text.Replace("\r", "");
                        Text = Text.Replace("\n", "");
                        Text = Text.Replace("\t", "");
                        Text = Text.Trim();

                        if (!string.IsNullOrWhiteSpace(Text) && !string.IsNullOrEmpty(Text))
                        {
                            parent.Children.Add(new TextContent(Text, parent));
                            Text = "";
                        }

                        node = new Node();
                        while (true) //Read until you get ending of declaration of this Node (> or />)
                        {
                            index++;
                            if (index > LastIndex)
                                return;
                            Character = Code[index];

                            if (char.IsLetterOrDigit(Character))
                            {
                                NodeName += Character;
                            }
                            else //you found special character such as ' ' or '/' or '>'
                            {
                                if (NodeName.Length > 0) //so you have found Node's name too
                                {
                                    node.Parent = parent;
                                    parent.Children.Add(node);
                                    node.Name = NodeName;
                                }
                                break;
                            }
                        }

                        string endingBracket = "</" + node.Name + ">";

                        //here we are going to examine that special character

                        if (Character == ' ') //Seems like your Node have some attributes (we are still before closing /> or >)
                        {
                            LoadAttribute(node, ref index, ref Character, LastIndex, Code);
                            index++;
                            if (index > LastIndex)
                                return;
                            Character = Code[index];
                        }
                        //now that you have a Node name, we should skip everything until it will make </nodename>
                        if (Character == '>')
                        {
                            int start = index;
                            while (true) //read until </end of the node>
                            {
                                index++;
                                if (index > LastIndex)
                                    return;
                                Character = Code[index];

                                if (index + endingBracket.Length - 1 > LastIndex)
                                    return;

                                if (Code.Substring(index, endingBracket.Length) == endingBracket) // |<|/somenode>
                                {

                                    int lastOfNodeContent = index;
                                    string newCode = Code.Substring(start, lastOfNodeContent - start);
                                    int newIndex = 0;

                                    bool Regular = true;

                                    if (node.Name == "script" || node.Name == "style")
                                        Regular = false;

                                    ReadChildNodes(node, ref Character, ref newIndex, newCode, newCode.Length - 1, Regular); // Recursion

                                    index += endingBracket.Length - 1;
                                    if (index > LastIndex)
                                        return;
                                    Character = Code[index]; //skip that ending
                                    break;
                                }
                            }
                        }

                        //Here you found out that this Node cannot have children
                        else if (Character == '/')
                        {
                            index += 2;
                            if (index > LastIndex)
                                return;
                            Character = Code[index];
                        }
                    }
                }
            }
            else //<style> or <script>
            {
                parent.Children.Add(new TextContent(Code));
            }
        }

        public void LoadAttribute(Node node, ref int index, ref char Character, int LastIndex, string Code)
        {
            NodeAttribute attrib = new NodeAttribute();
            while (true)
            {
                index++;
                if (index > LastIndex)
                    return;
                Character = Code[index];

                if (Character == '=')
                {
                    break;
                }
                else
                {
                    attrib.Key += Character;
                }
            }

            index++;

            if (index > LastIndex)
                return;
            Character = Code[index];

            if ((int)Character == 34)
            {
                while (true)
                {
                    index++;
                    if (index > LastIndex)
                        return;

                    Character = Code[index];

                    if ((int)Character == 34)
                    {
                        attrib.Parent = node;
                        node.Attributes.Add(attrib);
                        break;
                    }
                    else
                    {
                        attrib.Value += Character;
                    }
                }
            }
            if (Code[index + 1] == ' ')
            {
                index++;
                if (index > LastIndex)
                    return;
                Character = Code[index];

                LoadAttribute(node, ref index, ref Character, LastIndex, Code);
            }
        }
    }
}
