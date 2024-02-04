 using Restaurant.CClasses;
using Restaurant.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Restaurant.Wforms.MenuForms
{
    /// <summary>
    /// Логика взаимодействия для AddMenu.xaml
    /// </summary>
    public partial class AddMenu : Window
    {
        public AddMenu()
        {
            InitializeComponent();
        }

        private void Save_Add_Menu_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ComboboxIngredient1Select.Text) 
                || string.IsNullOrEmpty(FoodName_Add.Text)
                || string.IsNullOrEmpty(Cost_Add.Text) 
                || string.IsNullOrEmpty(CookingTime_Add.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            if (!int.TryParse(Cost_Add.Text, out int cost))
            {
                MessageBox.Show("Некорректный формат стоимости", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                Restaurant.Model.Menu restaurant_Menu = new Restaurant.Model.Menu
                {
                    Food_Name = FoodName_Add.Text,
                    Cost = Convert.ToInt32(Cost_Add.Text),
                    Cooking_time = CookingTime_Add.Text,
                    Ingredient_Amount_1 = Amount_Ingredient_1_Add.Text,
                    Ingredient_Amount_2 = Amount_Ingredient_2_Add.Text,
                    Ingredient_Amount_3 = Amount_Ingredient_3_Add.Text
                };


                if (ComboboxIngredient1Select.SelectedItem is DishClass ingredient1)
                {
                    restaurant_Menu.Ingredient_Code_1 = ingredient1.Ingredient_Code;
                }

                if (ComboboxIngredient2Select.SelectedItem is DishClass ingredient2)
                {
                    restaurant_Menu.Ingredient_Code_2 = ingredient2.Ingredient_Code;
                }

                if (ComboboxIngredient3Select.SelectedItem is DishClass ingredient3)
                {
                    restaurant_Menu.Ingredient_Code_3 = ingredient3.Ingredient_Code;
                }

                using (var context = new RestaurantEntities())
                {
                    context.Menu.Add(restaurant_Menu);
                    context.SaveChanges();
                }

                MessageBox.Show("Данные добавлены", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }




        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (var context = new RestaurantEntities())
            {
                List<DishClass> ingredients = context.Restaurant_Storage
                    .Select(i => new DishClass
                    {
                        Ingredient_Code = i.Ingredient_Code,
                        Ingredient_Name = i.Ingredient_Name
                    })
                    .ToList();

                ComboboxIngredient1Select.ItemsSource = ingredients;
                ComboboxIngredient2Select.ItemsSource = ingredients;
                ComboboxIngredient3Select.ItemsSource = ingredients;
            }

        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void СloseAppButton_Click(object sender, RoutedEventArgs e)
        {
            var windowsToClose = Application.Current.Windows.OfType<Window>().Where(x => x.Title == "Добавить").ToList();
            foreach (var window in windowsToClose)
            {
                window.Close();
            }
        }

        private void TrayAppButton_Click(object sender, RoutedEventArgs e)
        {
            var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.Title == "Добавить");
            if (currentWindow != null)
            {
                currentWindow.WindowState = WindowState.Minimized;
            }
        }
    }
}
