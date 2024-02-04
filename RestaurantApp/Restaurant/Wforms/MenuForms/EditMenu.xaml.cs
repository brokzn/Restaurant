using Restaurant.CClasses;
using Restaurant.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
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

namespace Restaurant.Wforms.MenuForms
{
    /// <summary>
    /// Логика взаимодействия для EditMenu.xaml
    /// </summary>
    public partial class EditMenu : Window
    {
        private int _id { get; set; }
        private Restaurant.Model.Menu RestaurantMenu { get; set; }

        public EditMenu(int id)
        {
            _id = id;
            InitializeComponent();
        }
       
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {


            RestaurantMenu = AppData.db.Menu.FirstOrDefault(x => x.Dish_code == _id);
            
            FoodName_Edit.Text = RestaurantMenu.Food_Name;
            Cost_Edit.Text = RestaurantMenu.Cost.ToString();
            CookingTime_Edit.Text = RestaurantMenu.Cooking_time;

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

                ComboboxIngredient1Select.SelectedItem = ingredients.FirstOrDefault(i => i.Ingredient_Code == RestaurantMenu.Ingredient_Code_1);
                ComboboxIngredient2Select.SelectedItem = ingredients.FirstOrDefault(i => i.Ingredient_Code == RestaurantMenu.Ingredient_Code_2);
                ComboboxIngredient3Select.SelectedItem = ingredients.FirstOrDefault(i => i.Ingredient_Code == RestaurantMenu.Ingredient_Code_3);
            }
            Amount_Ingredient_1_Edit.Text = RestaurantMenu.Ingredient_Amount_1;
            Amount_Ingredient_2_Edit.Text = RestaurantMenu.Ingredient_Amount_2;
            Amount_Ingredient_3_Edit.Text = RestaurantMenu.Ingredient_Amount_3;
        }

        private void СloseAppButton_Click(object sender, RoutedEventArgs e)
        {
            var windowsToClose = Application.Current.Windows.OfType<Window>().Where(x => x.Title == "Изменить").ToList();
            foreach (var window in windowsToClose)
            {
                window.Close();
            }
        }

        private void TrayAppButton_Click(object sender, RoutedEventArgs e)
        {
            var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.Title == "Изменить");
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

        private void Save_Edit_Menu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(CookingTime_Edit.Text)
                || string.IsNullOrEmpty(Cost_Edit.Text)
                || string.IsNullOrEmpty(FoodName_Edit.Text)
                || string.IsNullOrEmpty(Amount_Ingredient_1_Edit.Text)
                || string.IsNullOrEmpty(Amount_Ingredient_2_Edit.Text)
                || string.IsNullOrEmpty(ComboboxIngredient1Select.Text)
                || string.IsNullOrEmpty(ComboboxIngredient2Select.Text)
                || string.IsNullOrEmpty(ComboboxIngredient3Select.Text)
                || string.IsNullOrEmpty(Amount_Ingredient_3_Edit.Text))

                {
                    MessageBox.Show("Пожалуйста, заполните все поля.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
                else
                {
                    if (!int.TryParse(Cost_Edit.Text, out int cost))
                    {
                        MessageBox.Show("Некорректный формат стоимости", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        Restaurant.Model.Menu restaurant_Menu = new Restaurant.Model.Menu
                        {
                            Food_Name = FoodName_Edit.Text,

                            Cost = Convert.ToInt32(Cost_Edit.Text),
                            Cooking_time = CookingTime_Edit.Text,
                            Ingredient_Amount_1 = Amount_Ingredient_1_Edit.Text,
                            Ingredient_Amount_2 = Amount_Ingredient_2_Edit.Text,
                            Ingredient_Amount_3 = Amount_Ingredient_3_Edit.Text
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

                        var existingMenu = AppData.db.Menu.FirstOrDefault(m => m.Dish_code == _id);
                        if (existingMenu != null)
                        {
                            existingMenu.Food_Name = restaurant_Menu.Food_Name;
                            existingMenu.Cost = restaurant_Menu.Cost;
                            existingMenu.Cooking_time = restaurant_Menu.Cooking_time;
                            existingMenu.Ingredient_Amount_1 = restaurant_Menu.Ingredient_Amount_1;
                            existingMenu.Ingredient_Amount_2 = restaurant_Menu.Ingredient_Amount_2;
                            existingMenu.Ingredient_Amount_3 = restaurant_Menu.Ingredient_Amount_3;
                            existingMenu.Ingredient_Code_1 = restaurant_Menu.Ingredient_Code_1;
                            existingMenu.Ingredient_Code_2 = restaurant_Menu.Ingredient_Code_2;
                            existingMenu.Ingredient_Code_3 = restaurant_Menu.Ingredient_Code_3;

                            AppData.db.SaveChanges();
                            MessageBox.Show("Данные обновлены", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        }
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
