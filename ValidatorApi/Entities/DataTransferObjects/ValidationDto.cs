using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class ValidationDto
    {
        public Guid access_token { get; set; }
        public Guid user_id { get; set; }
        public DateTime creation_date { get; set; }
        public DateTime expiration_date { get; set; }
    }
}
