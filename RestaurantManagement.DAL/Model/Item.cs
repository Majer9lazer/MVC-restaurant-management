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

        [StringLength(250)]
        public string itemName { get; set; }

        [StringLength(250)]
        public string itemDesc { get; set; }

        [Column(TypeName = "money")]
        public decimal? cost { get; set; }
    }
}
