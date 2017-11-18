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

namespace DOMTree.NET.Views.Code
{
    /// <summary>
    /// Logika interakcji dla klasy CodeView.xaml
    /// </summary>
    [MvxRegion("PageContent")]
    public partial class CodeView : MvxWpfPage
    {
        public new CodeViewModel ViewModel
        {
            get { return (CodeViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        public CodeView()
        {
            InitializeComponent();
        }
    }
}
