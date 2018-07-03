using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestaurantManagement.DAL.Lang;

namespace RestaurantManagement.DAL.Model
{
    [Table("Comment")]
    public class Comment
    {
        public int CommentId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resource),ErrorMessageResourceName = "ErrComment")]
        [StringLength(250)]
        public string CommentText { get; set; }

    }
}
