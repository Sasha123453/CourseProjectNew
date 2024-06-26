﻿using CourseDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectDb.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public DaysOfWeek DepartureDays { get; set; }
        public Airplane Airplane { get; set; }
        public Brigade Brigade { get; set; }
        public FlightType FlightType { get; set; }
        public List<Ticket> Tickets { get; set; }
        public DateTime? DelayStartTime { get; set; }
        public DateTime? DelayEndTime { get; set; }
        public List<FlightPath> FlightPaths { get; set; }
        public FlightStatus FlightStatus { get; set; }
        public string? DelayReason { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
    }

}
