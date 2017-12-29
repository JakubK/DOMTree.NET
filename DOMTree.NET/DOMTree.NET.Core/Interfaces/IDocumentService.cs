using DOMTree.NET.Core.Models;
using System.Collections.ObjectModel;

namespace DOMTree.NET.Core.Interfaces
{
    public interface IDocumentService
    {
        ObservableCollection<Document> Documents { get; set; }
        Document Load(string uri);
        bool UnLoad(Document doc);
        Document CreateNew();
        bool SaveFile(string uri, Document doc, bool overwrite = true);
        bool SaveFileAs(Document doc);
        bool SaveAll();
        string SelectUri();
    }
}
