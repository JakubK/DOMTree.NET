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
        private ObservableCollection<IVisualNode> visualNodes;

        public ObservableCollection<IVisualNode> VisualNodes
        {
            get { return visualNodes; }
            set { visualNodes = value; }
        }

        public NodePackerService()
        {
            VisualNodes = new ObservableCollection<IVisualNode>();
        }

        /// <summary>
        /// Create VisualNodes for each Node in rootNode, and pack it 
        /// </summary>
        /// <param name="rootNode"></param>
        public void Pack(INestable nestable,IVisualNode parent = null)
        {
            VisualNodes = new ObservableCollection<IVisualNode>();
            if (nestable is Node)
            {
                Node myNode = (Node)nestable;
                //System.Diagnostics.Debug.WriteLine("NodePacking " + myNode.Name);

                MarkupNode markupNode = new MarkupNode(myNode.Name);
                markupNode.Node = myNode;

                if(parent != null)
                {
                    markupNode.ParentNode = (MarkupNode)parent;
                }

                VisualNodes.Add(markupNode);


                if (myNode.Attributes.Count > 0)
                {
                    string AttribText = "";
                    foreach (var attrib in myNode.Attributes)
                    {
                        AttribText += attrib.Key + " : " + attrib.Value;
                        //System.Diagnostics.Debug.WriteLine("AttributePacking " + attrib.Key + " : " + attrib.Value);
                    }
                    AttributeNode attribNode = new AttributeNode();
                    attribNode.Attributes = myNode.Attributes;
                    attribNode.Text = AttribText;

                    attribNode.ParentNode = markupNode;

                    VisualNodes.Add(attribNode);
                }

                if (myNode.Children.Count > 0) // <markup>...</markup>
                {
                    foreach (var child in myNode.Children)
                    {
                        Pack(child,markupNode);
                    }
                }
            }
            else //Text
            {
                TextContent content = (TextContent)nestable;
                //System.Diagnostics.Debug.WriteLine("TextPacking " + content.Text);
                TextNode textNode = new TextNode(content.Text);
                if (parent != null)
                {
                    textNode.ParentNode = (MarkupNode)parent;
                }
                VisualNodes.Add(textNode);
            }
        }
    }
}
