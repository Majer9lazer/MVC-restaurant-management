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

        [StringLength(250)]
        public string guestName { get; set; }

        [StringLength(250)]
        public string noofguests { get; set; }

        public int? typeid { get; set; }

        [StringLength(10)]
        public string guestid { get; set; }
    }
}
