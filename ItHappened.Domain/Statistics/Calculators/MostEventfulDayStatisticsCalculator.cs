﻿using System.Collections.Generic;
using System.Linq;
using LanguageExt;

namespace ItHappened.Domain.Statistics
{
    public class MostEventfulDayStatisticsCalculator : IMultipleTrackersStatisticsCalculator
    {
        private const int ThresholdEventAmount = 1;

        public Option<IMultipleTrackerTrackerFact> Calculate(
            IReadOnlyCollection<TrackerWithItsEvents> trackerWithItsEvents)
        {
            var allTrackersEvents = trackerWithItsEvents.SelectMany(info => info.Events).ToList();
            if (!CanCalculate(allTrackersEvents))
            {
                return Option<IMultipleTrackerTrackerFact>.None;
            }

            var dayWithBiggestEventCount = allTrackersEvents
                .GroupBy(@event => @event.HappensDate,
                    (date, g) => new
                    {
                        Date = date,
                        Count = g.Count()
                    }).OrderByDescending(g => g.Count).First();
            var dayWithLargestEventCount = dayWithBiggestEventCount.Date;
            var eventsCount = dayWithBiggestEventCount.Count;
            var ruEventName = RuEventName(eventsCount, "событие", "события", "событий");
            return Option<IMultipleTrackerTrackerFact>.Some(new MostEventfulDayTrackerTrackerFact(
                "Самый насыщенный событиями день",
                $"Самый насыщенный событиями день был {dayWithLargestEventCount:d}. Тогда произошло {eventsCount} {ruEventName}",
                1.5 * eventsCount,
                dayWithLargestEventCount,
                eventsCount
            ));
        }

        private static string RuEventName(int number, string nominativ, string genetiv, string plural)
        {
            var titles = new[] {nominativ, genetiv, plural};
            var cases = new[] {2, 0, 1, 1, 1, 2};
            return titles[number % 100 > 4 && number % 100 < 20 ? 2 : cases[number % 10 < 5 ? number % 10 : 5]];
        }

        private static bool CanCalculate(IReadOnlyCollection<Event> events)
        {
            return events.Count > ThresholdEventAmount;
        }
    }
}