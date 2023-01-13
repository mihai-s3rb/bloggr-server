﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Infrastructure.Models.Blobs
{
    public class BlobDto
    {
        public string? Uri { get; set; }

        public string? Name { get; set; }

        public string? ContentType { get; set; }

        public Stream? Content { get; set; }
    }
}
