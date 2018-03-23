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
            //System.Diagnostics.Debug.WriteLine("Level of " + node.Text + " " + node.Level);
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

        public void AlternateSetupPositions(int Level)
        {
            var nodes = Nodes[Level];

            for(int i = 0;i < nodes.Count;i++)
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

            if(Level > 0)
            {
                Level--;
                AlternateSetupPositions(Level);
            }
        }

        int MaxLevel = 0;

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            AddChildren(RootNode);
            //SetupPositions(RootNode);

            Nodes = new Dictionary<int, List<VisualNode>>();

            Prepare(RootNode, ref MaxLevel);
            AlternateSetupPositions(MaxLevel);

            //PrintLeft(RootNode);
            (sender as DispatcherTimer).Stop();
        }
    }
}