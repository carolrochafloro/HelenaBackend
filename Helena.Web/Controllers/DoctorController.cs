using Domain.Contracts.DTO;
using Domain.Entities;
using Infra.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Helena.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DoctorController : ControllerBase
    {

        private readonly DoctorData _doctorData;

        public DoctorController(DoctorData doctorData)
        {
            _doctorData = doctorData;
        }

        [Route("create")]
        [HttpPost]
        public async Task<IActionResult> CreateDoctor(NewDoctorDTO newDoctor)
        {
            var userIdString = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            Guid.TryParse(userIdString, out Guid userId);

            try
            {
                if (newDoctor is null)
                {
                    return BadRequest("Os dados do médico devem ser enviados.");
                }

                var doctor = new Doctor
                {
                    Name = newDoctor.Name,
                    Specialty = newDoctor.Specialty,
                    Phone = newDoctor.Phone,
                    Email = newDoctor.Email,
                    UserId = userId,
                };

                var result = await _doctorData.CreateDoctorAsync(doctor);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("get")]
        [HttpGet]
        public IActionResult GetDoctors()

        {
            var userIdString = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            Guid.TryParse(userIdString, out Guid userId);

            try
            {
                var doctors = _doctorData.GetDoctors(userId);
                return Ok(doctors);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("get/{doctorId}")]
        [HttpGet]
        public IActionResult GetDoctorById([FromRoute] Guid doctorId)
        {
            var userIdString = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            Guid.TryParse(userIdString, out Guid userId);

            try
            {
                var doctor = _doctorData.GetDoctorById(doctorId);

                if (doctor.UserId != userId)
                {
                    return Unauthorized("Médico não foi cadastrado por esse usuário");
                }

                return Ok(doctor);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("update/{doctorId}")]
        [HttpPut]
        public async Task<IActionResult> UpdateDoctor([FromRoute] Guid doctorId, [FromBody] NewDoctorDTO doctor)
        {
            try
            {

                var response = await _doctorData.UpdateDoctorAsync(doctorId, doctor);

                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("delete/{doctorId}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteDoctor([FromRoute] Guid doctorId)
        {

            var userIdString = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            Guid.TryParse(userIdString, out Guid userId);

            try
            {
                var doctor = _doctorData.GetDoctorById(doctorId);

                if (doctor.UserId != userId)
                {
                    return Unauthorized("Médico não foi cadastrado por esse usuário");
                }

                var response = await _doctorData.DeleteDoctorAsync(doctorId);

                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
