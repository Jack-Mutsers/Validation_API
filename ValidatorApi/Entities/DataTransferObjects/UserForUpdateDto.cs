using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class UserForUpdateDto
    {
        public bool active = true;
        public string username { get; set; }

        [Required(ErrorMessage = "password is required")]
        public string password { get; set; }
    }
}
