using Domain.Contracts.DTO.Times;
using Domain.Entities;
using Domain.Interfaces.Business;
using Domain.Interfaces.Data;

namespace Domain.Business;
public class TimesBusiness : ITimesBusiness
{
    private readonly ITimesData _timesData;

    public TimesBusiness(ITimesData timesData)
    {
        _timesData = timesData;
    }

    public async Task<List<Times>> CreateDailyTimes(Guid medId, DateOnly start, DateOnly end, List<NewTimeDTO> times)
    {

        List<Times> newTimeList = new List<Times>();
        int timeDifference = end.DayNumber - start.DayNumber + 1;

        for (int i = 0; i < timeDifference; i++)
        {
            var currentDay = start.AddDays(i);

            foreach (var item in times)
            {
                foreach (var time in item.Time)
                {
                    TimeOnly convertedNewTime = TimeOnly.Parse(time);
                    DateTime correctDateTime = currentDay.ToDateTime(convertedNewTime);
                    correctDateTime = DateTime.SpecifyKind(correctDateTime, DateTimeKind.Utc);

                    Console.WriteLine($"==== {correctDateTime} ====");

                    Times newTime = new Times()
                    {

                        MedicationId = medId,
                        DateTime = correctDateTime,
                        IsTaken = false,

                    };

                    newTimeList.Add(newTime);

                }
            }
        }

        await _timesData.CreateTimesAsync(newTimeList);
        return newTimeList;

    }

    public async Task<List<Times>> CreateWeeklyTimes(Guid medId, DateOnly start, DateOnly end, List<NewTimeDTO> times)
    {
        List<Times> newTimes = new List<Times>();
        int timeDifference = end.DayNumber - start.DayNumber + 1;


        for (int i = 0; i < timeDifference; i++)
        {
            var currentDay = start.AddDays(i);

            foreach (var item in times)
            {

                if (item.WeekDay.Contains((int)currentDay.DayOfWeek))
                {

                    foreach (var item1 in item.Time)
                    {
                        TimeOnly convertedNewTime = TimeOnly.Parse(item1);
                        DateTime correctDateTime = currentDay.ToDateTime(convertedNewTime);

                        if (correctDateTime.Year > 2030)
                        {
                            break;
                        }

                        correctDateTime = DateTime.SpecifyKind(correctDateTime, DateTimeKind.Utc);

                        var timeToAdd = new Times()
                        {
                            MedicationId = medId,
                            IsTaken = false,
                            DateTime = correctDateTime,

                        };

                        newTimes.Add(timeToAdd);
                    }

                }

            }

        }
        await _timesData.CreateTimesAsync(newTimes);
        return newTimes;
    }

    public async Task<List<Times>> CreateMonthlyTimes(Guid medId, DateOnly start, DateOnly end, List<NewTimeDTO> times)
    {

        List<Times> newTimes = new List<Times>();
        int timeInterval = end.DayNumber - start.DayNumber + 1;


        foreach (var time in times)
        {

            foreach (var item in time.Dates)
            {

                foreach (var item1 in time.Time)
                {
                    TimeOnly convertedTime = TimeOnly.Parse(item1);
                    var day = item.Day;
                    var month = item.Month;
                    var year = item.Year;

                    for (int i = 0; i < timeInterval; i++)
                    {
                        DateOnly theDate = new DateOnly(year, month, day).AddMonths(i);

                        if (theDate > end)
                        {
                            break;
                        }

                        if (theDate.Year > 2030)
                        {

                            break;
                        }


                        DateTime correctDate = item.ToDateTime(convertedTime);
                        correctDate = DateTime.SpecifyKind(correctDate, DateTimeKind.Utc);

                        var timeToAdd = new Times
                        {
                            DateTime = correctDate,
                            MedicationId = medId,
                            IsTaken = false
                        };

                        newTimes.Add(timeToAdd);

                    }

                }

            }


        }
        await _timesData.CreateTimesAsync(newTimes);
        return newTimes;
    }

    public async Task<List<Times>> CreateYearlyTimes(Guid medId, DateOnly start, DateOnly end, List<NewTimeDTO> times)
    {
        var newTimes = new List<Times>();
        int timeInterval = end.DayNumber - start.DayNumber + 1;
        int count = 0;

        foreach (var time in times)
        {

            foreach (var item in time.Dates)
            {

                foreach (var item1 in time.Time)
                {
                    TimeOnly convertedTime = TimeOnly.Parse(item1);
                    var day = item.Day;
                    var month = item.Month;
                    var year = item.Year;

                    while (year < 2030)
                    {

                        DateOnly theDate = new DateOnly(year, month, day).AddYears(count);

                        if (theDate > end)
                        {
                            break;
                        }


                        DateTime correctDate = theDate.ToDateTime(convertedTime);
                        correctDate = DateTime.SpecifyKind(correctDate, DateTimeKind.Utc);

                        var timeToAdd = new Times
                        {
                            DateTime = correctDate,
                            MedicationId = medId,
                            IsTaken = false
                        };

                        newTimes.Add(timeToAdd);
                        count++;
                    }

                }

            }


        }
        await _timesData.CreateTimesAsync(newTimes);
        return newTimes;
    }
}
