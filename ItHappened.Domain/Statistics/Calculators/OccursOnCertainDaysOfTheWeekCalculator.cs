﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ItHappend.Domain.Statistics;
using LanguageExt;

namespace ItHappened.Domain.Statistics
{
    public class OccursOnCertainDaysOfTheWeekCalculator : ISingleTrackerStatisticsCalculator
    {
        private const int MinEvents = 7;
        private const double PriorityCoefficient = 0.14;
        private const double LessNotPassPercent = 0.25;

        private readonly IEventRepository _eventRepository;
        
        public OccursOnCertainDaysOfTheWeekCalculator(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }
        
        public Option<ISingleTrackerFact> Calculate(EventTracker eventTracker)
        {
            if (!CanCalculate(_eventRepository.LoadAllTrackerEvents(eventTracker.Id).ToList()))
            {
                return Option<ISingleTrackerFact>.None;
            }
            var events = _eventRepository.LoadAllTrackerEvents(eventTracker.Id);
            var totalEvents = events.Count;
            var daysOfTheWeek = events.GroupBy(@event => @event.HappensDate.DayOfWeek,
                    (key, group) => new
                    {
                        DayTime = key,
                        HitOnDayOfWeek = group.Count()
                    })
                .Where(groupDays => groupDays.HitOnDayOfWeek > LessNotPassPercent * totalEvents).ToList();

            var amountEventsMoreThenPassPercent = daysOfTheWeek.Sum(day => day.HitOnDayOfWeek);
            var ruDaysOfWeek = GetRuDaysOfWeek(daysOfTheWeek.Select(x => x.DayTime));
            var percentage = 100.0d * amountEventsMoreThenPassPercent / totalEvents;

            return Option<ISingleTrackerFact>.Some(new OccursOnCertainDaysOfTheWeekFact(
                "Происходит в определённые дни недели",
                $"В {percentage}% случаев событие {eventTracker.Name} происходит {ruDaysOfWeek}",
                percentage * PriorityCoefficient,
                daysOfTheWeek.Select(x => x.DayTime),
                percentage
            ));
        }

        private static bool CanCalculate(IList<Event> events)
        {
            if (events.Count <= MinEvents) return false;

            var totalEvents = events.Count;
            return events.GroupBy(@event => @event.HappensDate.DayOfWeek,
                    (key, g) => new
                    {
                        DayTime = key,
                        HitOnDayOfWeek = g.Count() >= totalEvents * LessNotPassPercent
                    })
                .Any(groupDays => groupDays.HitOnDayOfWeek);
        }

        private static string GetRuDaysOfWeek(IEnumerable<DayOfWeek> dayOfTheWeek)
        {
            var result = new StringBuilder();
            foreach (var day in dayOfTheWeek)
            {
                var ruName = GetRuNameDayOfWeek(day);
                result.Append($"{ruName}, ");
            }

            result.Remove(result.Length - 2, 2); //delete last comma
            return result.ToString();
        }

        private static string GetRuNameDayOfWeek(DayOfWeek dayOfTheWeek)
        {
            var ruDayOfTime = dayOfTheWeek switch
            {
                DayOfWeek.Sunday => "в воскресенье",
                DayOfWeek.Monday => "в понедельник",
                DayOfWeek.Tuesday => "во вторник",
                DayOfWeek.Wednesday => "в среду",
                DayOfWeek.Thursday => "в четверг",
                DayOfWeek.Friday => "в пятницу",
                DayOfWeek.Saturday => "в субботу",
                _ => throw new ArgumentException("Unreachable statement")
            };
            return ruDayOfTime;
        }
    }
}