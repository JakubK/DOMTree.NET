using DOMTree.NET.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Input;

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

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        private void AddChildren(VisualNode node)
        {
            this.Children.Add(node);
            foreach(VisualNode child in node.Nodes)
            {
                AddChildren(child);
            }
        }

        void CalculateMaxWidths(VisualNode parent,ref List<double> maxWidths)
        {
            double d = 0.0;
            foreach(VisualNode child in parent.Nodes)
            {
                d += child.Width;
                CalculateMaxWidths(child, ref maxWidths);
            }

            maxWidths.Add(d);
        }

        public void SetupPositions(VisualNode node)
        {
            if(double.IsNaN(DOMCanvas.GetLeft(node)) && double.IsNaN(DOMCanvas.GetTop(node))) //Check if it's possition is not assigned
            {
                if(node.ParentNode == null)
                {
                    //^If it has no parent, then it's the RootNode
                    DOMCanvas.SetTop(node, 30.0);
                    DOMCanvas.SetLeft(node,this.Width / 2 - node.Width / 2);
                }
                else
                {
                    List<double> MaxWidths = new List<double>();
                    //Calculate MaxWidths in Lower Levels
                    CalculateMaxWidths(node.ParentNode,ref MaxWidths);
                    double max = MaxWidths.Max();

                    if(node.ParentNode.Nodes.Count % 2 == 0) //ChildCount is Even
                    {
                        double center = DOMCanvas.GetLeft(node.ParentNode) + node.ParentNode.Width / 2;
                        DOMCanvas.SetTop((VisualNode)node.ParentNode.Nodes[node.ParentNode.Nodes.Count / 2 - 1], DOMCanvas.GetTop(node.ParentNode) + node.ParentNode.Height + 10.0);
                        DOMCanvas.SetLeft((VisualNode)node.ParentNode.Nodes[node.ParentNode.Nodes.Count / 2 - 1], center - max/2 - ((VisualNode)node.ParentNode.Nodes[node.ParentNode.Nodes.Count / 2 - 1]).Width);

                        DOMCanvas.SetTop((VisualNode)node.ParentNode.Nodes[node.ParentNode.Nodes.Count / 2], DOMCanvas.GetTop(node.ParentNode) + node.ParentNode.Height + 10.0);
                        DOMCanvas.SetLeft((VisualNode)node.ParentNode.Nodes[node.ParentNode.Nodes.Count / 2], center + max/2 );

                        //To the left
                        for (int i = node.ParentNode.Nodes.Count / 2 - 2; i > -1; i--)
                        {
                            DOMCanvas.SetTop((VisualNode)node.ParentNode.Nodes[i], DOMCanvas.GetTop(node.ParentNode) + node.ParentNode.Height + 10.0);
                            DOMCanvas.SetLeft((VisualNode)node.ParentNode.Nodes[i], DOMCanvas.GetLeft((VisualNode)node.ParentNode.Nodes[i + 1]) - max/2  - ((VisualNode)node.ParentNode.Nodes[i]).Width);
                        }

                        //To the Right
                        for (int i = node.ParentNode.Nodes.Count / 2 + 1; i < node.ParentNode.Nodes.Count; i++)
                        {
                            DOMCanvas.SetTop((VisualNode)node.ParentNode.Nodes[i], DOMCanvas.GetTop(node.ParentNode) + node.ParentNode.Height + 10.0);
                            DOMCanvas.SetLeft((VisualNode)node.ParentNode.Nodes[i], DOMCanvas.GetLeft((VisualNode)node.ParentNode.Nodes[i - 1]) + max/2);
                        }
                    }
                    else //ChildCount is Odd
                    {
                        int center = node.ParentNode.Nodes.Count / 2;
                        DOMCanvas.SetTop((VisualNode)node.ParentNode.Nodes[center], DOMCanvas.GetTop(node.ParentNode) + node.ParentNode.Height + 10.0);
                        if (((VisualNode)node.ParentNode.Nodes[center]).Width > node.ParentNode.Width)
                        {
                            DOMCanvas.SetLeft((VisualNode)node.ParentNode.Nodes[center], DOMCanvas.GetLeft(node.ParentNode) - (((VisualNode)node.ParentNode.Nodes[center]).Width - node.ParentNode.Width) /2);
                        }
                        else
                        {
                            DOMCanvas.SetLeft((VisualNode)node.ParentNode.Nodes[center], DOMCanvas.GetLeft(node.ParentNode));
                        }
                       

                        //To the Left
                        for (int i = center-1;i > -1;i--)
                        {
                            DOMCanvas.SetTop((VisualNode)node.ParentNode.Nodes[i], DOMCanvas.GetTop(node.ParentNode) + node.ParentNode.Height + 10.0);
                            DOMCanvas.SetLeft((VisualNode)node.ParentNode.Nodes[i], DOMCanvas.GetLeft((VisualNode)node.ParentNode.Nodes[i + 1]) - ((VisualNode)node.ParentNode.Nodes[i]).Width - 10.0);
                        }

                        //To the Right
                        for (int i = center + 1; i < node.ParentNode.Nodes.Count; i++)
                        {
                            DOMCanvas.SetTop((VisualNode)node.ParentNode.Nodes[i], DOMCanvas.GetTop(node.ParentNode) + node.ParentNode.Height + 10.0);
                            DOMCanvas.SetLeft((VisualNode)node.ParentNode.Nodes[i], DOMCanvas.GetLeft((VisualNode)node.ParentNode.Nodes[i - 1]) + ((VisualNode)node.ParentNode.Nodes[i]).Width + 10.0);
                        }
                    }
                    
                    foreach(VisualNode child in node.ParentNode.Nodes)
                    {
                        DOMCanvas.SetTop(child, DOMCanvas.GetTop(node.ParentNode) + node.ParentNode.Height + 10.0);     
                    }
                }
            }
            foreach (VisualNode child in node.Nodes)
            {
                SetupPositions(child);
            }
        }

        void PrintLeft(VisualNode node)
        {
            System.Diagnostics.Debug.WriteLine(DOMCanvas.GetLeft(node));
            foreach(VisualNode child in node.Nodes)
            {
                PrintLeft(child);
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            AddChildren(RootNode);
            SetupPositions(RootNode);
            //PrintLeft(RootNode);


            (sender as DispatcherTimer).Stop();
        }
    }
}