using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMTree.NET.Core.ViewModels
{
    public class CodeViewModel : MvxViewModel
    {
        public CodeViewModel()
        {

        }

        protected override void InitFromBundle(IMvxBundle parameters)
        {
            if(parameters.Data.ContainsKey("Code"))
            {
                code = parameters.Data["Code"];
            }
            base.InitFromBundle(parameters);
        }

        private string code;
        public string Code
        {
            get { return code; }
            set { code = value; }
        }        
    }
}
