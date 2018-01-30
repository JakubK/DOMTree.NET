using DOMTree.NET.Controls;
using DOMTree.NET.Core.Interfaces;
using DOMTree.NET.Core.ViewModels;
using DOMTree.NET.Services;
using MvvmCross.Platform;
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

namespace DOMTree.NET.Views.Design
{
    /// <summary>
    /// Logika interakcji dla klasy DesignView.xaml
    /// </summary>
    [MvxRegion("PageContent")]
    public partial class DOMView : MvxWpfPage
    {
        public new DOMViewModel ViewModel
        {
            get { return (DOMViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }
        
        public DOMView()
        {
            InitializeComponent();
            Loaded += (x, y) => Keyboard.Focus(canvas);
        }

        private void DOMCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.Key == Key.W)
            {
                for (int i = 0; i < canvas.Children.Count; i++)
                {
                    if (double.IsNaN(DOMCanvas.GetTop(canvas.Children[i])))
                    {
                        DOMCanvas.SetTop(canvas.Children[i], 1.0);
                    }
                    DOMCanvas.SetTop(canvas.Children[i], DOMCanvas.GetTop(canvas.Children[i]) + 2);
                }
            }
            if (e.Key == Key.A)
            {
                for (int i = 0; i < canvas.Children.Count; i++)
                {
                    if (double.IsNaN(DOMCanvas.GetLeft(canvas.Children[i])))
                    {
                        DOMCanvas.SetLeft(canvas.Children[i], 1.0);
                    }
                    DOMCanvas.SetLeft(canvas.Children[i], DOMCanvas.GetLeft(canvas.Children[i]) + 2);
                }
            }


            if (e.Key == Key.S)
            {
                for (int i = 0; i < canvas.Children.Count; i++)
                {
                    if (double.IsNaN(DOMCanvas.GetTop(canvas.Children[i])))
                    {
                        DOMCanvas.SetTop(canvas.Children[i], 1.0);
                    }
                    DOMCanvas.SetTop(canvas.Children[i], DOMCanvas.GetTop(canvas.Children[i]) - 2);
                }
            }

            if (e.Key == Key.D)
            {
                for (int i = 0; i < canvas.Children.Count; i++)
                {
                    if (double.IsNaN(DOMCanvas.GetLeft(canvas.Children[i])))
                    {
                        DOMCanvas.SetLeft(canvas.Children[i], 1.0);
                    }
                    DOMCanvas.SetLeft(canvas.Children[i], DOMCanvas.GetLeft(canvas.Children[i]) - 2);
                }
            }
        }
    }
}
