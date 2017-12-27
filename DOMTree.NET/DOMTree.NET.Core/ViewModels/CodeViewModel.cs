using DOMTree.NET.Core.Interfaces;
using DOMTree.NET.Core.Models;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMTree.NET.Core.ViewModels
{
    /// <summary>
    /// Is responsible for rendering current Document
    /// It takes Current DocumentID from MainViewModel
    /// </summary>
    public class CodeViewModel : MvxViewModel
    {
        private IDocumentService DocumentService;

        public CodeViewModel(IDocumentService documentService)
        {
            this.DocumentService = documentService;
        }

        protected override void InitFromBundle(IMvxBundle parameters)
        {
            if (parameters.Data.ContainsKey("DocId")) //Change the current document's ID only if it has changed
            {
                ID = int.Parse(parameters.Data["DocId"]);
                Code = DocumentService.Documents.First(x => x.ID == ID).Code;
            }
            base.InitFromBundle(parameters);
        }

        private int ID { get; set; }

        private string code;
        public string Code
        {
            get { return code; }
            set
            {
                DocumentService.Documents[ID].Code = code = value;
            }
        }        
    }
}
