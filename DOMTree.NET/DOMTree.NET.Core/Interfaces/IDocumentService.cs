using DOMTree.NET.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMTree.NET.Core.Interfaces
{
    public interface IDocumentService
    {
        ObservableCollection<Document> Documents { get; set; }
        Document Load(string uri);
        Document CreateNew();
        bool SaveFile(string uri, Document doc, bool overwrite = true);
        bool SaveFileAs(Document doc);
        string SelectUri();
    }
}
