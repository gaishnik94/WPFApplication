namespace Teploseti
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Расчетные потери")]
    public partial class Расчетные_потери
    {
        [Key]
        public int id_расчетных_потерь { get; set; }

        public double t_воды { get; set; }

        public double t_среды { get; set; }

        public int id_коэфф_тепл_отдачи { get; set; }

        public int Номер_коэфф { get; set; }

        public int id_коэф_тепл_потери { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Дата { get; set; }

        [Required]
        [StringLength(20)]
        public string Код_трубы { get; set; }

        public int id_сотрудника { get; set; }

        public double Результат { get; set; }

        public DateTime? Дата_расчета { get; set; }

        public virtual Коэффициенты_теплоотдачи Коэффициенты_теплоотдачи { get; set; }

        public virtual Коэффициенты_учитыв__тепловые_потери Коэффициенты_учитыв__тепловые_потери { get; set; }

        public virtual Сотрудники Сотрудники { get; set; }

        public virtual Труба Труба { get; set; }
    }
}
