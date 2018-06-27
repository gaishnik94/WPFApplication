namespace Teploseti
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Коэффициенты теплоотдачи")]
    public partial class Коэффициенты_теплоотдачи
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Коэффициенты_теплоотдачи()
        {
            Расчетные_потери = new HashSet<Расчетные_потери>();
        }

        [Key]
        public int id_коэф_тепл_отдачи { get; set; }

        public string Номер_СНиП { get; set; }

        public double? t_до { get; set; }

        public double? t_после { get; set; }

        public string Тип_поверхности { get; set; }

        public double? Коэфф_помещения_с_малым_к_изл { get; set; }

        public double? Коэфф_помещения_с_высоким_к_изл { get; set; }

        public double? Коэфф_откр_с_малым_к_изл { get; set; }

        public double? Коэфф_откр_с_высоким_к_изл { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Расчетные_потери> Расчетные_потери { get; set; }
    }
}
