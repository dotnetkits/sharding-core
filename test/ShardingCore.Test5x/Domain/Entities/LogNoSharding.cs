﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShardingCore.Test5x.Domain.Entities
{
    public class LogNoSharding
    {
        public string Id { get; set; }
        public string Body { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
