﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.DTO.Times;
public class NewTimeDTO
{
    public List<int> WeekDay { get; set; }
    public List<DateOnly> Dates { get; set; }
    public List<string> Time { get; set; }

}
