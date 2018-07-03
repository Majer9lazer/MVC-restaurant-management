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

        public int itemid { get; set; }

        [Required]
        [StringLength(250)]
        public string itemName { get; set; }

        [Required]
        [StringLength(250)]
        public string itemDesc { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal? cost { get; set; }
    }
}
