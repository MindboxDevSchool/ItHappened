﻿namespace ItHappened.Domain.Statistics
{
    public class EventsCountFact : IMultipleTrackersStatisticsFact
    {
        public EventsCountFact(string factName, string description, double priority, int eventsCount)
        {
            FactName = factName;
            Description = description;
            Priority = priority;
            EventsCount = eventsCount;
        }

        public string FactName { get; }
        public string Description { get; }
        public double Priority { get; }
        public int EventsCount { get; }
    }
}