namespace Teploseti
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Обслуживание
    {
        [Key]
        public int id_обслуживания { get; set; }

        [Required]
        [StringLength(20)]
        public string Код_трубы { get; set; }

        [Required]
        public string Номер_бригады { get; set; }

        [Required]
        public string Результат { get; set; }

        [Column(TypeName = "date")]
        public DateTime Дата { get; set; }

        public int id_сотрудника { get; set; }

        public virtual Сотрудники Сотрудники { get; set; }

        public virtual Труба Труба { get; set; }
    }
}
