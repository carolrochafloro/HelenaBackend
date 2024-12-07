using Domain.Contracts.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Business;
public interface ITimesBusiness
{
    List<Times> CreateDailyTimes(Guid medId, DateOnly start, DateOnly end, List<NewTimeDTO> times);
    List<Times> CreateWeeklyTimes(Guid medId, DateOnly start, DateOnly end, List<NewTimeDTO> times);
    List<Times> CreateMonthlyTimes(Guid medId, DateOnly start, DateOnly end, List<NewTimeDTO> times);
    List<Times> CreateYearlyTimes(Guid medId, DateOnly start, DateOnly end, List<NewTimeDTO> times);

}
