using Domain.Contracts.DTO.Times;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Business;
public interface ITimesBusiness
{
    Task<List<Times>> CreateDailyTimes(Guid medId, DateOnly start, DateOnly end, List<NewTimeDTO> times);
    Task<List<Times>> CreateWeeklyTimes(Guid medId, DateOnly start, DateOnly end, List<NewTimeDTO> times);
    Task<List<Times>> CreateMonthlyTimes(Guid medId, DateOnly start, DateOnly end, List<NewTimeDTO> times);
    Task<List<Times>> CreateYearlyTimes(Guid medId, DateOnly start, DateOnly end, List<NewTimeDTO> times);

}
