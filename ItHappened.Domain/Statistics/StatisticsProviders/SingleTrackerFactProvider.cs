﻿using System.Collections.Generic;
using System.Linq;

namespace ItHappened.Domain.Statistics
{
    public class SingleTrackerFactProvider : ISingleTrackerFactProvider
    {
        private readonly List<ISingleTrackerStatisticsCalculator> _calculators =
            new List<ISingleTrackerStatisticsCalculator>();

        private readonly List<ISingleTrackerStatisticsCalculator> _eventRepository;
        private readonly List<ISingleTrackerStatisticsCalculator> _trackerRepository;

        public void Add(ISingleTrackerStatisticsCalculator calculator)
        {
            _calculators.Add(calculator);
        }

        public IReadOnlyCollection<ISingleTrackerFact> GetFacts(IReadOnlyCollection<Event> events, EventTracker tracker)
        {
            return _calculators
                .Select(calculator => calculator.Calculate(events, tracker))
                .Somes()
                .OrderByDescending(fact => fact.Priority)
                .ToList();
        }
    }
}