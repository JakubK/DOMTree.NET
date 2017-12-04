using DOMTree.NET.Core.Interfaces;
using DOMTree.NET.Core.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMTree.NET.Services
{
    /// <summary>
    /// Is responsible for Loading text from file and storing it in a collection
    /// It can also expose some Document's Code if necessary
    /// </summary>
    public class DocumentService : IDocumentService
    {
        public ObservableCollection<Document> Documents { get; set; }

        OpenFileDialog openFileDialog;

        public DocumentService()
        {
            Documents = new ObservableCollection<Document>();
        }

        /// <summary>
        /// Returns the Uri Path from OpenFileDialog
        /// </summary>
        /// <returns></returns>
        public string SelectUri()
        {
            openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "txt files (*.txt)|*.txt|HTML documents (*.html)|*.html|XML Documents (*.xml)|(*.xml)";
            if (openFileDialog.ShowDialog() == true)
                return openFileDialog.InitialDirectory + openFileDialog.FileName;

            else
                return null;
        }

        /// <summary>
        /// Takes the Uri Path to the Input
        /// and returns CodeData from It
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public Document Load(string uri)
        {
            if(string.IsNullOrEmpty(uri))
                return null;
            
            if(Documents.Any(x => x.Uri == uri))
                return null;
            
            Document result = new Document();
            result.Uri = uri;
            result.FileName = Path.GetFileName(result.Uri);

            try
            {
                result.Code = File.ReadAllText(uri);
            }
            catch
            {
                throw new FileNotFoundException();
            }
            finally
            {
                result.ID = Documents.Count;
                Documents.Add(result);
            }

            return result;
        }
    }
}
