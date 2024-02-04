using Restaurant.CClasses;
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


namespace Restaurant.Wforms.EmployeesForms
{
    /// <summary>
    /// Логика взаимодействия для EditEmployees.xaml
    /// </summary>
    public partial class EditEmployees : Window
    {
        private int _id { get; set; }
        private Restaurant.Model.Restaurant_Employees RestaurantEmp { get; set; }
        public EditEmployees(int id)
        {
            _id = id;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                RestaurantEmp = AppData.db.Restaurant_Employees.FirstOrDefault(x => x.Employee_code == _id);

            Firstname_Add.Text = RestaurantEmp.Firstname;
            Middlename_Add.Text = RestaurantEmp.Middlename;
            Lastname_Add.Text = RestaurantEmp.Lastname;
            Age_Add.Text = RestaurantEmp.Age;
            Gender_Add.Text = RestaurantEmp.Gender;
            Adress_Add.Text = RestaurantEmp.Adress;
            Phone_Add.Text = RestaurantEmp.Phone;
            Passport_Add.Text = RestaurantEmp.Passport;


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



                ComboboxPostSelect.SelectedItem = posts.FirstOrDefault(i => i.Post_Code == RestaurantEmp.Post_Code);
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


                    var existingPost = AppData.db.Restaurant_Employees.FirstOrDefault(m => m.Employee_code == _id);
                    if (existingPost != null)
                    {
                        existingPost.Firstname = restaurant_Post.Firstname;
                        existingPost.Middlename = restaurant_Post.Middlename;
                        existingPost.Lastname = restaurant_Post.Lastname;
                        existingPost.Age = restaurant_Post.Age;
                        existingPost.Gender = restaurant_Post.Gender;
                        existingPost.Adress = restaurant_Post.Adress;
                        existingPost.Phone = restaurant_Post.Phone;
                        existingPost.Passport = restaurant_Post.Passport;
                        existingPost.Post_Code = restaurant_Post.Post_Code;
                        AppData.db.SaveChanges();
                        MessageBox.Show("Данные обновлены", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка изменения информации", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
