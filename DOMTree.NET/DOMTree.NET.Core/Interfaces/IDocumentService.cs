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
        ObservableCollection<CodeData> Uris { get; set; }
        CodeData Load(string uri);
        string SelectUri();
    }
}
