using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMTree.NET.Core.Models
{
    public class Document
    {
        public string Uri { get; set; }
        public string Code { get; set; }
        public string FileName { get; set; }

        public int ID { get; set; }

        public Document()
        {

        }
    }
}
