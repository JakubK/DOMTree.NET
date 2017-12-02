using DOMTree.NET.Core.Interfaces;
using DOMTree.NET.Core.Models;
using DOMTree.NET.Core.Services;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DOMTree.NET.Core.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        #region Fields
        private readonly IViewReportService<Type> viewReportService;
        private readonly IDocumentService documentService;

        public ObservableCollection<CodeData> ListItems { get; set; }

        public CodeData DocumentData { get; set; }

        #endregion
        #region Constructors
        public MainViewModel(IViewReportService<Type> _reportService,IDocumentService _documentService) : this(_reportService)
        {
            this.documentService = _documentService;
        }

        public MainViewModel(IViewReportService<Type> _reportService) : this()
        {
            this.viewReportService = _reportService;
        }

        public MainViewModel()
        {
            ListItems = new ObservableCollection<CodeData>();
            DocumentData = new CodeData();
        }
        #endregion
        #region Commands
        public ICommand ShowDesignCommand
        {
            get { return new MvxCommand(ShowDesign); }
        }

        public ICommand ShowCodeCommand
        {
            get { return new MvxCommand(ShowCode); }
        }

        public ICommand OpenFileCommand
        {
            get { return new MvxCommand(OpenFile); }
        }

        public ICommand ShowContentCommand
        {
            get { return new MvxCommand<CodeData>(ShowContent); }
        }
        #endregion
        #region Methods
        public void LoadViewModel()
        {
            ShowViewModel<CodeViewModel>();
            viewReportService.AddView(typeof(CodeViewModel));
        }

        private void ShowContent(CodeData Data)
        {
            System.Diagnostics.Debug.WriteLine(Data.Uri);
            DocumentData.Code = Data.Code;

            ShowViewModel<CodeViewModel>(new Dictionary<string, string>()
            {
                {"Code",DocumentData.Code }
            });
        }

        public void OpenFile()
        {
            CodeData data = documentService.Load(documentService.SelectUri());

            if (data != null)
            {
                if (!ListItems.Any(x => x.Uri == data.Uri))
                {
                    ListItems.Add(data);
                }
                else
                {
                    ListItems.First(x => x.Uri == data.Uri).Code = data.Code;
                }
                ShowContentCommand.Execute(data);
            }
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
                if (DocumentData.Code != null)
                {
                    ShowViewModel<CodeViewModel>(new Dictionary<string, string>()
                    {
                        {"Code",DocumentData.Code }
                    });
                }
                else
                {
                    ShowViewModel<CodeViewModel>();
                }
                viewReportService.AddView(typeof(CodeViewModel));
            }
            viewReportService.RemoveView(typeof(DesignViewModel));
        }
        #endregion
    }
}
