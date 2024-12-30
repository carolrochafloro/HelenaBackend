using Domain.Contracts.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Data;
public interface ITimesData
{
    Task<ResponseDTO> CreateTimesAsync(List<Times> times);
}
