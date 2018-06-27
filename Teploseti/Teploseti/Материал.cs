namespace Teploseti
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Материал
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Материал()
        {
            Коэффициенты_учитыв__тепловые_потери = new HashSet<Коэффициенты_учитыв__тепловые_потери>();
            Труба = new HashSet<Труба>();
        }

        [Key]
        public int id_материала { get; set; }

        [Required]
        public string Наименование_материала { get; set; }

        public double Коэффициент_теплопроводности_материал { get; set; }

        public string Наименование_изоляции { get; set; }

        public double Коэффициент_теплопроводности_изоляция { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Коэффициенты_учитыв__тепловые_потери> Коэффициенты_учитыв__тепловые_потери { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Труба> Труба { get; set; }
    }
}
