namespace RestaurantManagement.DAL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Item")]
    public partial class Item
    {
        public int Itemid { get; set; }

        [Required]
        [StringLength(250)]
        public string ItemName { get; set; }

        [Required]
        [StringLength(250)]
        public string ItemDesc { get; set; }

        [Column(TypeName = "money")]
        public decimal Cost { get; set; }
    }
}
