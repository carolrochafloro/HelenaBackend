using Domain.Contracts.DTO;
using Domain.Entities;
using Domain.Interfaces.Data;
using Helena.Web.Data.Context;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data;
public class DoctorData : IDoctorData
{
    private readonly Context _context;
    private ILogger<DoctorData> _logger;    

    public DoctorData(Context context,
                      ILogger<DoctorData> logger)
    {
        _context = context;
        _logger = logger;
    }

    public Task<ResponseDTO> CreateDoctorAsync(Doctor doctor)
    {
        
    }

    public Task<ResponseDTO> DeleteDoctorAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Doctor Getdoctor(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDTO> UpdateDoctorAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
