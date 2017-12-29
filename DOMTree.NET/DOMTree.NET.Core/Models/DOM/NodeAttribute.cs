
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