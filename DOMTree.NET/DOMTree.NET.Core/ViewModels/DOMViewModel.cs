using DOMTree.NET.Core.Interfaces;
using DOMTree.NET.Core.Models.DOM;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMTree.NET.Core.ViewModels
{
    public class DOMViewModel : MvxViewModel
    {
        private IHtmlReaderService HtmlReader;
        private IDocumentService DocumentService;

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

        private Node mainNode;
        public Node MainNode
        {
            get { return mainNode; }
            set { mainNode = value; }
        }

        protected override void InitFromBundle(IMvxBundle parameters)
        {
            if (parameters.Data.ContainsKey("DocId")) //Change the current document's ID only if it has changed
            {
                ID = int.Parse(parameters.Data["DocId"]);
                Code = DocumentService.Documents.First(x => x.ID == ID).Code;
                MainNode = HtmlReader.Read(Code);
            }
            base.InitFromBundle(parameters);
        }

        public DOMViewModel(IHtmlReaderService reader,IDocumentService documentService)
        {
            this.HtmlReader = reader;
            this.DocumentService = documentService;
        }
    }
}
