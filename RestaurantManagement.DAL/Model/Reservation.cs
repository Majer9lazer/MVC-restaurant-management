namespace RestaurantManagement.DAL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Reservation")]
    public partial class Reservation
    {
        public int ReservationId { get; set; }
        [Required(ErrorMessage = "Guest name required")]
        [StringLength(250)]
        public string GuestName { get; set; }
        [Required]
        [StringLength(250)]
        public string Noofguests { get; set; }
        [Required]
        public int? Typeid { get; set; }

        public Guid Guestid { get; set; }

        public virtual EatType EatType { get; set; }
    }
}
