//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Site_Yönetim_Sistemi
{
    using System;
    using System.Collections.Generic;
    
    public partial class guvenlik
    {
        public int id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string telno { get; set; }
        public Nullable<int> apartmanId { get; set; }
    
        public virtual apartman apartman { get; set; }
    }
}
