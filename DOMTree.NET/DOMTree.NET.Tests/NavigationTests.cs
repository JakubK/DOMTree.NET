using DOMTree.NET.Core.Interfaces;
using DOMTree.NET.Core.Services;
using DOMTree.NET.Core.ViewModels;
using DOMTree.NET.Tests.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MvvmCross.Core.Platform;
using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Views;
using MvvmCross.Platform;
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
    public class NavigationTests : MvxIoCSupportingTest
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
            Ioc.RegisterSingleton<IViewReportService<Type>>(new ViewReportService());
        }

        [TestInitialize]
        public void TestInit()
        {
            Setup();
        }

        [TestMethod]
        public void TestIsSwitchingToCodeView()
        {
            // Arrange
            var reportService = Mvx.Resolve<IViewReportService<Type>>();

            var viewModel = new MainViewModel(reportService);
            // Act
            viewModel.ShowCodeCommand.Execute(null);
            // Assert
            Assert.AreEqual(1, MockDispatcher.Requests.Count);
            Assert.AreEqual(typeof(CodeViewModel),
                MockDispatcher.Requests[0].ViewModelType);
        }

        [TestMethod]
        public void TestIsSwitchingToDesignView()
        {
            // Arrange
            var reportService = Mvx.Resolve<IViewReportService<Type>>();

            var viewModel = new MainViewModel(reportService);
            // Act
            viewModel.ShowDesignCommand.Execute(null);
            // Assert
            Assert.AreEqual(1, MockDispatcher.Requests.Count);
            Assert.AreEqual(typeof(DesignViewModel),
                MockDispatcher.Requests[0].ViewModelType);
        }
    }
}
