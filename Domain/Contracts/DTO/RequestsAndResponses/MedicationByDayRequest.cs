using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.DTO.Requests_and_Responses;
public class MedicationByDayRequest
{
    public DateOnly Date { get; set; }
    public Guid UserId { get; set; }
}
