using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using testCase.Classes;

namespace testCase.ViewerViewModel
{
    class ViewerVM : INotifyPropertyChanged
    {
        private string dir;

        private string imageSource;

        private ObservableCollection<string> nodes;

        public string Dir
        {
            get { return dir; }
            set
            {
                dir = value;
                OnPropertyChanged("Dir");
            }
        }

        public string ImageSource
        {
            get { return imageSource; }
            set
            {
                imageSource = value;
                OnPropertyChanged("ImageSource");
            }
        }

        public ObservableCollection<string> Nodes
        {
            get { return nodes; }
            set
            {
                nodes = value;
                OnPropertyChanged("Nodes");
            }
        }

        public ViewerVM()
        {
            dir = string.Empty;
            nodes = new ObservableCollection<string>();
        }

        public void SetImageSource(object item)
        {
            if(item.GetType().ToString().Equals("System.String"))
            {
                ImageSource = dir + @"\" + item.ToString();
            }
        }

        private BindingCommand openCommand;

        public BindingCommand OpenCommand
        {
            get
            {
                return openCommand ??
                  (openCommand = new BindingCommand(obj =>
                  {
                      FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                      folderBrowserDialog.ShowDialog();
                      Dir = !folderBrowserDialog.SelectedPath.Equals("") ? folderBrowserDialog.SelectedPath : Dir;
                      UpdateNodes();
                  }));
            }
        }

        private void UpdateNodes()
        {
            Nodes.Clear();
            Nodes = new ObservableCollection<string>(Directory.GetFiles(Dir).
                Select(item => item.Replace(dir + @"\", "")).ToList().
                FindAll(item => item.EndsWith(".jpg") || item.EndsWith(".jpeg") || item.EndsWith(".gif") || item.EndsWith(".png")));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
