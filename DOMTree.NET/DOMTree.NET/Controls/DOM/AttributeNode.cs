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
    public class AttributeNode : VisualNode
    {
        static AttributeNode()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AttributeNode), new FrameworkPropertyMetadata(typeof(AttributeNode)));
        }

        public AttributeNode()
        {
            Attributes = new List<NodeAttribute>();
        }

        public AttributeNode(string text) : this()
        {
            this.Text = text;
        }

        public List<NodeAttribute> Attributes { get; set; }
    }
}
