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
            //MarkupNode markup = new MarkupNode();
            //markup.Text = "div";
            //tempCanvas.Children.Add(markup);
            //Canvas.SetLeft(markup,200);

            //AttributeNode attrib = new AttributeNode();
            //attrib.Text = "id empty";
            //tempCanvas.Children.Add(attrib);
            //Canvas.SetLeft(attrib, 200);
            //Canvas.SetTop(attrib, 200);

            //TextNode text = new TextNode();
            //text.Text = "Lorem ipsum dolor sit amet";
            //tempCanvas.Children.Add(text);
            //Canvas.SetLeft(text, 200);
            //Canvas.SetTop(text, 400);


            //Connection markup2attrib = new Connection(markup,attrib);
            //tempCanvas.Children.Add(markup2attrib);

            //Connection markup2text = new Connection(markup, text);
            //tempCanvas.Children.Add(markup2text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("Text " + text.InputPoint());
            //System.Diagnostics.Debug.WriteLine("Attribute " + attrib.OutputPoint());
        }
    }
}
