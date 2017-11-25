using DOMTree.NET.Core.Interfaces;
using DOMTree.NET.Core.Services;
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
        private readonly IViewReportService<Type> viewReportService;

        public MainViewModel(IViewReportService<Type> reportService)
        {
            this.viewReportService = reportService;
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
            if(!viewReportService.IsLoaded(typeof(DesignViewModel)))
            {
                ShowViewModel<DesignViewModel>();
                viewReportService.AddView(typeof(DesignViewModel));
            }
            viewReportService.RemoveView(typeof(CodeViewModel));
        }

        public void ShowCode()
        {
            if (!viewReportService.IsLoaded(typeof(CodeViewModel)))
            {
                ShowViewModel<CodeViewModel>();
                viewReportService.AddView(typeof(CodeViewModel));
            }
            viewReportService.RemoveView(typeof(DesignViewModel));

        }
    }
}
