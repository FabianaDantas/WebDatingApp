
using System;

namespace WebDatingApp.API.Domain.DTOs
{
    public class PhotosForDetailedDTO
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateAdd { get; set; }
        public bool IsMain { get; set; }
    }
}