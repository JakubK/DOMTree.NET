using DOMTree.NET.Core.Interfaces;
using DOMTree.NET.Core.Models.DOM;
using MvvmCross.Core.ViewModels;
using System.Collections.ObjectModel;
using System.Linq;

namespace DOMTree.NET.Core.ViewModels
{
    public class DOMViewModel : MvxViewModel
    {
        private IHtmlReaderService HtmlReader;
        private IDocumentService DocumentService;

        private INodePackerService nodePacker;
        public INodePackerService NodePacker
        {
            get { return nodePacker; }
            set { nodePacker = value; }
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

        private Node mainNode;
        public Node MainNode
        {
            get { return mainNode; }
            set { mainNode = value; }
        }

        double canvasWidth;
        public double CanvasWidth
        {
            get
            {
                return canvasWidth;
            }
            set
            {
                canvasWidth = value;
            }
        }

        double canvasHeight;
        public double CanvasHeight
        {
            get
            {
                return canvasHeight;
            }
            set
            {
                canvasHeight = value;
            }
        }

        protected override void InitFromBundle(IMvxBundle parameters)
        {
            if (parameters.Data.ContainsKey("DocId")) //Change the current document's ID only if it has changed
            {
                ID = int.Parse(parameters.Data["DocId"]);
                Code = DocumentService.Documents.First(x => x.ID == ID).Code;
                MainNode = HtmlReader.Read(Code);
                NodePacker.Pack(MainNode);
            }
            base.InitFromBundle(parameters);
        }

        public DOMViewModel(IHtmlReaderService reader,IDocumentService documentService,INodePackerService nodePacker)
        {
            this.HtmlReader = reader;
            this.DocumentService = documentService;
            this.nodePacker = nodePacker;

            CanvasWidth = 1000;
            CanvasHeight = 750;
        }
    }
}
