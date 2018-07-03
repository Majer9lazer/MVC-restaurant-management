namespace RestaurantManagement.DAL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EatType")]
    public partial class EatType
    {
        [Key]
        public int TypeId { get; set; }

        [Required]
        [StringLength(50)]
        public string EatTypeName { get; set; }
    }
}
