﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GT.TripManagementService.Domain.Models.TripModel
{
    public class TripServiceViewModel
    {
        public Guid? Id { get; set; }
        public Guid ServiceId { get; set; }
        public string ServiceName{ get; set; }
        public int Quantity { get; set; }
        public DateTime StartInDatetime { get; set; }
        public DateTime EndInDatetime { get; set; }
        public string? Note { get; set; }
    }
}
