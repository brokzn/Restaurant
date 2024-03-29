//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OrderToRestaurant.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Menu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Menu()
        {
            this.Ticket = new HashSet<Ticket>();
            this.Ticket1 = new HashSet<Ticket>();
            this.Ticket2 = new HashSet<Ticket>();
        }
    
        public int Dish_code { get; set; }
        public string Food_Name { get; set; }
        public int Ingredient_Code_1 { get; set; }
        public string Ingredient_Amount_1 { get; set; }
        public int Ingredient_Code_2 { get; set; }
        public string Ingredient_Amount_2 { get; set; }
        public int Ingredient_Code_3 { get; set; }
        public string Ingredient_Amount_3 { get; set; }
        public int Cost { get; set; }
        public string Cooking_time { get; set; }
    
        public virtual Restaurant_Storage Restaurant_Storage { get; set; }
        public virtual Restaurant_Storage Restaurant_Storage1 { get; set; }
        public virtual Restaurant_Storage Restaurant_Storage2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ticket> Ticket { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ticket> Ticket1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ticket> Ticket2 { get; set; }
    }
}
