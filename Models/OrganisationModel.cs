﻿namespace Models
{
    public class OrganisationModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string? Website { get; set; } = null;
    }
}
