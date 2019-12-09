using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class ValidationForUpdateDto
    {

        [Required(ErrorMessage = "access token is required")]
        public Guid access_token { get; set; }

        [Required(ErrorMessage = "user id is required")]
        public Guid user_id { get; set; }

        [Required(ErrorMessage = "creation date is required")]
        public DateTime creation_date { get; set; }

        [Required(ErrorMessage = "expiration date is required")]
        public DateTime expiration_date { get; set; }
    }
}
