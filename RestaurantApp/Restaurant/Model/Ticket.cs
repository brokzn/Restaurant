//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Restaurant.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ticket
    {
        public int Ticket_Code { get; set; }
        public System.DateTime Ticket_Date { get; set; }
        public System.TimeSpan Ticket_Time { get; set; }
        public string Customers_Name { get; set; }
        public string Phone_Number { get; set; }
        public int Dish_code_1 { get; set; }
        public int Dish_code_2 { get; set; }
        public int Dish_code_3 { get; set; }
        public string Cost { get; set; }
        public string Delivery_note { get; set; }
        public int Employee_code { get; set; }
        public System.DateTime Completion_Date { get; set; }
        public System.TimeSpan Completion_Time { get; set; }
    
        public virtual Menu Menu { get; set; }
        public virtual Menu Menu1 { get; set; }
        public virtual Menu Menu2 { get; set; }
        public virtual Restaurant_Employees Restaurant_Employees { get; set; }
    }
}
