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
    }
}
