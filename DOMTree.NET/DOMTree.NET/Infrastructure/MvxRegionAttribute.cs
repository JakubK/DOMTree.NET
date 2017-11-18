﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMTree.NET
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class MvxRegionAttribute
       : Attribute
    {
        public MvxRegionAttribute(string regionName)
        {
            this.Name = regionName;
        }

        public string Name { get; private set; }
    }
}
