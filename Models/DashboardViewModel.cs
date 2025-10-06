using System.Collections.Generic;

namespace DayCareProject.Models
{
    public class DashboardViewModel
    {
        public IEnumerable<Event> Events { get; set; }
        public IEnumerable<ChildApplication> Applications { get; set; }
    }
}
