using DOMTree.NET.Core.ViewModels;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMTree.NET.Core
{
    /// <summary>
    /// This class is used to customize how the application starts
    /// and which view is loaded on start.
    /// </summary>
    class CustomAppStart : MvxNavigatingObject, IMvxAppStart
    {
        /// <summary>
        /// Hint can take command-line startup parameters, and then pass them to the called view models.
        /// </summary>
        public void Start(object hint = null)
        {
            // ShowViewModel is a core navigation mechanism in MvvmCross.
            // for now, just start the regular MainMenuViewModel view.
            ShowViewModel<MainViewModel>();
        }
    }
}
