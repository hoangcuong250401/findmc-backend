﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Paging
{
    public class MediaPagedRequest:PagedRequest
    {
        public int? UserId { get; set; }
        public MediaType? MediaType { get; set; }
    }
}
