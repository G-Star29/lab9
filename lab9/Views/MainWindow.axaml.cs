using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using lab9.Models;
using lab9.ViewModels;
using System.IO;
using System.Linq;


namespace lab9.Views
{
    public partial class MainWindow : Window
    {
        private Carousel _carousel;
        private Button _goBack;
        private Button _goNext;


        public MainWindow()
        {
            this.InitializeComponent();

            _goBack.Click += (h, z) => _carousel.Previous();
            _goNext.Click += (h, z) => _carousel.Next();

        }



        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            _carousel = this.FindControl<Carousel>("carousel");
            _goBack = this.FindControl<Button>("GoBack");
            _goNext = this.FindControl<Button>("GoNext");


        }

        private void CheckCorrect(object sender, PointerReleasedEventArgs e)
        {

            string[] Filter = new[] {".png", ".jpg", ".jpeg" };
            TreeViewItem Item = sender as TreeViewItem;
            Node selected = Item.DataContext as Node;

            if (selected != null && Filter.Any(selected.PathFull.ToLower().EndsWith)) 
            {
                string path = selected.PathFull.Substring(0, selected.PathFull.IndexOf(selected.AllPaths));
                var files = Directory.EnumerateFiles(path).Where(file => Filter.Any(file.ToLower().EndsWith)).ToList();
                files.Remove(selected.PathFull);  
                var context = this.DataContext as MainWindowViewModel;
                context.Update(files, selected.PathFull);

            }

        }

        private void LoadNodes(object sender, TemplateAppliedEventArgs e)
        {
            ContentControl treeViewItem = sender as ContentControl;
            Node selected = treeViewItem.DataContext as Node;
            if (selected != null) selected.GetSubfolders();
        }
    }
}
