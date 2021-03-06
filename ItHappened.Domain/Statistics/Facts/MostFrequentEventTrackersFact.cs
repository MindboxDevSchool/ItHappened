﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ItHappened.Domain.Statistics
{
    public class MostFrequentEventTrackersFact : IMultipleTrackersFact
    {
        public string FactName { get; }
        public string Description { get; }
        public double Priority { get; }
        public string TrackingName { get; }
        public double EventsPeriod { get; }

        internal MostFrequentEventTrackersFact(string factName, string description, double priority,
            string trackingName, double eventsPeriod)
        {
            FactName = factName;
            Description = description;
            Priority = priority;
            TrackingName = trackingName;
            EventsPeriod = eventsPeriod;
        }
    }
}