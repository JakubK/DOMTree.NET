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
        private IDocumentService documentService;

        public ObservableCollection<Document> ListItems { get; set; }

        public int CurrentDocumentID { get; set; }

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
            ListItems = new ObservableCollection<Document>();
            CurrentDocumentID = -1;
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
            get { return new MvxCommand<int>(ShowContent); }
        }
        public ICommand NewFileCommand
        {
            get { return new MvxCommand(NewFile); }
        }
        #endregion
        #region Methods

        private void NewFile()
        {
            ListItems.Add(documentService.CreateNew());
            ShowContentCommand.Execute(ListItems[ListItems.Count-1].ID);
            //throw new NotImplementedException();
        }

        public void LoadViewModel()
        {
            ShowViewModel<CodeViewModel>();
            viewReportService.AddView(typeof(CodeViewModel));
        }

        private void ShowContent(int ID)
        {
            if (CurrentDocumentID == ID)
                return;

            CurrentDocumentID = ID;

            ShowViewModel<CodeViewModel>(new Dictionary<string, string>()
            {
                {"DocId",ID.ToString() }
            });
        }

        public void OpenFile()
        {
            Document data = documentService.Load(documentService.SelectUri());

            if (data == null)
                return;

            if (!ListItems.Any(x => x.Uri == data.Uri))
            {
                ListItems.Add(data);
            }
            else
            {
                ListItems.First(x => x.Uri == data.Uri).Code = data.Code;
            }

            ShowContentCommand.Execute(data.ID);
            
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
            //if (!viewReportService.IsLoaded(typeof(CodeViewModel)))
            //{
            //    if (DocumentData.Code != null)
            //    {
            //        ShowViewModel<CodeViewModel>(new Dictionary<string, string>()
            //        {
            //            {"Code",DocumentData.Code }
            //        });
            //    }
            //    else
            //    {
            //        ShowViewModel<CodeViewModel>();
            //    }
            //    viewReportService.AddView(typeof(CodeViewModel));
            //}
            //viewReportService.RemoveView(typeof(DesignViewModel));
        }
        #endregion
    }
}
