namespace Teploseti
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBase : DbContext
    {
        public DBase()
            : base("name=DBase")
        {
        }
        public DBase(string conn)
            : base(conn)
        {
        }

        public virtual DbSet<Аккаунты> Аккаунты { get; set; }
        public virtual DbSet<Коэффициенты_теплоотдачи> Коэффициенты_теплоотдачи { get; set; }
        public virtual DbSet<Коэффициенты_учитыв__тепловые_потери> Коэффициенты_учитыв__тепловые_потери { get; set; }
        public virtual DbSet<Материал> Материал { get; set; }
        public virtual DbSet<Обслуживание> Обслуживание { get; set; }
        public virtual DbSet<Расчетные_потери> Расчетные_потери { get; set; }
        public virtual DbSet<Сотрудники> Сотрудники { get; set; }
        public virtual DbSet<Труба> Труба { get; set; }
        public virtual DbSet<Участок> Участок { get; set; }
        public virtual DbSet<Фактические_потери> Фактические_потери { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Аккаунты>()
                .Property(e => e.Логин)
                .IsUnicode(false);

            modelBuilder.Entity<Аккаунты>()
                .Property(e => e.Хэш_пароль)
                .IsUnicode(false);

            modelBuilder.Entity<Аккаунты>()
                .HasMany(e => e.Сотрудники)
                .WithRequired(e => e.Аккаунты)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Коэффициенты_теплоотдачи>()
                .Property(e => e.Номер_СНиП)
                .IsUnicode(false);

            modelBuilder.Entity<Коэффициенты_теплоотдачи>()
                .Property(e => e.Тип_поверхности)
                .IsUnicode(false);

            modelBuilder.Entity<Коэффициенты_теплоотдачи>()
                .HasMany(e => e.Расчетные_потери)
                .WithRequired(e => e.Коэффициенты_теплоотдачи)
                .HasForeignKey(e => e.id_коэфф_тепл_отдачи);

            modelBuilder.Entity<Коэффициенты_учитыв__тепловые_потери>()
                .Property(e => e.Номер_СНиП)
                .IsUnicode(false);

            modelBuilder.Entity<Коэффициенты_учитыв__тепловые_потери>()
                .HasMany(e => e.Расчетные_потери)
                .WithRequired(e => e.Коэффициенты_учитыв__тепловые_потери)
                .HasForeignKey(e => e.id_коэф_тепл_потери);

            modelBuilder.Entity<Материал>()
                .Property(e => e.Наименование_материала)
                .IsUnicode(false);

            modelBuilder.Entity<Материал>()
                .Property(e => e.Наименование_изоляции)
                .IsUnicode(false);

            modelBuilder.Entity<Материал>()
                .HasMany(e => e.Труба)
                .WithRequired(e => e.Материал)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Обслуживание>()
                .Property(e => e.Номер_бригады)
                .IsUnicode(false);

            modelBuilder.Entity<Обслуживание>()
                .Property(e => e.Результат)
                .IsUnicode(false);

            modelBuilder.Entity<Сотрудники>()
                .Property(e => e.Фамилия)
                .IsUnicode(false);

            modelBuilder.Entity<Сотрудники>()
                .Property(e => e.Имя)
                .IsUnicode(false);

            modelBuilder.Entity<Сотрудники>()
                .Property(e => e.Отчество)
                .IsUnicode(false);

            modelBuilder.Entity<Сотрудники>()
                .Property(e => e.Должность)
                .IsUnicode(false);

            modelBuilder.Entity<Сотрудники>()
                .HasMany(e => e.Обслуживание)
                .WithRequired(e => e.Сотрудники)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Сотрудники>()
                .HasMany(e => e.Расчетные_потери)
                .WithRequired(e => e.Сотрудники)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Сотрудники>()
                .HasMany(e => e.Фактические_потери)
                .WithRequired(e => e.Сотрудники)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Участок>()
                .Property(e => e.Наименование)
                .IsUnicode(false);

            modelBuilder.Entity<Участок>()
                .Property(e => e.Населенный_пункт)
                .IsUnicode(false);

            modelBuilder.Entity<Участок>()
                .HasMany(e => e.Труба)
                .WithRequired(e => e.Участок)
                .WillCascadeOnDelete(false);
        }
    }
}
