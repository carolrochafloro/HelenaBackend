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

namespace Infra.Data.Data
{
    public class TimesData : ITimesData
    {

        private readonly ILogger<TimesData> _logger;
        private readonly Context _context;

        public TimesData(ILogger<TimesData> logger,
                         Context context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<ResponseDTO> CreateTimes(Times times)
        {
            try
            {

                await _context.Set<Times>().AddAsync(times);
                await _context.SaveChangesAsync();

                return new ResponseDTO
                {
                    Status = Domain.Contracts.Enum.StatusResponseEnum.Success,
                    Message = "Horários adicionados"
                };

            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao adicionar os horários: {ex.Message}");
                throw;
            }

        }
    }
}
