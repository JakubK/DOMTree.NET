using DOMTree.NET.Core.Interfaces;

namespace DOMTree.NET.Core.Models.DOM
{
    public class TextContent : INestable
    {
        public string Text;

        public Node Parent;

        public TextContent(string text,Node parent) : this(text)
        {
            this.Parent = parent;
        }

        public TextContent(string text)
        {
            this.Text = text;
        }

        public TextContent()
        {

        }
    }
}