﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASample.Service.Models
{
    public class ServiceRequest
    {
        public string LicenseKey { get; set; }
   
        public Guid Id { get; set; }
        public string Department { get; set; }
    }
}
