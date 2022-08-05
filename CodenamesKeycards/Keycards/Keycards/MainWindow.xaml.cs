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

namespace Keycards
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string team = "";
            int maxNumberOfReds = 0;
            int currentReds = 0;
            int maxNumberOfBlues = 0;
            int currentBlues = 0;

            List<int> redIndexes = new List<int>();
            List<int> blueIndexes = new List<int>();

            Random rnd = new Random();
            SolidColorBrush scb = new SolidColorBrush();
            int idx = rnd.Next(1, 3);
            if (idx == 1)
            {
                scb = new SolidColorBrush(Colors.Red);
                team = "Red";
                maxNumberOfReds = 9;
                maxNumberOfBlues = 8;
            }
            else
            {
                scb = new SolidColorBrush(Colors.Blue);
                maxNumberOfReds = 8;
                maxNumberOfBlues = 9;
                team = "Blue";
            }

            foreach (Border border in FindVisualChildren<Border>(this))
            {
                border.BorderBrush = scb;
            }

            /*
            for(int i = 0; i< maxNumberOfBlues; i++)
            {
                int newIndex = rnd.Next(1, 26);
                if (!blueIndexes.Contains(newIndex))
                {
                    blueIndexes.Add(newIndex);
                }
            }
            */
            while(currentBlues != maxNumberOfBlues)
            {
                int newIndex = rnd.Next(1, 26);
                if (!blueIndexes.Contains(newIndex))
                {
                    blueIndexes.Add(newIndex);
                    currentBlues++;
                }

            }

            while (currentReds != maxNumberOfReds)
            {
                int newIndex = rnd.Next(1, 26);
                if (!redIndexes.Contains(newIndex))
                {
                    if (!blueIndexes.Contains(newIndex))
                    {
                        redIndexes.Add(newIndex);
                        currentReds++;
                    }
                }

            }


            bool foundPlace = false;
            int assassinIndex = 0;
            while (!foundPlace)
            {
                int newIndex = rnd.Next(1, 26);
                if(!redIndexes.Contains(newIndex))
                {
                    if (!blueIndexes.Contains(newIndex))
                    {
                        assassinIndex = newIndex;
                        foundPlace = true;
                    }
                }
            }

            /*
            for (int i = 0; i < maxNumberOfReds; i++)
            {
                int newIndex = rnd.Next(1, 26);
                if (!redIndexes.Contains(newIndex))
                {
                    if (!blueIndexes.Contains(newIndex))
                    {
                        redIndexes.Add(newIndex);
                    }
                }
            }
            */

            foreach (Rectangle rectangle in FindVisualChildren<Rectangle>(this))
            {
                string[] parsedName = rectangle.Name.Split('_');
                int place = Convert.ToInt32(parsedName[1]) * 5 + Convert.ToInt32(parsedName[2])+1;
                if (blueIndexes.Contains(place))
                {
                    rectangle.Fill = new SolidColorBrush(Colors.Blue);
                }
                else if (redIndexes.Contains(place))
                {
                    rectangle.Fill = new SolidColorBrush(Colors.Red);
                }
                else if(place == assassinIndex)
                {
                    rectangle.Fill = new SolidColorBrush(Colors.Black);
                }
                else
                {
                    rectangle.Fill = new SolidColorBrush(Colors.BlanchedAlmond);
                }
            }

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

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }
    }
}
