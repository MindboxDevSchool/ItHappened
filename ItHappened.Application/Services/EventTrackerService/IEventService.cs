﻿using System;
using System.Collections.Generic;
using ItHappened.Domain;

namespace ItHappened.Application.Services.EventTrackerService
{
    public interface IEventService
    {
        Event AddEvent(Guid actorId, Guid trackerId, DateTimeOffset timeStamp, EventCustomParameters customParameters);
        Event GetEvent(Guid actorId, Guid eventId);
        IReadOnlyCollection<Event> GetAllEvents(Guid actorId, Guid trackerId);

        Event EditEvent(Guid actorId,
            Guid eventId,
            DateTimeOffset timeStamp,
            EventCustomParameters customParameters);

        Event DeleteEvent(Guid actorId, Guid eventId);
    }
}