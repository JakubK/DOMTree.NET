using DOMTree.NET.Core.Models.DOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DOMTree.NET.Controls
{
    public class TextNode : VisualNode
    {
        static TextNode()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextNode), new FrameworkPropertyMetadata(typeof(TextNode)));          
        }

        public TextNode()
        {

        }

        public TextNode(string text)
        {
            this.Text = text;
        }

        public TextContent TextContent { get; set; }
    }
}
