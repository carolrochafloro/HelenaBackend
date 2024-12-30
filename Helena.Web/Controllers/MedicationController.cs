using Domain.Contracts.DTO.Medication;
using Domain.Contracts.DTO.Requests_and_Responses;
using Domain.Entities;
using Domain.Interfaces.Business;
using Domain.Interfaces.Data;
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
    public class MedicationController : ControllerBase
    {

        private readonly IMedicationData _medData;
        private readonly IMedicationBusiness _medBusiness;
        private readonly ILogger<MedicationController> _logger;

        public MedicationController(IMedicationData medData,
                                    IMedicationBusiness medBusiness,
                                    ILogger<MedicationController> logger)
        {
            _medData = medData;
            _medBusiness = medBusiness;
            _logger = logger;
        }

        [HttpGet]
        [Route("get/all/{userId}")]
        public IActionResult GetAllMedications([FromRoute] Guid userId)
        {

            try
            {
                var medications = _medData.GetAllMedications(userId);
              foreach (var item in medications)
                {
                    _logger.LogDebug($"==== {item.Name} ====");
                }
                return Ok(medications);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("get/{id}")]
        public IActionResult GetMedication([FromRoute] Guid id)
        {
            try
            {
                var medication = _medData.GetMedicationById(id);

                return Ok(medication);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("get/date")]
        public IActionResult GetMedicationByDate([FromBody] MedicationByDayRequest request)
        {
            var medication = _medData.GetAllMedicationsByDate(request.Date, request.UserId);
            return Ok(medication);
        }

        [HttpPost]
        public async Task<IActionResult> NewMedication([FromBody] NewMedicationDTO newMedication)
        {

            var result  = Guid.TryParse(newMedication.UserId, out Guid userId);

            try
            {
                var medication = await _medBusiness.CreateMedicationWithTimes(newMedication,userId);

                return Ok(medication);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateMedication([FromBody] UpdateMedDTO updateMedication, [FromRoute] Guid id)
        {
            try
            {
                var medication = await _medData.UpdateMedicationAsync(updateMedication, id);
                return Ok(medication);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteMedication([FromRoute] Guid id)
        {
            try
            {
                var response = await _medData.DeleteMedicationAsync(id);

                return Ok(response.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
