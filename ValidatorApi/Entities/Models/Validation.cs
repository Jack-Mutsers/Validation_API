using Entities.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    [Table("validation")]
    public class Validation
    {
        [Key]
        public Guid access_token { get; set; }

        [Column("user_id")]
        [Required(ErrorMessage = "user id is required")]
        public Guid userId { get; set; }

        public DateTime Creation_date { get; set; }

        public DateTime expiration_date { get; set; }

        public UserForTransfer user { get; set; }
    }
}
