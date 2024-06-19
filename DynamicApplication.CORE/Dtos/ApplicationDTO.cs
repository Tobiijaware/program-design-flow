﻿namespace DynamicApplication.CORE.Dtos
{
    public class ApplicationDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Nationality { get; set; }
        public string? CurrentResidence { get; set; }
        public string? IdNumber { get; set; }
        public string? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public List<QuestionDTO>? Questions { get; set; }
    }

    
}