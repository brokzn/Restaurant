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
    
    public partial class Restaurant_Posts
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Restaurant_Posts()
        {
            this.Restaurant_Employees = new HashSet<Restaurant_Employees>();
        }
    
        public int Post_Code { get; set; }
        public string Post_Name { get; set; }
        public string Salary { get; set; }
        public string Responsibilities { get; set; }
        public string Requirements { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Restaurant_Employees> Restaurant_Employees { get; set; }
    }
}
