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

namespace Restaurant.Wforms.StorageForms
{
    /// <summary>
    /// Логика взаимодействия для AddStorage.xaml
    /// </summary>
    public partial class AddStorage : Window
    {
        public AddStorage()
        {
            InitializeComponent();
        }

        private void Save_Add_Storage_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Ingredient_Name_Add.Text) || datePicker1.SelectedDate == null || string.IsNullOrEmpty(Amount_Add.Text) ||
            string.IsNullOrEmpty(Shelf_Life_Add.Text) || string.IsNullOrEmpty(Cost_Add.Text) || string.IsNullOrEmpty(Supplier_Add.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            else
            {
                DateTime selectedDate;
                if (!DateTime.TryParse(datePicker1.Text, out selectedDate))
                {
                    MessageBox.Show("Выбрана некорректная дата", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    if(!int.TryParse(Cost_Add.Text, out int cost))
{
                        MessageBox.Show("Некорректный формат стоимости", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                     else
                    {
                        
                    
                    Restaurant_Storage restaurant_Storage = new Restaurant_Storage();
                    restaurant_Storage.Ingredient_Name = Ingredient_Name_Add.Text;
                    restaurant_Storage.Date_of_Issue = selectedDate;
                    restaurant_Storage.Amount = Amount_Add.Text;
                    restaurant_Storage.Shelf_Life = Shelf_Life_Add.Text;
                    restaurant_Storage.Cost = Convert.ToInt32(Cost_Add.Text);
                    restaurant_Storage.Supplier = Supplier_Add.Text;
                    AppData.db.Restaurant_Storage.Add(restaurant_Storage);
                    AppData.db.SaveChanges();
                    MessageBox.Show("Данные добавлены", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                   }
            }
            }


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void СloseAppButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем текущее окно
            var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.Title == "Добавить данные");

            // Если окно найдено, закрываем его
            if (currentWindow != null)
            {
                currentWindow.Close();
            }

        }

        private void TrayAppButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем текущее окно
            var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.Title == "Добавить данные");

            // Если окно найдено, сворачиваем его
            if (currentWindow != null)
            {
                currentWindow.WindowState = WindowState.Minimized;
            }
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
