using DOMTree.NET.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace DOMTree.NET.Controls
{
    public class DOMCanvas : Canvas
    {
        public VisualNode RootNode
        {
            get { return (VisualNode)GetValue(RootNodeProperty); }
            set {
                SetValue(RootNodeProperty, value);
            }
        }
        public static readonly DependencyProperty RootNodeProperty = DependencyProperty.Register("RootNode", typeof(VisualNode), typeof(DOMCanvas));

        private void DrawChildren(VisualNode node)
        {

            //System.Diagnostics.Debug.WriteLine(node.Text);
            //foreach(var child in node.Nodes)
            //{
            //    DrawChildren((VisualNode)child);
            //}

            //this.Children.Add(node);
            //DOMCanvas.SetLeft(node, 50);

            //Here implement your positioning alghoritm
        }

        static DOMCanvas()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DOMCanvas), new FrameworkPropertyMetadata(typeof(DOMCanvas)));
        }

        public DOMCanvas()
        {
            new DispatcherTimer(//It will not wait after the application is idle.
                       TimeSpan.Zero,
                       //It will wait until the application is idle
                       DispatcherPriority.ApplicationIdle,
                       //It will call this when the app is idle
                       dispatcherTimer_Tick,
                       //On the UI thread
                       Application.Current.Dispatcher);
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            DrawChildren(RootNode);
            (sender as DispatcherTimer).Stop();
        }
    }
}
