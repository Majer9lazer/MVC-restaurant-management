namespace RestaurantManagement.DAL.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class RestaurantManagement_DB : DbContext
    {
        public RestaurantManagement_DB()
            : base("name=RestaurantManagement_DB")
        {
        }

        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<EatType> EatTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .Property(e => e.cost)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Reservation>()
                .Property(e => e.guestid)
                .IsFixedLength();
        }
    }
}
