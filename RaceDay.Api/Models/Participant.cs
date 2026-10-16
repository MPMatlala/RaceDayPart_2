using System.ComponentModel.DataAnnotations;

namespace RaceDay.Api.Models
{//start of namespace
    public class Participant
    {
        public int ParticipantId { get; set; }

        [Required, MaxLength(100)]
        public string FullName { get; set; }

        [Required, MaxLength(150)]
        public string Email { get; set; }

        [Required, MaxLength(255)]
        public string PasswordHash { get; set; }

        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<Enrolment> Enrolments { get; set; } = new List<Enrolment>();
    }
}
