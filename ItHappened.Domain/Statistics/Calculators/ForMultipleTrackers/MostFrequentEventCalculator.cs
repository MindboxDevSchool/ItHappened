﻿using System;
using System.Collections.Generic;
using System.Linq;
using ItHappened.Domain.Statistics.Facts.ForMultipleTrackers;
using LanguageExt;

namespace ItHappened.Domain.Statistics.Calculators.ForMultipleTrackers
{
    public class MostFrequentEventCalculator : IMultipleTrackersStatisticsCalculator<MostFrequentEvent>
    {
        public Option<IMultipleTrackersStatisticsFact> Calculate(IEnumerable<EventTracker.EventTracker> eventTrackers)
        {
            var enumerable = eventTrackers as EventTracker.EventTracker[] ?? eventTrackers.ToArray();
            if (!CanCalculate(enumerable))
                return Option<IMultipleTrackersStatisticsFact>.None;

            var (eventTrackerName, eventsPeriod) = enumerable
                .Select(et => (et.Name, GetEventsPeriod(et)))
                .OrderBy(x => x.Item2)
                .First();

            var description = $"Чаще всего у вас происходит событие {eventTrackerName} - раз в {eventsPeriod} дней";
            var priority = 10 / eventsPeriod;
            
            return Option<IMultipleTrackersStatisticsFact>.Some(new MostFrequentEvent(description,
                priority,
                eventTrackerName,
                eventsPeriod));
        }

        private bool CanCalculate(IEnumerable<EventTracker.EventTracker> eventTrackers)
        {
            return eventTrackers.Count() > 1 &&
                   eventTrackers.Count(et => et.Events.Count > 3) > 1;
        }

        private double GetEventsPeriod(EventTracker.EventTracker eventTracker)
        {
            var events = eventTracker.Events;
            return (DateTime.Now - events
                .OrderBy(e => e.HappensDate)
                .First().HappensDate).Days / events.Count();
        }
    }
}