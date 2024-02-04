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

namespace OrderToRestaurant
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Загрузка страницы авторизации
           mainframe.Navigate(new WelcomePage());

        }
        //Бар перемещения окна
        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        //Обработчик закрытия окна 
        private void СloseAppButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }
        //Обработчик сворачивания окна
        private void TrayAppButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;

        }
        private void ResizeClick_Click(object sender, RoutedEventArgs e)
        {
           
            this.WindowState = WindowState.Normal;
            this.WindowStyle = WindowStyle.None;
            this.Top = 0;
            this.Left = 0;
            this.Width = SystemParameters.PrimaryScreenWidth;
            this.Height = SystemParameters.WorkArea.Height;

        }

        private void Resize2Click_Click(object sender, RoutedEventArgs e)
        {
            
            WindowState = WindowState.Normal;
            this.Width = 1550;
            this.Height = 870;
        }

        private void UpdatePageLabel(string pageTitle)
        {
            WinInfoLabel.Content = pageTitle;
        }

        private void mainframe_Navigated(object sender, NavigationEventArgs e)
        {
            if (e.Content is Page currentPage)
            {
                UpdatePageLabel(currentPage.Title);
            }
        }
    }
}
