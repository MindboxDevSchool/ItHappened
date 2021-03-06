﻿using System;
using System.Collections.Generic;
using ItHappened.Domain;

namespace ItHappened.Application.Services.EventService
{
    public interface IEventService
    {
        Guid CreateEvent(Guid actorId, Guid trackerId, DateTimeOffset eventHappensDate,
            EventCustomParameters customParameters);

        Event GetEvent(Guid actorId, Guid eventId);
        IReadOnlyCollection<Event> GetAllTrackerEvents(Guid actorId, Guid trackerId);

        void EditEvent(Guid actorId,
            Guid eventId,
            DateTimeOffset timeStamp,
            EventCustomParameters customParameters);

        void DeleteEvent(Guid actorId, Guid eventId);
        IReadOnlyCollection<Event> GetAllFilteredEvents(Guid userId, Guid trackerId, EventFilterData eventFilter);
    }
}