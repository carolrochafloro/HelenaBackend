using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.DTO.Times;
public class TimeDTO
{
    public Guid Id { get; set; }
    public DateTime DateTime { get; set; }
    public bool IsTaken { get; set; }
}
