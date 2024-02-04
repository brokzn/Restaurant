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
using Restaurant.Model;
using Restaurant.Wforms.StorageForms;
using System.Windows.Shapes;
using Restaurant.pages;
using System.Data.Entity.Migrations;

namespace Restaurant.Wforms.StorageForms
{
    /// <summary>
    /// Логика взаимодействия для EditStorage.xaml
    /// </summary>
    public partial class EditStorage : Window
    {
        private int _id { get; set; }
        private Restaurant_Storage RestaurantStorage { get; set; }

        public EditStorage(int id)
        {
            _id = id;

            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RestaurantStorage = AppData.db.Restaurant_Storage.FirstOrDefault(x => x.Ingredient_Code == _id);
            Ingredient_Name_Edit.Text = RestaurantStorage.Ingredient_Name;
            datePicker2.SelectedDate = RestaurantStorage.Date_of_Issue;
            Amount_Edit.Text = RestaurantStorage.Amount;
            Shelf_Life_Edit.Text = RestaurantStorage.Shelf_Life;
            Cost_Edit.Text = Convert.ToString(RestaurantStorage.Cost);
            Supplier_Edit.Text = RestaurantStorage.Supplier;

        }

        private void Save_Edit_Storage_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Ingredient_Name_Edit.Text) || datePicker2.SelectedDate == null || string.IsNullOrEmpty(Amount_Edit.Text) ||
            string.IsNullOrEmpty(Shelf_Life_Edit.Text) || string.IsNullOrEmpty(Cost_Edit.Text) || string.IsNullOrEmpty(Supplier_Edit.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            else
            {
                DateTime selectedDate;
                if (!DateTime.TryParse(datePicker2.Text, out selectedDate))
                {
                    MessageBox.Show("Выбрана некорректная дата", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    if (!int.TryParse(Cost_Edit.Text, out int cost))
                    {
                        MessageBox.Show("Некорректный формат стоимости", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        RestaurantStorage.Ingredient_Name = Ingredient_Name_Edit.Text;
                        RestaurantStorage.Date_of_Issue = selectedDate;
                        RestaurantStorage.Amount = Amount_Edit.Text;
                        RestaurantStorage.Shelf_Life = Shelf_Life_Edit.Text;
                        RestaurantStorage.Cost = Convert.ToInt32(Cost_Edit.Text);
                        RestaurantStorage.Supplier = Supplier_Edit.Text;

                        AppData.db.Restaurant_Storage.AddOrUpdate(RestaurantStorage);
                        AppData.db.SaveChanges();
                        MessageBox.Show("Данные добавлены", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Asterisk);


                        this.Close();
                    }
                }
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
    }
}
