using System.Collections.ObjectModel;

namespace testCase.Classes
{
    sealed class Node
    {
        public string dir { get; set; }

        public string imageSource { get; set; }

        public ObservableCollection<string> nodes { get; set; }

        public Node()
        {
            dir = string.Empty;
            imageSource = string.Empty;
            nodes = new ObservableCollection<string>();
        }
    }
}
