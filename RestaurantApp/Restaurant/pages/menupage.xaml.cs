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
using System.Configuration;
using System.IO;


namespace Restaurant.pages
{
    /// <summary>
    /// Логика взаимодействия для menupage.xaml
    /// </summary>
    public partial class menupage : Page
    {
        public string Firstname { get; set; }
        public menupage()
        {
            InitializeComponent();

        }
      

        private void StorageMenuItem_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new storagepage());
        
        }

        private void DeliveryMenuItem_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ticketpage());
        }

        private void MenuMenuItem_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new foodmenupage());
        }

       private  void RotateMainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            menupage menu = new menupage();
            menu.Firstname = Firstname; // Сохранение значения Firstname при обновлении страницы
            NavigationService.Navigate(menu, Firstname);
        }

        private void employeesItem_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new employeespage());
        }

        private void ticketitem_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new postspage());
        }

        private void ExitFromMainMenu_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы точно уверены, что хотите выйти?", "Подтверждение выхода", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                NavigationService.Navigate(new loginpage());
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(App.CurrentUserFirstname))
            {
                string firstname = App.CurrentUserFirstname;
                DateTime currentTime = DateTime.Now;
                if (currentTime.Hour < 6 || currentTime.Hour >= 23)
                {
                    ToUserMess.Content = $"Доброй ночи, {firstname}!";
                }
                else if (currentTime.Hour >= 6 && currentTime.Hour < 12)
                {
                    ToUserMess.Content = $"Доброе утро, {firstname}!";
                }
                else if (currentTime.Hour >= 12 && currentTime.Hour < 18)
                {
                    ToUserMess.Content = $"Добрый день, {firstname}!";
                }
                else
                {
                    ToUserMess.Content = $"Добрый вечер, {firstname}!";
                }
            }

        }
        
    }
}
