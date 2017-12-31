using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DOMTree.NET.Controls
{
    public class VisualNode : Control
    {
        public Point InputPoint()
        {
            double left = double.IsNaN(Canvas.GetLeft(this)) ? 0 : Canvas.GetLeft(this);
            double top = double.IsNaN(Canvas.GetTop(this)) ? 0 : Canvas.GetTop(this);

            return new Point(left + (this.ActualWidth/2), top);
        }
        public Point OutputPoint()
        {
            double left = double.IsNaN(Canvas.GetLeft(this)) ? 0 : Canvas.GetLeft(this);
            double top = double.IsNaN(Canvas.GetTop(this)) ? 0 : Canvas.GetTop(this);

            return new Point(left + (this.ActualWidth / 2), top + this.ActualHeight);
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(VisualNode));
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

    }
}
