using DOMTree.NET.Core.Interfaces;
using DOMTree.NET.Core.Models.DOM;
using HtmlAgilityPack;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace DOMTree.NET.Core.Services
{
    public class HtmlReaderService : IHtmlReaderService
    {
        public Node Read(string Code)
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(Code);

            Node result = new Node(htmlDocument.DocumentNode.Name.Substring(1, htmlDocument.DocumentNode.Name.Length - 1).Trim());

            foreach (var element in htmlDocument.DocumentNode.ChildNodes.Where(x => x.NodeType == HtmlNodeType.Element))
            {
                result.Children.Add(ReadChild(element));
            }

            return result;
        }

        public Node ReadChild(HtmlNode element)
        {
            Node result = new Node(element.Name.Trim());

            foreach (var attrib in element.Attributes)
            {
                result.Attributes.Add(new NodeAttribute(attrib.Name, attrib.Value));
            }

            var textNodes = element.ChildNodes.OfType<HtmlTextNode>();
            foreach (HtmlTextNode node in textNodes)
            {
                if (node.Text.Trim().Length > 0)
                    result.Children.Add(new TextContent(node.Text.Trim()));
            }

            foreach (var xelement in element.ChildNodes.Where(x => x.NodeType == HtmlNodeType.Element))
            {
                result.Children.Add(ReadChild(xelement));
            }

            return result;
        }
    }
}