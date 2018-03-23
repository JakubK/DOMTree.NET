using DOMTree.NET.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DOMTree.NET.Controls
{
    public class VisualNode : Control,IVisualNode
    {
        public Point InputPoint()
        {
            double left = double.IsNaN(DOMCanvas.GetLeft(this)) ? 0 : DOMCanvas.GetLeft(this);
            double top = double.IsNaN(DOMCanvas.GetTop(this)) ? 0 : DOMCanvas.GetTop(this);

            return new Point(left + (this.Width / 2), top);
        }
        public Point OutputPoint()
        {
            double left = double.IsNaN(DOMCanvas.GetLeft(this)) ? 0 : DOMCanvas.GetLeft(this);
            double top = double.IsNaN(DOMCanvas.GetTop(this)) ? 0 : DOMCanvas.GetTop(this);

            return new Point(left + (this.Width / 2), top + this.Height);
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(VisualNode));
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public VisualNode()
        {
            Nodes = new List<IVisualNode>();
        }

        public List<IVisualNode> Nodes { get; set; }

        private int level = -1;
        public int Level
        {
            get
            {
                if (level < 0)
                {
                    level = GetLevel();
                }

                return level;
            }
        }

        public VisualNode ParentNode;

        private int GetLevel()
        {
            int Lvl = 0;
            if(ParentNode != null)
            {
                ClimbToNext(ref Lvl, ParentNode);
            }
            return Lvl;
        }

        private int ClimbToNext(ref int Level,VisualNode parent)
        {
            Level++;
            if(parent.ParentNode != null)
            {
                ClimbToNext(ref Level, parent.ParentNode);
            }

            return Level;
        }
    }
}
