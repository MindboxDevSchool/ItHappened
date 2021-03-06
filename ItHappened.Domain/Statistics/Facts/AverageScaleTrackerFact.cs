﻿namespace ItHappened.Domain.Statistics
{
    public class AverageScaleTrackerFact : ISingleTrackerFact
    {
        public string FactName { get; }
        public string Description { get; }
        public double Priority { get; }
        public double AverageValue { get; }
        public string MeasurementUnit { get; }

        public AverageScaleTrackerFact(string factName, string description, double priority, double averageValue,
            string measurementUnit)
        {
            FactName = factName;
            Description = description;
            Priority = priority;
            MeasurementUnit = measurementUnit;
            AverageValue = averageValue;
        }
    }
}