//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NinjaManager
{
    using System;
    using System.Collections.Generic;
    using NinjaManager.ViewModel;

    public partial class EquipmentCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EquipmentCategory()
        {
            this.Equipments = new HashSet<Equipment>();
        }

        internal EquipmentCategoryViewModel ToPoCo()
        {
            return new EquipmentCategoryViewModel(this);
        }

        public int CategoryId { get; set; }
        public string Name { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Equipment> Equipments { get; set; }
    }
}
