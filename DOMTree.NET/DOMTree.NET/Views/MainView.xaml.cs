using DOMTree.NET.Core.ViewModels;
using MvvmCross.Wpf.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMTree.NET.Views
{
    public partial class MainView : MvxWpfView
    {
        public new MainViewModel ViewModel
        {
            get { return (MainViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        public MainView()
        {
            InitializeComponent();
        }

        private void MainView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            ViewModel.LoadViewModel();
        }
    }
}