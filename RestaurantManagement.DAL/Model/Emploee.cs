namespace RestaurantManagement.DAL.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Emploee")]
    public class Emploee
    {
        [Key]
        public int Empid { get; set; }

        [StringLength(50)]
        public string Fullname { get; set; }

        public DateTime? Dob { get; set; }

        [StringLength(50)]
        public string Education { get; set; }

        public byte? Experience { get; set; }

        [StringLength(10)]
        public string Phonenumber { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(1000)]
        public string Skills { get; set; }
        [StringLength(100)]
        public string Username { get; set; }

        [StringLength(50)]
        public string Emppassword { get; set; }

        public int? Jobid { get; set; }

        public virtual Job Job { get; set; }
    }
}
