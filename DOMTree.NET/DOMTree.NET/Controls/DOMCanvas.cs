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
using System.Windows.Shapes;
using System.Windows.Media;

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

        Dictionary<int, List<VisualNode>> Nodes;

        public void Prepare(VisualNode node, ref int highest)
        {
            if (node.Level > highest)
                highest = node.Level;

            if (!Nodes.Keys.Contains(node.Level))
                Nodes[node.Level] = new List<VisualNode>();

            Nodes[node.Level].Add(node);

            foreach(VisualNode childNode in node.Nodes)
            {
                Prepare(childNode, ref highest);
            }
        }

        public void SetupPositions(int Level)
        {
            var nodes = Nodes[Level];

            for (int i = 0; i < nodes.Count; i++)
            {
                if (Level == MaxLevel)
                {
                    var MaxHeight = Nodes[Level].Aggregate((h1, h2) => h1.Height > h2.Height ? h1 : h2).Height;
                    DOMCanvas.SetBottom(nodes[i], 10.0 + (MaxHeight - nodes[i].Height));
                    DOMCanvas.SetLeft(nodes[i], i == 0 ? 0 : DOMCanvas.GetLeft(nodes[i - 1]) + nodes[i - 1].Width + 10.0);
                }
                else
                {
                    nodes = nodes.OrderByDescending(x => x.Nodes.Count).ToList();
                    var MaxHeight = Nodes[Level].Aggregate((h1, h2) => h1.Height > h2.Height ? h1 : h2).Height;

                    if (nodes[i].Nodes.Count > 0)
                    {
                        DOMCanvas.SetBottom(nodes[i], DOMCanvas.GetBottom(Nodes[Level + 1].Aggregate((h1, h2) => h1.Height > h2.Height ? h1 : h2)) + Nodes[Level + 1].Aggregate((h1, h2) => h1.Height > h2.Height ? h1 : h2).Height + 10.0 + (MaxHeight - nodes[i].Height));
                    }
                    else
                    {
                        DOMCanvas.SetBottom(nodes[i], DOMCanvas.GetBottom(Nodes[Level + 1].Aggregate((h1, h2) => h1.Height > h2.Height ? h1 : h2)) + Nodes[Level + 1].Aggregate((h1, h2) => h1.Height > h2.Height ? h1 : h2).Height + 10.0 + (MaxHeight - nodes[i].Height));
                    }

                    if (nodes[i].Nodes.Count > 0)
                    {
                        DOMCanvas.SetLeft(nodes[i], nodes[i].Nodes.Sum(x => DOMCanvas.GetLeft((VisualNode)x)) / nodes[i].Nodes.Count + Math.Abs(nodes[i].Width - ((VisualNode)nodes[i].Nodes[nodes[i].Nodes.Count / 2]).Width) / 2);
                    }
                    else
                    {
                        DOMCanvas.SetLeft(nodes[i], i == 0 ? 0 : DOMCanvas.GetLeft(nodes[i - 1]) + nodes[i - 1].Width + 10.0);
                    }
                }
            }
            
            if (Level > 0)
            {
                Level--;
                SetupPositions(Level);
            }
        }

        int MaxLevel = 0;

        private void MakeConnections(VisualNode parentNode)
        {
            parentNode.Nodes = parentNode.Nodes.OrderBy(x => GetLeft((VisualNode)x)).ToList();
            if (parentNode.Nodes.Count > 0)
            {
                if (parentNode.Nodes.Count == 1)
                {
                    Line line = new Line();
                    line.Visibility = System.Windows.Visibility.Visible;
                    line.StrokeThickness = 4;
                    line.Stroke = System.Windows.Media.Brushes.White;

                    line.X1 = line.X2 = parentNode.OutputPoint().X;
                    line.Y1 = parentNode.OutputPoint().Y;
                    line.Y2 = ((VisualNode)parentNode.Nodes[0]).InputPoint().Y;

                    this.Children.Add(line);
                    SetBottom(line, parentNode.OutputPoint().Y - (Math.Abs(line.Y2 - line.Y1)));

                    MakeConnections((VisualNode)parentNode.Nodes[0]);
                }
                else
                {

                    Line common = new Line();
                    bool commonDrawn = false;
                    foreach (VisualNode node in parentNode.Nodes) //TODO: Draw 1 common line and 1 line for parent and 1 for each node
                    {
                        if (parentNode.Text == "fieldset")
                        {
                           
                            System.Diagnostics.Debug.WriteLine(node.Text + " " + GetLeft(node));
                        }
                        if (!commonDrawn)
                        {
                            common.Visibility = Visibility.Visible;
                            common.StrokeThickness = 4;
                            common.Stroke = Brushes.White;

                            common.Y1 = common.Y2 = (parentNode.OutputPoint().Y + node.InputPoint().Y) / 2;
                            common.X1 = ((VisualNode)parentNode.Nodes[0]).InputPoint().X;
                            common.X2 = ((VisualNode)parentNode.Nodes[parentNode.Nodes.Count - 1]).InputPoint().X;

                            if (parentNode.Text == "fieldset")
                            {
                                System.Diagnostics.Debug.WriteLine("Common X1X2 " + common.X1 + ":" + common.X2);
                                
                            }

                            this.Children.Add(common);
                            SetZIndex(common, -1);
                            SetBottom(common, parentNode.OutputPoint().Y - ((parentNode.OutputPoint().Y - node.InputPoint().Y) / 2));

                            commonDrawn = true;
                        }
                        Line parentLine = new Line();
                        parentLine.Visibility = System.Windows.Visibility.Visible;
                        parentLine.StrokeThickness = 4;
                        parentLine.Stroke = System.Windows.Media.Brushes.White;

                        parentLine.X1 = parentLine.X2 = parentNode.OutputPoint().X;
                        parentLine.Y1 = parentNode.OutputPoint().Y;
                        parentLine.Y2 = common.Y1;

                        this.Children.Add(parentLine);
                        SetZIndex(parentLine, -1);
                        SetBottom(parentLine, parentNode.OutputPoint().Y - (Math.Abs(parentLine.Y2 - parentLine.Y1)));

                        Line childLine = new Line();
                        childLine.Visibility = System.Windows.Visibility.Visible;
                        childLine.StrokeThickness = 4;
                        childLine.Stroke = System.Windows.Media.Brushes.White;

                        childLine.X1 = childLine.X2 = node.InputPoint().X;
                        childLine.Y1 = node.InputPoint().Y;
                        childLine.Y2 = common.Y2;
                        this.Children.Add(childLine);
                        SetZIndex(childLine, -1);
                        SetBottom(childLine, node.InputPoint().Y);

                        MakeConnections(node);
                    }
                }
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            AddChildren(RootNode);
            Nodes = new Dictionary<int, List<VisualNode>>();

            Prepare(RootNode, ref MaxLevel);
            SetupPositions(MaxLevel);
            MakeConnections(RootNode);

            (sender as DispatcherTimer).Stop();
        }
    }
}