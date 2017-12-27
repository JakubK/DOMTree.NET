using DOMTree.NET.Core.Models.DOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMTree.NET.Core.Interfaces
{
    public interface IHtmlReaderService
    {
        Node Read(string Code);
    }
}
