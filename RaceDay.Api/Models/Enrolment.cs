using System.ComponentModel.DataAnnotations;

namespace RaceDay.Api.Models
{//start of namespace
    public class Enrolment
    {//start of class
        public int EnrolmentId { get; set; }

        public int ParticipantId { get; set; }

        public int CategoryId { get; set; }

        public DateTime EnrolmentDate { get; set; } = DateTime.Now;

        [Required, MaxLength(20)]
        public string RaceNumber { get; set; }

        [Required, MaxLength(20)]
        public string Status { get; set; } = "Pending";   // Pending, Confirmed, Cancelled

        public Participant Participant { get; set; }
        public Category Category { get; set; }
        public Result Result { get; set; }
    }//end of class
}//end of namespace
