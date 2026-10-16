using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaceDay.Api.Data;
using RaceDay.Api.DTOs;
using RaceDay.Api.Helpers;

namespace RaceDay.Api.Controllers
{
    /// <summary>
    /// Participant profile endpoints.
    /// </summary>
    [ApiController]
    [Route("api/participants")]
    public class ParticipantsController : ControllerBase
    {
        private readonly RaceDayContext _context;

        public ParticipantsController(RaceDayContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns the profile of the logged-in Participant.
        /// </summary>
        /// <response code="200">The participant profile.</response>
        /// <response code="401">Not logged in.</response>
        /// <response code="403">Logged in as an Organiser.</response>
        [HttpGet("me")]
        [Authorize(Roles = "Participant")]
        [ProducesResponseType(typeof(ParticipantProfileDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMyProfile()
        {
            int id = User.GetUserId();
            var participant = await _context.Participants.FindAsync(id);

            if (participant == null)
            {
                return NotFound();
            }

            return Ok(ToDto(participant));
        }

        /// <summary>
        /// Updates the profile of the logged-in Participant.
        /// </summary>
        /// <remarks>Expects fullName and phoneNumber.</remarks>
        /// <response code="200">The updated profile.</response>
        /// <response code="400">Invalid details.</response>
        /// <response code="401">Not logged in.</response>
        /// <response code="403">Logged in as an Organiser.</response>
        [HttpPut("me")]
        [Authorize(Roles = "Participant")]
        [ProducesResponseType(typeof(ParticipantProfileDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateMyProfile(UpdateProfileDto dto)
        {
            int id = User.GetUserId();
            var participant = await _context.Participants.FindAsync(id);

            if (participant == null)
            {
                return NotFound();
            }

            participant.FullName = dto.FullName;
            participant.PhoneNumber = dto.PhoneNumber;
            await _context.SaveChangesAsync();

            return Ok(ToDto(participant));
        }

        /// <summary>
        /// Gets a participant's profile (used by an Organiser managing enrolments).
        /// </summary>
        /// <response code="200">The participant profile.</response>
        /// <response code="401">Not logged in.</response>
        /// <response code="403">Logged in as a Participant.</response>
        /// <response code="404">Participant not found.</response>
        [HttpGet("{participantId:int}")]
        [Authorize(Roles = "Organiser")]
        [ProducesResponseType(typeof(ParticipantProfileDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetParticipant(int participantId)
        {
            var participant = await _context.Participants.FindAsync(participantId);

            if (participant == null)
            {
                return NotFound();
            }

            return Ok(ToDto(participant));
        }

        private ParticipantProfileDto ToDto(Models.Participant p)
        {
            return new ParticipantProfileDto
            {
                ParticipantId = p.ParticipantId,
                FullName = p.FullName,
                Email = p.Email,
                PhoneNumber = p.PhoneNumber
            };
        }
    }
}
