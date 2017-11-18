using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DOMTree.NET.Core.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        public MainViewModel()
        {
        }

        public void LoadViewModel()
        {
            ShowViewModel<CodeViewModel>();
        }

        public ICommand ShowDesignCommand
        {
            get { return new MvxCommand(ShowDesign); }
        }

        public ICommand ShowCodeCommand
        {
            get { return new MvxCommand(ShowCode); }
        }

        public void ShowDesign()
        {
            ShowViewModel<DesignViewModel>();
        }

        public void ShowCode()
        {
            ShowViewModel<CodeViewModel>();
        }
    }
}
