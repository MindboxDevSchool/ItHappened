﻿using System;

namespace ItHappened.Domain.Statistics
{
    public class MostEventfulWeekTrackerTrackerFact : IMultipleTrackerTrackerFact
    {
        public string FactName { get; }
        public string Description { get; }
        public double Priority { get; }
        public DateTimeOffset WeekWithLargestEventCountFirstDay { get; }
        public DateTimeOffset WeekWithLargestEventCountLastDay { get; }
        public int EventsCount { get; }

        internal MostEventfulWeekTrackerTrackerFact(string type, string description, double priority,
            DateTimeOffset weekWithLargestEventCountFirstDay, DateTimeOffset weekWithLargestEventCountLastDay,
            int eventsCount)
        {
            FactName = type;
            Description = description;
            Priority = priority;
            WeekWithLargestEventCountFirstDay = weekWithLargestEventCountFirstDay;
            WeekWithLargestEventCountLastDay = weekWithLargestEventCountLastDay;
            EventsCount = eventsCount;
        }
    }
}