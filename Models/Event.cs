using System.ComponentModel.DataAnnotations;

namespace DayCareProject.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public string PhotoPath { get; set; } // Stores the image path
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }

}




