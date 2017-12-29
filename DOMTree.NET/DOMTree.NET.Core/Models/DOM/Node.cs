using DOMTree.NET.Core.Interfaces;
using System.Collections.Generic;

namespace DOMTree.NET.Core.Models.DOM
{
    public class Node : INestable
    {
        public Node Parent;
        public List<INestable> Children;
        public List<NodeAttribute> Attributes;

        public string Name;

        public Node(string name) : this()
        {
            this.Name = name;
        }

        public Node()
        {
            this.Children = new List<INestable>();
            this.Attributes = new List<NodeAttribute>();
        }

        public Node NodeByIndex(int index)
        {
            if (index >= 0 && index <= Children.Count - 1)
            {
                if (Children[index] is Node)
                {
                    return (Node)Children[index];
                }
                else
                {
                    return null; //Object is not Node ( It's TextContent)
                }
            }
            else
            {
                return null;
            }
        }
    }
}