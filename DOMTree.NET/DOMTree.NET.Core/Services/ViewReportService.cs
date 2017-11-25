using DOMTree.NET.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Core.Views;
using MvvmCross.Core.ViewModels;

namespace DOMTree.NET.Core.Services
{
    public class ViewReportService : IViewReportService<Type>
    {
        public List<Type> CurrentViews { get; set; }

        public ViewReportService()
        {
            if(CurrentViews == null)
                CurrentViews = new List<Type>();
        }

        public bool IsLoaded(Type view)
        {
            return CurrentViews.Contains(view) ? true : false;
        }

        public void AddView(Type view)
        {
            if(!IsLoaded(view))
                CurrentViews.Add(view);
        }

        public void RemoveView(Type view)
        {
            if (IsLoaded(view))
                CurrentViews.Remove(view);
        }
    }
}