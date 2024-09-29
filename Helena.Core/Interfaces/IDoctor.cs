using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helena.Core.Interfaces;
public interface IDoctor : IEntity
{
    string Name { get; set; }
    string Specialty { get; set; }
    string Phone { get; set; }
    string Email { get; set; }
    Guid UserId { get; set; }

}
