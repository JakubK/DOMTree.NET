﻿using DOMTree.NET.Core.Interfaces;
using DOMTree.NET.Core.Models;
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

        //public ObservableCollection<Document> ListItems { get; set; }

        public int CurrentDocumentID { get; set; }

        #endregion
        #region Constructors
        public MainViewModel(IDocumentService _documentService) : this()
        {
            this.documentService = _documentService;
        }

        public MainViewModel()
        {
            //ListItems = new ObservableCollection<Document>();
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
            documentService.UnLoad(doc);
            documentService.Load(doc.Uri);
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
            Document data = documentService.Load(documentService.SelectUri());

            if (data == null)
                return;

            ShowContentCommand.Execute(data.ID);
        }

        public void ShowDesign()
        {
            ShowViewModel<DesignViewModel>();
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
