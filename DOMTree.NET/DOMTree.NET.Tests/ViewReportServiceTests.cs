using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvvmCross.Test.Core;
using DOMTree.NET.Tests.Classes;
using MvvmCross.Core.Views;
using DOMTree.NET.Core.Interfaces;
using MvvmCross.Core.Platform;
using DOMTree.NET.Core.Services;
using MvvmCross.Platform.Core;
using MvvmCross.Platform;
using DOMTree.NET.Core.ViewModels;

namespace DOMTree.NET.Tests
{
    [TestClass]
    public class ViewReportServiceTests : MvxIoCSupportingTest
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
       public void TestIsLoadedMethod()
       {
            // Arrange
            var reportService = Mvx.Resolve<IViewReportService<Type>>();

            var viewModel = new MainViewModel(reportService);
            viewModel.ShowDesignCommand.Execute(null);

            // Act
            bool FirstResult = reportService.IsLoaded(typeof(CodeViewModel));
            viewModel.ShowCodeCommand.Execute(null);
            bool SecondResult = reportService.IsLoaded(typeof(CodeViewModel));

            // Assert
            Assert.AreEqual(FirstResult, false);
            Assert.AreEqual(SecondResult, true);
        }
    }
}
