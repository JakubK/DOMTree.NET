using DOMTree.NET.Core.ViewModels;
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
        }
    }
}
