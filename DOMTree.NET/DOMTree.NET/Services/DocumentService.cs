using DOMTree.NET.Core.Interfaces;
using DOMTree.NET.Core.Models;
using Microsoft.Win32;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public class DocumentService : MvxNotifyPropertyChanged,IDocumentService
    {
        ObservableCollection<Document> documents;
        public ObservableCollection<Document> Documents
        {
            get { return documents; }
            set
            {
                documents = value;
                RaisePropertyChanged(() => Documents);
            }
        }

        OpenFileDialog openFileDialog;
        SaveFileDialog saveFileDialog;

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

        public bool UnLoad(Document doc)
        {
            Documents.Remove(doc);
            return true;
        }

        public bool SaveFile(string uri, Document doc, bool overwrite = true)
        {
            if (!File.Exists(uri) && overwrite)
            {
                return SaveFileAs(doc);
            }

            File.WriteAllText(uri, doc.Code);
            return true;
        }

        public bool SaveFileAs(Document doc)
        {
            saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Select path for this file";
            saveFileDialog.DefaultExt = "html";
            saveFileDialog.CheckFileExists = false;
            saveFileDialog.CheckPathExists = true;
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.Filter = "txt files (*.txt)|*.txt|HTML documents (*.html)|*.html|XML Documents (*.xml)|(*.xml)";

            if (saveFileDialog.ShowDialog() == true)
            {
                Documents.First(x => x.ID == doc.ID).FileName = Path.GetFileName(saveFileDialog.FileName);
                Documents.First(x => x.ID == doc.ID).Uri = saveFileDialog.FileName;
                SaveFile(saveFileDialog.FileName, doc,false);
            }

            return true;
        }

        public bool SaveAll()
        {
            for(int i = 0;i < Documents.Count;i++)
            {
                SaveFile(Documents[i].Uri, Documents[i]);
            }
            return true;
        }

        public Document CreateNew()
        {
            Document Doc = new Document();
            Doc.ID = Documents.Count;
            Doc.FileName = "New Document " + Doc.ID;
            Doc.Uri = Doc.FileName;
            Doc.Code = string.Empty;

            Documents.Add(Doc);

            return Doc;
        }
    }
}
