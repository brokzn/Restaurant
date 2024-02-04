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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Restaurant.Wforms.PostForms
{
    /// <summary>
    /// Логика взаимодействия для AddPost.xaml
    /// </summary>
    public partial class AddPost : Window
    {
        public AddPost()
        {
            InitializeComponent();
        }

        private void Save_Add_Post_Click(object sender, RoutedEventArgs e)
        {
            try
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

                Restaurant_Posts restaurant_Posts = new Restaurant_Posts();
                restaurant_Posts.Post_Name = Post_Name_Add.Text;
                restaurant_Posts.Salary = Salary_Add.Text;
                restaurant_Posts.Responsibilities = Responsibilities_Add.Text;
                restaurant_Posts.Requirements = Requirements_Add.Text;
                AppData.db.Restaurant_Posts.Add(restaurant_Posts);
                AppData.db.SaveChanges();
                MessageBox.Show("Данные  добавлены", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
}

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void СloseAppButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем текущее окно
            var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.Title == "Добавить");

            // Если окно найдено, закрываем его
            if (currentWindow != null)
            {
                currentWindow.Close();
            }
        }

        private void TrayAppButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем текущее окно
            var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.Title == "Добавить");

            // Если окно найдено, сворачиваем его
            if (currentWindow != null)
            {
                currentWindow.WindowState = WindowState.Minimized;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
