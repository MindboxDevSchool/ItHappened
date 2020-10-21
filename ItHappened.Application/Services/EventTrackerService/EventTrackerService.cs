using System;
using System.Collections.Generic;
using System.Linq;
using ItHappened.Domain;
using Serilog;
using Status = ItHappened.Application.Services.EventTrackerService.EventTrackerServiceStatusCodes;

namespace ItHappened.Application.Services.EventTrackerService
{
    public class EventTrackerService : IEventTrackerService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IEventTrackerRepository _eventTrackerRepository;

        public EventTrackerService(IEventTrackerRepository eventTrackerRepository, IEventRepository eventRepository)
        {
            _eventTrackerRepository = eventTrackerRepository;
            _eventRepository = eventRepository;
        }

        public Status DeleteTracker(Guid trackerId, Guid creatorId)
        {
            if (!_eventTrackerRepository.IsContainTracker(trackerId))
            {
                Log.Information(
                    $"Tracker with id: {trackerId} doesn't exist in repository");
                return Status.TrackerDontExist;
            }
            var tracker = _eventTrackerRepository.LoadTracker(trackerId);
            if (creatorId != tracker.CreatorId)
            {
                Log.Information(
                    $"Can't remove tracker with trackerId={trackerId} userId={creatorId}. TrackerCreatorId does not match");
                return Status.WrongInitiatorId;
            }

            Log.Information($"Tracker deleted with trackerId={trackerId} userId={creatorId}");
            return Status.Ok;;
        }

        public Guid CreateTracker(EventTracker tracker)
        {
            _eventTrackerRepository.SaveTracker(tracker);
            return tracker.Id;
        }

        public IReadOnlyCollection<EventTracker> GetAllUserTrackers(Guid userId)
        {
            return _eventTrackerRepository.LoadAllUserTrackers(userId).ToList();
        }

        public Status AddEventToTracker(Guid initiatorId, Guid trackerId, Event @event)
        {
            if (_eventRepository.IsContainEvent(@event.Id))
            {
                Log.Information(
                    $"Event with id: {@event.Id} already exists in repository");
                return Status.EventAlreadyExist;
            }
            if (!_eventTrackerRepository.IsContainTracker(trackerId))
            {
                Log.Information(
                    $"Tracker with id: {trackerId} doesn't exist in repository");
                return Status.TrackerDontExist;
            }
            
            var tracker = _eventTrackerRepository.LoadTracker(trackerId);
            if (initiatorId != tracker.CreatorId)
            {
                Log.Information(
                    $"Can't add event to tracker id: {trackerId}. TrackerCreatorId does not match creator id: {initiatorId}");
                return Status.WrongInitiatorId;
            }

            if (tracker.CreatorId != @event.CreatorId)
            {
                Log.Information(
                    $"Can't add event {@event.Id} to tracker id: {trackerId}. Event creator id does not match tracker creator id");
                return Status.WrongEventCreatorId;
            }

            if (!tracker.IsTrackerAndEventCustomizationsMatch(@event))
            {
                Log.Information(
                    $"Can't add event {@event.Id} to tracker id: {trackerId}. Tracker and event customizations don't match.");
                return Status.WrongEventCustomisation;
            }

            _eventRepository.AddEvent(@event);
            Log.Information(
                $"Event: {@event.Id} added to tracker: {trackerId}");
            return Status.Ok;
        }

        public Status RemoveEventFromTracker(Guid initiatorId, Guid trackerId, Guid eventId)
        {
            if (!_eventTrackerRepository.IsContainTracker(trackerId))
            {
                Log.Information(
                    $"Tracker with id: {trackerId} doesn't exist in repository");
                return Status.TrackerDontExist;
            }
            
            if (!_eventRepository.IsContainEvent(eventId))
            {
                Log.Information(
                    $"Event with id: {eventId} doesn't exist in repository");
                return Status.EventDontExist;
            }
            
            var tracker = _eventTrackerRepository.LoadTracker(trackerId);
            if (initiatorId != tracker.CreatorId)
            {
                Log.Information(
                    $"Can't remove event from tracker id: {trackerId}. TrackerCreatorId does not match initiator id: {initiatorId}");
                return Status.WrongInitiatorId;
            }
            
            var eventToRemove = _eventRepository.LoadEvent(eventId);
            if (tracker.CreatorId != eventToRemove.CreatorId)
            {
                Log.Information(
                    $"Can't remove event {eventId} from tracker id: {trackerId}. Event creator id does not match tracker creator id");
                return Status.WrongEventCreatorId;
            }
            
            _eventRepository.DeleteEvent(eventId);
            Log.Information(
                $"Event was removed from tracker.  trackerId={trackerId}  eventId={eventId} userId={initiatorId}");
            return Status.Ok;
        }

        public Status EditEventInTracker(Guid initiatorId, Guid trackerId, Event editedEvent)
        {
            if (!_eventTrackerRepository.IsContainTracker(trackerId))
            {
                Log.Information(
                    $"Tracker with id: {trackerId} doesn't exist in repository");
                return Status.TrackerDontExist;
            }
            var tracker = _eventTrackerRepository.LoadTracker(trackerId);
            
            if (initiatorId != tracker.CreatorId)
            {
                Log.Information(
                    $"Can't edit event from tracker id: {trackerId}. TrackerCreatorId does not match initiator id: {initiatorId}");
                return Status.WrongInitiatorId;
            }

            if (!_eventRepository.IsContainEvent(editedEvent.Id))
            {
                Log.Information(
                    $"Event with id: {editedEvent.Id} doesn't exist in repository");
                return Status.EventDontExist;
            }
            _eventRepository.DeleteEvent(editedEvent.Id);
            _eventRepository.AddEvent(editedEvent);
            Log.Information(
                $"Event Added trackerId={trackerId} userId={initiatorId} eventId={editedEvent.Id}");
            return Status.Ok;
        }

        public (IReadOnlyCollection<Event> collection, Status statusCode) GetAllEventsFromTracker(Guid trackerId, Guid initiatorId)
        {
            if (!_eventTrackerRepository.IsContainTracker(trackerId))
            {
                Log.Information(
                    $"Tracker with id: {trackerId} doesn't exist in repository");
                return (new List<Event>(), Status.TrackerDontExist);
            }
            var tracker = _eventTrackerRepository.LoadTracker(trackerId);
            if (initiatorId != tracker.CreatorId)
            {
                Log.Information(
                    $"Can't edit event from tracker id: {trackerId}. TrackerCreatorId does not match initiator id: {initiatorId}");
                return (new List<Event>(), Status.WrongInitiatorId);
            }
            
            Log.Information($"Returned events from trackerId={trackerId}");
            return (_eventRepository.LoadAllTrackerEvents(trackerId), Status.Ok);
        }
    }
}

