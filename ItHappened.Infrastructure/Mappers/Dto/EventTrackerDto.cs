﻿using System;
using ItHappened.Infrastructure.Mappers;

namespace ItHappened.Infrastructure.Dto
{
    public class EventTrackerDto
    {
        public Guid Id { get; set; }
        public Guid CreatorId { get; set; }
        public string Name { get; set; }
        public string ScaleMeasurementUnit { get; set; }
        public bool IsPhotoRequired { get; set; }
        public bool IsScaleRequired { get; set; }
        public bool IsRatingRequired { get; set; }
        public bool IsGeotagRequired { get; set; }
        public bool IsCommentRequired { get; set; }
        public bool IsCustomizationRequired { get; set; }
        public UserDto UserDto { get; set; }
        public bool IsUpdated { get; set; }
    }
}