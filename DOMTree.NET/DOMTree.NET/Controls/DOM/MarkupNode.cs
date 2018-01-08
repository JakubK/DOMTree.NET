using DOMTree.NET.Core.Models.DOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DOMTree.NET.Controls
{
    public class MarkupNode : VisualNode
    {
        static MarkupNode()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MarkupNode), new FrameworkPropertyMetadata(typeof(MarkupNode)));
        }

        public MarkupNode()
        {

        }

        public MarkupNode(string text)
        {
            this.Text = text;
        }

        public Node Node { get; set; }

    }
}
