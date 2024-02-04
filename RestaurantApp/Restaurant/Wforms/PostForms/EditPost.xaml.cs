using Restaurant.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
using System.Windows.Shapes;

namespace Restaurant.Wforms.PostForms
{
    /// <summary>
    /// Логика взаимодействия для EditPost.xaml
    /// </summary>
    public partial class EditPost : Window
    {
        private int _id { get; set; }
        private Restaurant_Posts RestaurantPosts { get; set; }
        public EditPost(int id)
        {
            _id = id;
            InitializeComponent();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RestaurantPosts = AppData.db.Restaurant_Posts.FirstOrDefault(x => x.Post_Code == _id);
            Post_Name_Add.Text = RestaurantPosts.Post_Name;
            Salary_Add.Text = RestaurantPosts.Salary;
            Responsibilities_Add.Text = RestaurantPosts.Responsibilities;
            Requirements_Add.Text = RestaurantPosts.Requirements;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void СloseAppButton_Click(object sender, RoutedEventArgs e)
        {
            // Находим все окна с заголовком "Изменить"
            var windowsToClose = Application.Current.Windows.OfType<Window>().Where(x => x.Title == "Изменить").ToList();

            // Закрываем все найденные окна
            foreach (var window in windowsToClose)
            {
                window.Close();
            }
        }

        private void TrayAppButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем текущее окно
            var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.Title == "Изменить");

            // Если окно найдено, сворачиваем его
            if (currentWindow != null)
            {
                currentWindow.WindowState = WindowState.Minimized;
            }
        }

        private void Save_Edit_Post_Click(object sender, RoutedEventArgs e)
        {
               if (string.IsNullOrEmpty(Post_Name_Add.Text)
                || string.IsNullOrEmpty(Salary_Add.Text)
                || string.IsNullOrEmpty(Responsibilities_Add.Text)
                || string.IsNullOrEmpty(Requirements_Add.Text))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
                else
                {

                    RestaurantPosts.Post_Name = Post_Name_Add.Text;
                    RestaurantPosts.Salary = Salary_Add.Text;
                    RestaurantPosts.Responsibilities = Responsibilities_Add.Text;
                    RestaurantPosts.Requirements = Requirements_Add.Text;


                    AppData.db.Restaurant_Posts.AddOrUpdate(RestaurantPosts);
                    AppData.db.SaveChanges();
                    MessageBox.Show("Данные добавлены", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Asterisk);


                    this.Close();

                }
           
        }
    }
}
