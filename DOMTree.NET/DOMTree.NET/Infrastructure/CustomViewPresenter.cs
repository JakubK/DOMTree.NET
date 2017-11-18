using DOMTree.NET.Views;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DOMTree.NET
{
    public class CustomViewPresenter : MvxMultiRegionWpfViewPresenter
    {
        ContentControl _contentControl;

        public CustomViewPresenter(ContentControl contentControl)
            : base(contentControl)
        {
            _contentControl = contentControl;
        }

        public override void ChangePresentation(MvxPresentationHint hint)
        {
           

            base.ChangePresentation(hint);
        }
    }
}
