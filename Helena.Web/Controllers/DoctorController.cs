﻿using Domain.Contracts.DTO.AppUser;
using Domain.Contracts.DTO.Doctor;
using Domain.Entities;
using Domain.Interfaces.Data;
using Infra.Data.Data;
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

        private readonly IDoctorData _doctorData;

        public DoctorController(IDoctorData doctorData)
        {
            _doctorData = doctorData;
        }

        [Route("create")]
        [HttpPost]
        public async Task<IActionResult> CreateDoctor([FromBody] NewDoctorDTO newDoctor)
        {

            try
            {
                if (newDoctor is null)
                {
                    return BadRequest("Os dados do médico devem ser enviados.");
                }

                if (!Guid.TryParse(newDoctor.UserId, out Guid userId))
                {
                    return BadRequest("ID de usuário inválido.");
                }

                var doctor = new Doctor
                {
                    Name = newDoctor.Name,
                    Specialty = newDoctor.Specialty,
                    Contact = newDoctor.Contact,
                    UserId = userId
                };

                var result = await _doctorData.CreateDoctorAsync(doctor);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao criar o médico: {ex.Message}");
            }
        }

        [Route("get")]
        [HttpPost]
        public IActionResult GetDoctors([FromBody] UserIdRequestDTO id)

        {
            string stringId = id.UserId;

            if (!Guid.TryParse(stringId, out Guid userId))
            {
                return BadRequest("ID de usuário inválido.");
            }

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
            try
            {
                var doctor = _doctorData.GetDoctorById(doctorId);

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
            try
            {
                var doctor = _doctorData.GetDoctorById(doctorId);

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
