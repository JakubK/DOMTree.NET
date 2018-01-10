using DOMTree.NET.Core.Models.DOM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMTree.NET.Core.Interfaces
{
    public interface INodePackerService
    {
        void Pack(INestable node,IVisualNode parentNode = null);

        IVisualNode VisualNode { get; set; }
    }
}
