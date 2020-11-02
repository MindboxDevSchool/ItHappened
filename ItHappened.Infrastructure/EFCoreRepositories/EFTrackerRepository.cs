﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ItHappened.Domain;
using ItHappened.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace ItHappened.Infrastructure.EFCoreRepositories
{
    // ReSharper disable once InconsistentNaming
    public class EFTrackerRepository : ITrackerRepository
    {
        private readonly ItHappenedDbContext _context;
        private readonly IMapper _mapper;

        public EFTrackerRepository(ItHappenedDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public void SaveTracker(EventTracker newTracker)
        {
            var trackerDto = _mapper.Map<EventTrackerDto>(newTracker);
            _context.EventTrackers.Add(trackerDto);
        }

        public EventTracker LoadTracker(Guid eventTrackerId)
        {
            var trackerDto = _context.EventTrackers.Find(eventTrackerId);
            return _mapper.Map<EventTracker>(trackerDto);
        }

        public IReadOnlyCollection<EventTracker> LoadAllUserTrackers(Guid userId)
        {
           var trackersDto = _context.EventTrackers.Where(tracker => tracker.CreatorId == userId);
           return _mapper.Map<EventTracker[]>(trackersDto.ToList());
        }

        public void UpdateTracker(EventTracker eventTracker)
        {
            var eventTrackerDto = _mapper.Map<EventTrackerDto>(eventTracker);
            _context.EventTrackers.Update(eventTrackerDto);
        }

        public void DeleteTracker(Guid trackerId)
        {
            //TODO test cascade delete 
            //Deleting without loading from the database has not worked (attach, remove) 
            var tracker = _context.EventTrackers.First(trackerDto => trackerDto.Id == trackerId);
            _context.EventTrackers.Remove(tracker);
        }

        public bool IsContainTracker(Guid trackerId)
        {
            return _context.EventTrackers.Any(o => o.Id == trackerId);
        }
    }
}