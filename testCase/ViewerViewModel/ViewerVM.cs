using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Forms;
using testCase.Classes;

namespace testCase.ViewerViewModel
{
    class ViewerVM : INotifyPropertyChanged
    {
        private Node node;

        private BindingCommand openCommand;

        private BindingCommand closeCommand;

        public string Dir
        {
            get { return node.dir; }
            set
            {
                node.dir = value;
                OnPropertyChanged();
            }
        }

        public string ImageSource
        {
            get { return node.imageSource; }
            set
            {
                node.imageSource = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> Nodes
        {
            get { return node.nodes; }
            set
            {
                node.nodes = value;
                OnPropertyChanged();
            }
        }

        public ViewerVM()
        {
            node = new Node();
        }

        public BindingCommand OpenCommand
        {
            get
            {
                return openCommand ??
                  (openCommand = new BindingCommand(obj =>
                  {
                      FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                      folderBrowserDialog.ShowDialog();
                      UpdateDir(folderBrowserDialog.SelectedPath);
                      UpdateNodes();
                  }));
            }
        }

        public BindingCommand CloseCommand
        {
            get
            {
                return closeCommand ??
                  (closeCommand = new BindingCommand(window =>
                  {
                      (window as Window).Close();
                  }));
            }
        }

        private void UpdateDir(string path)
        {
            Dir = !path.Equals("") ? path : Dir;
        }

        public void UpdateImageSource(object item)
        {
            ImageSource = item.GetType().ToString().Equals("System.String") ? node.dir + @"\" + item.ToString() : ImageSource;
        }

        private void UpdateNodes()
        {
            if (!Dir.Equals(""))
            {
                Nodes.Clear();
                Nodes = new ObservableCollection<string>(
                    Directory.GetFiles(Dir).
                    Select(item => item.Replace(node.dir + @"\", "")).ToList().
                    FindAll(item => item.EndsWith(".jpg") || item.EndsWith(".jpeg") || item.EndsWith(".gif") || item.EndsWith(".png"))
                    );
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
