using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helena.Core.Interfaces;
public interface IAppUser
{
    string Name { get; set; }
    string LastName { get; set; }
}
