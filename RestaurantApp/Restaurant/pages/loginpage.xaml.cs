using Restaurant.Model;
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
using System.Configuration;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace Restaurant.pages
{
    /// <summary>
    /// Логика взаимодействия для loginpage.xaml
    /// </summary>
    public partial class loginpage : Page
    {
        public loginpage()
        {
            InitializeComponent();
            
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(LoginTB.Text) && !string.IsNullOrEmpty(PassTextBox.Text))
            {
                try
                {
                    ErrorBodyRectangle.Visibility = Visibility.Hidden;
                    ErrorMessageAuthLabel.Visibility = Visibility.Hidden;
                    LoginButton.IsEnabled = false;
                    loadingIndicator.Visibility = Visibility.Visible;
                    await Task.Delay(3000);

                    var CurrentUser = AppData.db.Restaurant_Employees.FirstOrDefault(u => u.Firstname == LoginTB.Text 
                    && u.Middlename == PassTextBox.Text);
                    if (CurrentUser != null)
                    {
                        App.CurrentUserFirstname = CurrentUser.Firstname;
                        menupage menu = new menupage();
                        NavigationService.Navigate(menu);
                    }
                    else
                    {
                        LoginButton.IsEnabled = true;
                        ErrorBodyRectangle.Visibility = Visibility.Visible;
                        ErrorMessageAuthLabel.Visibility = Visibility.Visible;
                        ErrorMessageAuthLabel.Content = "Аккаунт не найден.";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка" + ex.Message);
                }
                finally
                {
                    loadingIndicator.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                ErrorBodyRectangle.Visibility = Visibility.Visible;
                ErrorMessageAuthLabel.Visibility = Visibility.Visible;
                ErrorMessageAuthLabel.Content = "Заполните все поля!";
            }
           
        }

        private void LoginTB_Loaded(object sender, RoutedEventArgs e)
        {
            LoginTB.Text = "Иван";
        }

        private void PassTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            PassTextBox.Text = "Иванович";
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
