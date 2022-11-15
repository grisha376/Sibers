//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sibers.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Project
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ID_customer { get; set; }
        public int ID_executor { get; set; }
        public int ID_leader { get; set; }
        public Nullable<System.DateTime> Date_of_start { get; set; }
        public Nullable<System.DateTime> Date_of_end { get; set; }
        public Nullable<int> Prority { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Employees Employees { get; set; }
        public virtual Executor Executor { get; set; }
    }
}