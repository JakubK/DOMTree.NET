using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMTree.NET.Core
{
    public class App : MvxApplication
    {

        /// <summary>
        /// Setup IoC registrations.
        /// </summary>
        public App()
        {
            // Whenever Mvx.Resolve is used, a new instance of Calculation is provided.
           // Mvx.RegisterType<IBillCalculator, BillCalculator>();
          //  var calcExample = Mvx.Resolve<IBillCalculator>();

            // Tells the MvvmCross framework that whenever any code requests an IMvxAppStart reference,
            // the framework should return that same appStart instance.
            var appStart = new CustomAppStart();
            Mvx.RegisterSingleton<IMvxAppStart>(appStart);

            // Another option is to utilize a default App Start object with 
            // var appStart = new MvxAppStart<TipViewModel>();
        }
    }
}
