using OrderToRestaurant.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
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

namespace OrderToRestaurant
{
    /// <summary>
    /// Логика взаимодействия для OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        public OrderPage()
        {
            InitializeComponent();
        }

        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ComboboxDish1Select.Text)
                            || string.IsNullOrEmpty(Customers_Name_Add.Text)
                            || string.IsNullOrEmpty(Phone_Number_Add.Text)
                            || string.IsNullOrEmpty(Delivery_note_Add.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            else
            {
                OrderToRestaurant.Model.Ticket restaurant_Order = new OrderToRestaurant.Model.Ticket
                {
                    Customers_Name = Customers_Name_Add.Text,
                    Phone_Number = Phone_Number_Add.Text,
                    Delivery_note = Delivery_note_Add.Text,
                    Ticket_Date = DateTime.Now,
                    Ticket_Time = DateTime.Now.TimeOfDay,
                    Cost = "500",
                    Employee_code = 1,
                    Completion_Date = DateTime.Now,
                    Completion_Time = DateTime.Now.TimeOfDay
                };
            if (ComboboxDish1Select.SelectedItem is DishClass ingredient1)
            {
                    restaurant_Order.Dish_code_1 = ingredient1.Dish_Code;
                }

                if (ComboboxDish1Select.SelectedItem is DishClass ingredient2)
                {
                    restaurant_Order.Dish_code_2 = ingredient2.Dish_Code;
                }
                if (ComboboxDish1Select.SelectedItem is DishClass ingredient3)
                {
                    restaurant_Order.Dish_code_3 = ingredient3.Dish_Code;
                }

                using (var context = new RestaurantEntities())
                {
                    context.Ticket.Add(restaurant_Order);
                    context.SaveChanges();
                }

                MessageBox.Show("Cпасибо за покупку, ожидайте!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            using (var context = new RestaurantEntities())
            {
                List<DishClass> ingredients = context.Menu
                    .Select(i => new DishClass
                    {
                        Dish_Code = i.Dish_code,
                        Food_Name = i.Food_Name,
                    })
                    .ToList();

                ComboboxDish1Select.ItemsSource = ingredients;
                ComboboxDish2Select.ItemsSource = ingredients;
                ComboboxDish3Select.ItemsSource = ingredients;
            }
        }

        
    }
}
