using DOMTree.NET.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DOMTree.NET.Core.Models.DOM;
using DOMTree.NET.Controls;
using System.Collections.ObjectModel;

namespace DOMTree.NET.Services
{
    public class NodePackerService : INodePackerService
    {
        public IVisualNode VisualNode { get; set; }

        public NodePackerService()
        {
        }

        public void Pack(INestable nestable, IVisualNode parent = null)
        {
            if (nestable is Node)
            {
                Node myNode = (Node)nestable;
                

                MarkupNode markupNode = new MarkupNode(myNode.Name);

                
                if(myNode.Attributes.Count > 0)
                {
                    string AttribText = "";
                    foreach (var attribute in myNode.Attributes)
                    {
                        AttribText += attribute.Key + " : " + attribute.Value;
                    }
                    AttributeNode attribNode = new AttributeNode();
                    attribNode.Attributes = myNode.Attributes;
                    attribNode.Text = AttribText;

                    markupNode.Attribute = attribNode;
                }

                if (parent == null)
                {
                    //System.Diagnostics.Debug.WriteLine(markupNode.Text);
                    VisualNode = markupNode;
                }
                else
                {
                    parent.Nodes.Add(markupNode);
                }

                if(myNode.Children.Count > 0)
                {
                    foreach(var child in myNode.Children)
                    {
                        Pack(child, markupNode);
                    }
                }
            }
           else //Text
            {
                TextContent content = (TextContent)nestable;
                //System.Diagnostics.Debug.WriteLine("TextPacking " + content.Text);
                TextNode textNode = new TextNode(content.Text);
                if (parent == null)
                {
                    VisualNode = textNode;
                }
                else
                {
                    parent.Nodes.Add(textNode);
                }
            }
        }

    }
}
