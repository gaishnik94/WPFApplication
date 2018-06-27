namespace Teploseti
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Труба
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Труба()
        {
            Обслуживание = new HashSet<Обслуживание>();
            Расчетные_потери = new HashSet<Расчетные_потери>();
            Фактические_потери = new HashSet<Фактические_потери>();
        }

        [Key]
        [StringLength(20)]
        public string Код_трубы { get; set; }

        public int Длина { get; set; }

        public double d_внутр_материал { get; set; }

        public double d_наруж_материал { get; set; }

        public double d_внутр_изол { get; set; }

        public double d_наруж_изол { get; set; }

        public int id_материала { get; set; }

        public int id_участка { get; set; }

        public virtual Материал Материал { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Обслуживание> Обслуживание { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Расчетные_потери> Расчетные_потери { get; set; }

        public virtual Участок Участок { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Фактические_потери> Фактические_потери { get; set; }
    }
}
