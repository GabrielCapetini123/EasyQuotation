using EasyQuotation.Models.Data;
using EasyQuotation.Models.Entities;
using System;

namespace EasyQuotation.DAL
{
    public class LogDAL : ILogDAL
    {
        private readonly EasyQuotationDataContext _context;

        public LogDAL(string connectionString)
        {
            _context = new EasyQuotationDataContext(connectionString);
        }

        public void RegistrarLog(string rotina, Exception ex)
        {
            try
            {
                var log = new Log
                {
                    Rotina = rotina ?? "Rotina não informada",
                    ExcecaoCapturada = ex?.ToString() ?? "Exceção não informada",
                    Data = DateTime.Now
                };

                _context.Logs.InsertOnSubmit(log);
                _context.SubmitChanges();
            }
            catch
            {
            }
        }
    }
}
