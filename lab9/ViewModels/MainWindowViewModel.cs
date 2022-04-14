using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.IO;
using Avalonia.Media.Imaging;
using System.Reactive;
using lab9.Models;

namespace lab9.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {


        bool buttonEnable = false;

        bool ButtonEnable
        {
            get => buttonEnable;
            set => this.RaiseAndSetIfChanged(ref buttonEnable, value);
        }

        public ObservableCollection<Node> Items { get; }
        public ObservableCollection<Node> SelectedItems { get; }
        public string strFolder { get; set; }

        public ObservableCollection<Image> PathsNames { get; set; }

        List<string> Files;

        public class Image
        {
            public string PathImage { get; set; }
            public Bitmap ImageFile { get; set; }
            
            public Image(string path)
            {
                ImageFile = new Bitmap(path);
                PathImage = path;
            }
        }


        public MainWindowViewModel()
        {
            string root = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            strFolder = root; 

            Items = new ObservableCollection<Node>();
            Files = new List<string>();

            PathsNames = new ObservableCollection<Image>();
            DriveInfo[] All_Files = DriveInfo.GetDrives();

            for (int i = 0; i < All_Files.Length; i++)
            {
                Files.Add(All_Files[i].Name);
                Node rootNodee = new Node(All_Files[i].Name);
                Items.Add(rootNodee);
            }
            
        }

        public void Update(List<string> imagesRoots, string Image)
        {
            PathsNames.Clear();
            PathsNames.Add(new Image(Image));
            for (int i = 0; i < imagesRoots.Count; i++)
                PathsNames.Add(new Image(imagesRoots[i]));

            if(imagesRoots.Count > 1)
            {
                ButtonEnable = true;
            }
            else ButtonEnable = false;
        }





      
    }
}

   
