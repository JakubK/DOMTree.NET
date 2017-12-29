using DOMTree.NET.Core.Interfaces;
using DOMTree.NET.Core.Models.DOM;
using DOMTree.NET.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvvmCross.Test.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DOMTree.NET.Tests
{
    [TestClass]
    public class HtmlReaderServiceTests : MvxIoCSupportingTest
    {
        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();

            Ioc.RegisterSingleton<IHtmlReaderService>(new HtmlReaderService());
        }

        [TestInitialize]
        public void TestInit()
        {
            Setup();
        }

        [TestMethod]
        public void Test_Is_Children_Being_Added()
        {
            // Arrange
            Node node = new Node("document");
            // Act
            node.Children.Add(new Node("html"));
            // Assert
            Assert.IsTrue(node.Children.Count == 1);
            Assert.IsTrue(node.NodeByIndex(0).Name == "html");
        }

        [TestMethod]
        public void Test_Loading_Single_Node()
        {
            // Arrange
            string Code;

            var directory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var path = string.Format("{0}\\{1}", directory, "TestData/html.txt");
            Code = File.ReadAllText(path);

            Node resultNode;
            var service = Ioc.Resolve<IHtmlReaderService>();
            // Act
            resultNode = service.Read(Code);
            // Assert

            Assert.IsTrue(resultNode.Name == "document");
            Assert.IsTrue(resultNode.Children.Count == 1);
            Assert.IsTrue(resultNode.NodeByIndex(0).Name == "html");
        }
    }
}
