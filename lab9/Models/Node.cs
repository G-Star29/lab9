using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using System.Collections.ObjectModel;

namespace lab9.Models
{
    public class Node : INotifyPropertyChanged
    {

        public ObservableCollection<Node>? Notes { get; set; }
        public string AllPaths { get; set; }
        public string PathFull { get; set; }

        public Node(string fullRoot)
        {
            Notes = new ObservableCollection<Node>();
            PathFull = fullRoot;
            AllPaths = Path.GetFileName(fullRoot);
            if (PathFull.Length <= 3) AllPaths = fullRoot;
        }

        public event PropertyChangedEventHandler PropertyChanged;
		protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
			else return;
		}       

        public void GetSubfolders()
        {
            try
            {
                string[] Filter = new[] { ".png", ".jpg", ".jpeg" };

                IEnumerable<string> items = Directory.EnumerateDirectories(PathFull, "*", SearchOption.TopDirectoryOnly); 
                IEnumerable<string> files = Directory.EnumerateFiles(PathFull).Where(file => Filter.Any(file.ToLower().EndsWith)).ToList(); 

                foreach (string item in items) Notes.Add(new Node(item));
                foreach (string file in files) { Notes.Add(new Node(file)); };

            }
            catch { }

        }
    }
}

