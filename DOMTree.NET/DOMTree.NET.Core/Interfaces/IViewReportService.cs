using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMTree.NET.Core.Interfaces
{
    public interface IViewReportService<Type>
    {
        /// <summary>
        /// Stores all currently shown views
        /// </summary>
        List<Type> CurrentViews { get; set; }

        /// <summary>
        /// Checks if the view is currently begin rendered
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        bool IsLoaded(Type view);

        void AddView(Type view);

        void RemoveView(Type view);
    }
}
