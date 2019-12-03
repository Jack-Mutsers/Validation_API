using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class UserDto
    {
        public Guid id { get; set; }
        public string username { get; set; }
        public string password { get; set; }


    }
}