namespace Teploseti
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Аккаунты
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Аккаунты()
        {
            Сотрудники = new HashSet<Сотрудники>();
        }

        [Key]
        public int id_аккаунта { get; set; }

        [Required]
        [StringLength(50)]
        public string Логин { get; set; }

        [Required]
        public string Хэш_пароль { get; set; }

        public bool Администратор { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Сотрудники> Сотрудники { get; set; }
    }
}
