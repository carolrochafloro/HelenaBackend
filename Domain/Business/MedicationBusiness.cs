using Domain.Contracts.DTO.Medication;
using Domain.Contracts.DTO.Times;
using Domain.Contracts.Enum;
using Domain.Entities;
using Domain.Interfaces.Business;
using Domain.Interfaces.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Business;
public class MedicationBusiness : IMedicationBusiness
{
    private readonly ITimesBusiness _timesBusiness;
    private readonly ILogger<MedicationBusiness> _logger;
    private readonly IMedicationData _medData;
    private readonly ITimesData _timesData;

    public MedicationBusiness(ITimesBusiness timesBusiness,
                              ILogger<MedicationBusiness> logger,
                              IMedicationData medData,
                              ITimesData timesData)
    {
        _timesBusiness = timesBusiness;
        _logger = logger;
        _medData = medData;
        _timesData = timesData;
    }

    public async Task<Medication> CreateMedicationWithTimes(NewMedicationDTO newMedication, Guid userId)
    {
        var result = Guid.TryParse(newMedication.DoctorId, out Guid doctorId);



        var medication = new Medication
        {
            Name = newMedication.Name,
            Lab = newMedication.Lab,
            Type = newMedication.Type,
            Dosage = newMedication.Dosage,
            Notes = newMedication.Notes,
            StartDate = DateOnly.Parse(newMedication.Start),
            EndDate = DateOnly.Parse(newMedication.End),
            IndicatedFor = newMedication.IndicatedFor,
            FrequencyType = (FrequencyTypeEnum)newMedication.FrequencyType,
            Recurrency  = newMedication.Recurrency,
            DoctorId = doctorId,
            UserId = userId
        };

        List<NewTimeDTO> newMedTimes = newMedication.Times;
        List<Times> timesToAdd = new List<Times>();

        await _medData.CreateMedicationAsync(medication);

        switch (newMedication.FrequencyType)
        {
            case FrequencyTypeEnum.Daily:
                timesToAdd = await _timesBusiness.CreateDailyTimes(medication.Id, medication.StartDate, medication.EndDate, newMedTimes);
                break;

            case FrequencyTypeEnum.Weekly:
                timesToAdd = await _timesBusiness.CreateWeeklyTimes(medication.Id, medication.StartDate, medication.EndDate, newMedTimes);
                break;

            case FrequencyTypeEnum.Monthly:
                timesToAdd = await _timesBusiness.CreateMonthlyTimes(medication.Id, medication.StartDate, medication.EndDate, newMedTimes);
                break;

            case FrequencyTypeEnum.Yearly:
                timesToAdd = await _timesBusiness.CreateYearlyTimes(medication.Id, medication.StartDate, medication.EndDate, newMedTimes);
                break;
        }

        return medication;
    }
}
