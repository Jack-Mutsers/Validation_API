﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class ValidationForCreationDto
    {
        public Guid access_token { get; set; }
        public Guid user_id { get; set; }
    }
}
