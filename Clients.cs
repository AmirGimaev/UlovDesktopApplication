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
    
    public partial class Clients
    {
        public int ID { get; set; }
        public string FIO { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public int IDUser { get; set; }
    
        public virtual Users Users { get; set; }
    }
}