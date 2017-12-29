using DOMTree.NET.Core.Interfaces;
using DOMTree.NET.Core.Models;
using MvvmCross.Core.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace DOMTree.NET.Core.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        #region Fields

        private IDocumentService _documentService;
        public IDocumentService documentService
        {
            get { return _documentService; }
            set
            {
                _documentService = value;
                RaisePropertyChanged(() => documentService);
            }
        }

        public int CurrentDocumentID { get; set; }

        #endregion
        #region Constructors
        public MainViewModel(IDocumentService _documentService) : this()
        {
            this.documentService = _documentService;
        }

        public MainViewModel()
        {
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
        public ICommand SaveFileCommand
        {
            get { return new MvxCommand(SaveFile); }
        }
        public ICommand SaveFileAsCommand
        {
            get { return new MvxCommand(SaveFileAs); }
        }
        public ICommand SaveAllCommand
        {
            get { return new MvxCommand(SaveAll); }
        }

        #endregion
        #region Methods
        private void SaveFile()
        {
            documentService.SaveFile(documentService.Documents.First(x => x.ID == CurrentDocumentID).Uri, documentService.Documents.First(x => x.ID == CurrentDocumentID));
        }

        private void SaveFileAs()
        {
            Document doc = documentService.Documents.First(x => x.ID == CurrentDocumentID);
            documentService.SaveFileAs(doc);   
        }

        private void SaveAll()
        {
            documentService.SaveAll();
        }

        private void NewFile()
        {
            documentService.CreateNew();
            ShowContentCommand.Execute(documentService.Documents[documentService.Documents.Count - 1].ID);
        }

        /// <summary>
        /// First method on Start-up
        /// </summary>
        public void LoadViewModel()
        {
            ShowViewModel<CodeViewModel>();
            NewFile();
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
            Document doc = documentService.Load(documentService.SelectUri());

            if (doc == null)
                return;

            ShowContentCommand.Execute(doc.ID);
        }

        public void ShowDesign()
        {
            ShowViewModel<DOMViewModel>(new Dictionary<string, string>()
            {
                {"DocId",CurrentDocumentID.ToString() }
            });
        }

        public void ShowCode()
        {
            ShowViewModel<CodeViewModel>(new Dictionary<string, string>()
            {
                {"DocId",CurrentDocumentID.ToString() }
            });
        }
        #endregion
    }
}
