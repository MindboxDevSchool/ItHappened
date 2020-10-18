﻿using ItHappened.Domain.Statistics;
using LanguageExt;

namespace ItHappened.UnitTests.StatisticsCalculatorsTests
{
    public static class ConvertorExtensions
    {
        public static Option<T> ConvertTo<T>(this Option<ISingleTrackerStatisticsFact> fact)
        {
            return fact.Map(f => (T)f);
        }
    }
}