using DOMTree.NET.Core.Interfaces;
using DOMTree.NET.Core.Models;
using DOMTree.NET.Services;
using DOMTree.NET.Tests.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvvmCross.Core.Platform;
using MvvmCross.Core.Views;
using MvvmCross.Platform.Core;
using MvvmCross.Test.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMTree.NET.Tests
{
    [TestClass]
    public class DocumentServiceTests : MvxIoCSupportingTest
    {
        protected MockDispatcher MockDispatcher;
        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();
            MockDispatcher = new MockDispatcher();
            Ioc.RegisterSingleton<IMvxViewDispatcher>(MockDispatcher);
            Ioc.RegisterSingleton<IMvxMainThreadDispatcher>(MockDispatcher);

            // required only when passing parameters
            Ioc.RegisterSingleton<IMvxStringToTypeParser>(new MvxStringToTypeParser());
            Ioc.RegisterSingleton<IDocumentService>(new DocumentService());
        }

        [TestInitialize]
        public void TestInit()
        {
            Setup();
        }

        [TestMethod]
        public void TestIsEmptyDocumentBeingCreated()
        {
            //arrange
            var service = Ioc.Resolve<IDocumentService>();
            //act
            service.CreateNew();
            //assert
            Assert.IsTrue(service.Documents.Count == 1);
        }

        [TestMethod]
        public void TestCanRemoveExistingDocument()
        {
            //arrange
            var service = Ioc.Resolve<IDocumentService>();
            //act
            Document doc = service.CreateNew();
            service.UnLoad(doc);
            //assert
            Assert.IsTrue(service.Documents.Count == 0);
        }
    }
}