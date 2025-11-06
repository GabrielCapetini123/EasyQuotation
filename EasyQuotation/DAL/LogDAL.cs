using System;
using System.Data.SqlClient;

namespace EasyQuotation.DAL
{
    public class LogDAL
    {
        private readonly string _connectionString;

        public LogDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void RegistrarLog(string rotina, Exception ex)
        {
            try
            {
                using (var conexao = new SqlConnection(_connectionString))
                {
                    string query = @"
                        INSERT INTO Log (Rotina, ExcecaoCapturada, Data)
                        VALUES (@Rotina, @ExcecaoCapturada, @Data)";

                    using (var comandoSql = new SqlCommand(query, conexao))
                    {
                        comandoSql.Parameters.AddWithValue("@Rotina", rotina ?? "Rotina não informada");
                        comandoSql.Parameters.AddWithValue("@ExcecaoCapturada", ex.ToString());
                        comandoSql.Parameters.AddWithValue("@Data", DateTime.Now);

                        conexao.Open();
                        comandoSql.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
            }
        }
    }
}