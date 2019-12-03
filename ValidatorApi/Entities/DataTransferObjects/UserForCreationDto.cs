using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class UserForCreationDto
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(60, ErrorMessage = "Username can't be longer than 60 characters")]
        public string username { get; set; }

        [Required(ErrorMessage = "password is required")]
        public string password { get; set; }
    }
}