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
    public class DocumentService : IDocumentService
    {
        public ObservableCollection<CodeData> Uris { get; set; }

        OpenFileDialog openFileDialog;

        public DocumentService()
        {
            Uris = new ObservableCollection<CodeData>();
        }

        public string SelectUri()
        {
            openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "txt files (*.txt)|*.txt|HTML documents (*.html)|*.html|XML Documents (*.xml)|(*.xml)";
            if (openFileDialog.ShowDialog() == true)
                return openFileDialog.InitialDirectory + openFileDialog.FileName;

            else
                return null;
        }

        public CodeData Load(string uri)
        {
            if(string.IsNullOrEmpty(uri))
            {
                return null;
            }
            CodeData result = new CodeData();
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
                Uris.Add(result);
            }

            return result;
        }
    }
}
