﻿using MvvmCross.Core.ViewModels;
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

        private string code;
        public string Code
        {
            get { return code; }
            set { code = value; }
        }
        
    }
}
