//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UlovDesktopApplication
{
    using System;
    using System.Collections.Generic;
    
    public partial class CompositonOrders
    {
        public int ID { get; set; }
        public int IDOrder { get; set; }
        public System.DateTime Date { get; set; }
        public int IDPointOfIsusse { get; set; }
        public int IDStatus { get; set; }
        public string Coupon { get; set; }
    
        public virtual Orders Orders { get; set; }
        public virtual PointsOfIsusse PointsOfIsusse { get; set; }
        public virtual Statuses Statuses { get; set; }
    }
}
