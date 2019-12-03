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

        [Required(ErrorMessage = "user_id is required")]
        public Guid user_id { get; set; }

        public DateTime Creation_date { get; set; }

        public DateTime expiration_date { get; set; }
    }
}
