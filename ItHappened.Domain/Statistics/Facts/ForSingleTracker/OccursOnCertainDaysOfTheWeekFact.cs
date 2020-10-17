﻿using System;
using System.Collections.Generic;
using ItHappened.Domain.Statistics;

namespace ItHappend.Domain.Statistics
{
    public class OccursOnCertainDaysOfTheWeekFact : ISingleTrackerStatisticsFact
    {
        public string FactName { get; }
        public string Description { get; }
        public double Priority { get; }
        public IEnumerable<DayOfWeek> DaysOfTheWeek { get; }
        public double Percentage { get; }
        
        internal OccursOnCertainDaysOfTheWeekFact(string factName, string description, double priority, IEnumerable<DayOfWeek> daysOfTheWeek, double percentage)
        {
            FactName = factName;
            Description = description;
            Priority = priority;
            DaysOfTheWeek = daysOfTheWeek;
            Percentage = percentage;
        }
    }
}