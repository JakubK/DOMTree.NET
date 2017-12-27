using DOMTree.NET.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMTree.NET.Core.Models.DOM
{
    public class NodeAttribute
    {
        public string Key;
        public string Value;

        public Node Parent;

        public NodeAttribute()
        {

        }

        public NodeAttribute(string key,string value)
        {
            this.Key = key;
            this.Value = value;
        }
    }
}