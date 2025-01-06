using Domain.Contracts.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.DTO;
public class ResponseDTO
{
    public bool Status { get; set; }
    public string? Message { get; set; }
}
