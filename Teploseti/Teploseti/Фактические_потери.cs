namespace Teploseti
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Фактические потери")]
    public partial class Фактические_потери
    {
        [Key]
        public int id_факт_потерь { get; set; }

        public double Значение { get; set; }

        [Column(TypeName = "date")]
        public DateTime Дата { get; set; }

        [Required]
        [StringLength(20)]
        public string Код_трубы { get; set; }

        public int id_сотрудника { get; set; }

        public virtual Сотрудники Сотрудники { get; set; }

        public virtual Труба Труба { get; set; }
    }
}
