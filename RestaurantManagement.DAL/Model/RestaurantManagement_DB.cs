namespace RestaurantManagement.DAL.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public  class RestaurantManagement_DB : DbContext
    {
        public RestaurantManagement_DB()
            : base("name=RestaurantManagement_DB")
        {
        }

        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<EatType> EatTypes { get; set; }
        public virtual DbSet<Emploee> Emploees { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Emploee>()
                .Property(e => e.Phonenumber)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Item>()
                .Property(e => e.Cost)
                .HasPrecision(19, 4);

        }
    }
}
