using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Codenames
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();          

        }

        string ReturnRandomWord(List<string> words)
        {
            string word;
            Random rnd = new Random();
            int idx = rnd.Next(words.Count());
            word = words[idx];
            words.RemoveAt(idx);
            return word;
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            List<string> words = System.IO.File.ReadAllLines("Words.txt").ToList();
            List<TextBlock> GrayText = new List<TextBlock>();
            List<TextBlock> DarkText = new List<TextBlock>();
            foreach (TextBlock tb in FindVisualChildren<TextBlock>(this))
            {
                string[] parsedName = tb.Name.Split('_');
                if (parsedName[0] == "TextBlockGray")
                {
                    GrayText.Add(tb);
                }
                else
                {
                    DarkText.Add(tb);
                }
            }

            foreach (var darkText in DarkText)
            {   
                darkText.Text = ReturnRandomWord(words);
                string[] parsedDarkName = darkText.Name.Split('_');
                int columnDark = Convert.ToInt32(parsedDarkName[1]);
                int rowDark = Convert.ToInt32(parsedDarkName[2]);

                foreach (var grayText in GrayText)
                {
                    string[] parsedGrayName = grayText.Name.Split('_');
                    int columnGray = Convert.ToInt32(parsedGrayName[1]);
                    int rowGray = Convert.ToInt32(parsedGrayName[2]);

                    if (columnDark == columnGray && rowDark == rowGray)
                    {
                        grayText.Text = darkText.Text;
                    }

                }
            }
        }

        private void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle nr = new Rectangle();
            nr = (Rectangle)e.OriginalSource;
            if ((string)nr.Tag == "0")
            {
                nr.Stroke = new SolidColorBrush(Colors.Black);
                nr.StrokeThickness = 2;
                nr.Opacity = 1;
                nr.Fill = new SolidColorBrush(Colors.Blue);
                nr.Tag = "1";
            }
            else if ((string)nr.Tag == "1")
            {
                nr.Stroke = new SolidColorBrush(Colors.Black);
                nr.StrokeThickness = 2;
                nr.Opacity = 1;
                nr.Fill = new SolidColorBrush(Colors.Red);
                nr.Tag = "2";
            }
            else if ((string)nr.Tag == "2")
            {
                nr.Stroke = new SolidColorBrush(Colors.Black);
                nr.StrokeThickness = 0;
                nr.Opacity = 0;
                nr.Tag = "0";
            }
        }

        private void MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle nr = new Rectangle();
            nr = (Rectangle)e.OriginalSource;
            nr.Stroke = new SolidColorBrush(Colors.Black);
            nr.StrokeThickness = 0;
            nr.Opacity = 0;
            nr.Tag = "0";
        }
    }
}
