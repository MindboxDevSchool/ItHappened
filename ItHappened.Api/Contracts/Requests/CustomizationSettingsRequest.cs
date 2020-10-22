﻿namespace ItHappened.Api.Contracts.Requests
{
    public class CustomizationSettingsRequest
    {
        public bool PhotoIsOptional { get; set; }
        public bool ScaleIsOptional { get; set; }
        public bool RatingIsOptional { get; set; }
        public bool GeoTagIsOptional { get; set; }
        public bool CommentIsOptional { get; set; }
    }
}