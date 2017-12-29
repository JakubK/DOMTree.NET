using DOMTree.NET.Core.Models.DOM;

namespace DOMTree.NET.Core.Interfaces
{
    public interface IHtmlReaderService
    {
        Node Read(string Code);
    }
}
