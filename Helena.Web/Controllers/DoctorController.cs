﻿using Domain.Contracts.DTO;
using Domain.Entities;
using Infra.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Helena.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {

        private readonly DoctorData _doctorData;

        public DoctorController(DoctorData doctorData)
        {
            _doctorData = doctorData;
        }

        [Route("newdoctor")]
        [HttpPost]
        public async Task<IActionResult> CreateDoctor(NewDoctorDTO newDoctor)
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
            };

            var result = await _doctorData.CreateDoctorAsync(doctor);

            return Ok(result);
        }

        [Route("getdoctors")]
        [HttpGet]
        public IActionResult GetDoctors()
        {

            return Ok();

        }



    }
}