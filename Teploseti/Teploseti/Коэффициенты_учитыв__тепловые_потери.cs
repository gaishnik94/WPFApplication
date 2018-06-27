namespace Teploseti
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("[Коэффициенты учитыв. тепловые потери]")]
    public partial class Коэффициенты_учитыв__тепловые_потери
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Коэффициенты_учитыв__тепловые_потери()
        {
            Расчетные_потери = new HashSet<Расчетные_потери>();
        }

        [Key]
        public int id_коэфф_тепл_потери { get; set; }

        [Required]
        public string Номер_СНиП { get; set; }

        public int id_материала { get; set; }

        public double Нач_диаметр { get; set; }

        public double Кон_диаметр { get; set; }

        public double Коэффициент { get; set; }

        public virtual Материал Материал { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Расчетные_потери> Расчетные_потери { get; set; }
    }
}
