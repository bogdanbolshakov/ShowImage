using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Forms;

namespace testCase
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> picturesList = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
        }
        private string SelectDirWithPictures()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();
            return folderBrowserDialog?.SelectedPath;
        }
        private void SetListPictures(string path)
        {
            if(!path.Equals(""))
            {
                var dirFiles = Directory.GetFiles(path).ToList();
                picturesList.Clear();
                picturesList = dirFiles.FindAll(item => item.EndsWith(".jpg") || item.EndsWith(".jpeg") ||
                    item.EndsWith(".gif") || item.EndsWith(".png"));
            }
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            SetListPictures(SelectDirWithPictures());
            picturesList.ForEach(item => AddNodeToTreeViewItemPictures(item));
            TreeView_Pictures.Items.Refresh();
        }
        private void AddNodeToTreeViewItemPictures(string picturePath)
        {
            var node = new TreeViewItem
            {
                Header = Path.GetFileName(picturePath),
                Tag = picturePath,
                ToolTip = "Щелкните два раза мышкой для открытия изображения",
                FontWeight = FontWeights.Medium
            };
            TreeView_Pictures.Items.Add(node);
            node.MouseDoubleClick += Node_MouseDoubleClick;
        }
        private void Node_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Image_Field.Source = new BitmapImage(new Uri((sender as TreeViewItem).Tag.ToString()));
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Не удалось открыть изображение.\nВозможно оно имеет неподдерживаемый формат.");
            }
        }
    }
}
