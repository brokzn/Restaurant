using Restaurant.CClasses;
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

namespace Restaurant.Wforms.EmployeesForms
{
    /// <summary>
    /// Логика взаимодействия для AddEmployees.xaml
    /// </summary>
    public partial class AddEmployees : Window
    {
        public AddEmployees()
        {
            InitializeComponent();
        }

        private void Save_Add_Emp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Firstname_Add.Text)
                || string.IsNullOrEmpty(Middlename_Add.Text)
                || string.IsNullOrEmpty(Lastname_Add.Text)
                || string.IsNullOrEmpty(Age_Add.Text)
                || string.IsNullOrEmpty(Gender_Add.Text)
                || string.IsNullOrEmpty(Adress_Add.Text)
                || string.IsNullOrEmpty(Phone_Add.Text)
                || string.IsNullOrEmpty(Passport_Add.Text)
                || string.IsNullOrEmpty(ComboboxPostSelect.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            else
            {
                Restaurant.Model.Restaurant_Employees restaurant_Post = new Restaurant.Model.Restaurant_Employees
                {
                    Firstname = Firstname_Add.Text,
                    Middlename = Middlename_Add.Text,
                    Lastname = Lastname_Add.Text,
                    Age = Age_Add.Text,
                    Gender = Gender_Add.Text,
                    Adress = Adress_Add.Text,
                    Phone = Phone_Add.Text,
                    Passport = Passport_Add.Text
                };

                if (ComboboxPostSelect.SelectedItem is PostClass post)
                {
                    restaurant_Post.Post_Code = post.Post_Code;
                }

                using (var context = new RestaurantEntities())
                {
                    context.Restaurant_Employees.Add(restaurant_Post);
                    context.SaveChanges();
                }


                MessageBox.Show("Данные добавлены", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Asterisk);


            }
        }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (var context = new RestaurantEntities())
            {
                List<PostClass> posts = context.Restaurant_Posts
                    .Select(i => new PostClass
                    {
                        Post_Code = i.Post_Code,
                        Post_Name = i.Post_Name
                    })
                    .ToList();

                ComboboxPostSelect.ItemsSource = posts;
               
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
    }
}
