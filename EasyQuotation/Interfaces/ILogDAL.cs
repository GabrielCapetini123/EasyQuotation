using System;

namespace EasyQuotation.DAL
{
    public interface ILogDAL
    {
        void RegistrarLog(string rotina, Exception ex);
    }
}
