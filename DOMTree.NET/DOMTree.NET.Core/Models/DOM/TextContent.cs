using DOMTree.NET.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMTree.NET.Core.Models.DOM
{
    public class TextContent : INestable
    {
        public string Text;

        public Node Parent;

        public TextContent(string text,Node parent) : this(text)
        {
            this.Parent = parent;
        }

        public TextContent(string text)
        {
            this.Text = text;
        }

        public TextContent()
        {

        }
    }
}