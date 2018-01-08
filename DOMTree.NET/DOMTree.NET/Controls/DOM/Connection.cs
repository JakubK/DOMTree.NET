﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace DOMTree.NET.Controls
{
    public class Connection : Control
    {
        public static readonly DependencyProperty X1Property = DependencyProperty.Register("X1", typeof(double), typeof(Connection));

        public double X1
        {
            get { return (double)GetValue(X1Property); }
            set { SetValue(X1Property, value); }
        }

        public static readonly DependencyProperty Y1Property = DependencyProperty.Register("Y1", typeof(double), typeof(Connection));
        public double Y1
        {
            get { return (double)GetValue(Y1Property); }
            set { SetValue(Y1Property, value); }
        }

        public static readonly DependencyProperty X2Property = DependencyProperty.Register("X2", typeof(double), typeof(Connection));
        public double X2
        {
            get { return (double)GetValue(X2Property); }
            set { SetValue(X2Property, value); }
        }

        public static readonly DependencyProperty Y2Property = DependencyProperty.Register("Y2", typeof(double), typeof(Connection));
        public double Y2
        {
            get { return (double)GetValue(Y2Property); }
            set { SetValue(Y2Property, value); }
        }

        public VisualNode ParentNode;
        public VisualNode ChildNode;

        static Connection()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Connection),new FrameworkPropertyMetadata(typeof(Connection)));
        }

        public Connection(VisualNode parent,VisualNode child)
        {
            this.ParentNode = parent;
            this.ChildNode = child;

            ReInitialize();
        }

        public void ReInitialize()
        {
            X1 = ParentNode.OutputPoint().X;
            Y1 = ParentNode.OutputPoint().Y;

            X2 = ChildNode.InputPoint().X;
            Y2 = ChildNode.InputPoint().Y;
        }
    }
}