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

        public void SetupPositions(VisualNode node)
        {
            if (double.IsNaN(DOMCanvas.GetLeft(node)) && double.IsNaN(DOMCanvas.GetTop(node)))
            {
                if (node.ParentNode == null)
                {
                    //Center the Node Horizontally and Align to Top Vertically [1]
                    DOMCanvas.SetLeft(node, this.Width / 2 - (node.Width / 2));
                    DOMCanvas.SetTop(node, 30);
                }
                else //If Node has parent
                {
                    if (node.ParentNode.Nodes.Count == 1) //If node is the only child
                    {
                        //Center horizontally and put under parent [1]
                        if (node.Width > node.ParentNode.Width)
                        {
                            DOMCanvas.SetLeft(node, DOMCanvas.GetLeft(node.ParentNode) - (node.Width - node.ParentNode.Width) / 2);
                        }
                        else
                        {
                            DOMCanvas.SetLeft(node, DOMCanvas.GetLeft(node.ParentNode));
                        }
                        DOMCanvas.SetTop(node, DOMCanvas.GetTop(node.ParentNode) + node.ParentNode.Height + 30.0);
                    }
                    else //if there is more than 1 child
                    {
                        if (node.ParentNode.Nodes.Count % 2 == 0) //If ChildNumb is Even
                        {
                            double SumWidth = 0.0;
                            double temp = 0.0;
                            foreach (VisualNode child in node.ParentNode.Nodes)
                            {
                                for (int i = 0; i < child.Nodes.Count; i++)
                                {
                                    temp += ((VisualNode)child.Nodes[i]).Width + 10.0;
                                }
                                if (temp > SumWidth)
                                    SumWidth = temp;
                            }


                            double left = DOMCanvas.GetLeft(node.ParentNode) + (node.ParentNode.Width / 2) - (SumWidth / 2);

                            for (int i = 0; i < node.ParentNode.Nodes.Count; i++)
                            {
                                DOMCanvas.SetLeft((VisualNode)node.ParentNode.Nodes[i], left);
                                DOMCanvas.SetTop((VisualNode)node.ParentNode.Nodes[i], DOMCanvas.GetTop(node.ParentNode) + node.ParentNode.Height + 30.0);
                                double distance = DOMCanvas.GetLeft(node.ParentNode) - DOMCanvas.GetLeft((VisualNode)node.ParentNode.Nodes[i]);
                                if ((i + 1) < node.ParentNode.Nodes.Count)
                                    left += distance + node.ParentNode.Width + distance - ((VisualNode)node.ParentNode.Nodes[i + 1]).Width;
                            }
                        }
                        else //Else is Odd
                        {
                            double SumWidth = 0.0;
                            double temp = 0.0;
                            foreach (VisualNode child in node.ParentNode.Nodes)
                            {
                                for (int i = 0; i < child.Nodes.Count; i++)
                                {
                                    temp += ((VisualNode)child.Nodes[i]).Width + 10.0;
                                }
                                if (temp > SumWidth)
                                    SumWidth = temp;
                            }

                            //retrieve center element
                            int middleIndex = node.ParentNode.Nodes.Count / 2;

                            DOMCanvas.SetTop((VisualNode)node.ParentNode.Nodes[middleIndex], DOMCanvas.GetTop(node.ParentNode) + node.ParentNode.Height + 30.0);
                            // DOMCanvas.SetLeft((VisualNode)node.ParentNode.Nodes[middleIndex], DOMCanvas.GetLeft(node.ParentNode)); //FIX Centering
                            if (((VisualNode)node.ParentNode.Nodes[middleIndex]).Width > ((VisualNode)node.ParentNode.Nodes[middleIndex]).ParentNode.Width)
                            {
                                DOMCanvas.SetLeft((VisualNode)node.ParentNode.Nodes[middleIndex], DOMCanvas.GetLeft(node.ParentNode) - (((VisualNode)node.ParentNode.Nodes[middleIndex]).Width - node.ParentNode.Width) / 2); //FIX Centering

                            }
                            else
                            {
                                DOMCanvas.SetLeft((VisualNode)node.ParentNode.Nodes[middleIndex], DOMCanvas.GetLeft(node.ParentNode)); //FIX Centering
                            }

                            //from middle to 0
                            for (int i = middleIndex - 1; i >= 0; i--)
                            {
                                DOMCanvas.SetTop((VisualNode)node.ParentNode.Nodes[i], DOMCanvas.GetTop(node.ParentNode) + node.ParentNode.Height + 30.0);
                                DOMCanvas.SetLeft((VisualNode)node.ParentNode.Nodes[i], DOMCanvas.GetLeft((VisualNode)node.ParentNode.Nodes[i + 1]) - SumWidth / node.ParentNode.Nodes.Count - 30);
                            }
                            //from middle to the limit
                            for (int i = middleIndex + 1; i < node.ParentNode.Nodes.Count; i++)
                            {
                                DOMCanvas.SetTop((VisualNode)node.ParentNode.Nodes[i], DOMCanvas.GetTop(node.ParentNode) + node.ParentNode.Height + 30.0);
                                DOMCanvas.SetLeft((VisualNode)node.ParentNode.Nodes[i], DOMCanvas.GetLeft((VisualNode)node.ParentNode.Nodes[i - 1]) + SumWidth / node.ParentNode.Nodes.Count + 30);
                            }
                        }
                    }
                }
            }

            foreach (VisualNode child in node.Nodes)
            {
                SetupPositions(child);
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            AddChildren(RootNode);
            SetupPositions(RootNode);

            (sender as DispatcherTimer).Stop();
        }
    }
}