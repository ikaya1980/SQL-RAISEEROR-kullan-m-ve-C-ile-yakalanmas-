using System.Data;
using System.Data.SqlClient;

namespace SqlError
{
    class DbProcesor
    {
        public static void Execute()
        {
            try
            {
                string conStr = "Server=.;Database=myProject;Trusted_Connection=True;";
                SqlConnection conn = new SqlConnection(conStr);
                SqlCommand command = new SqlCommand("EXEC sp_Myprocedure", conn);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters;
                #region Initize and Add Sql Parameter in parameters
                parameters = new SqlParameter[3];

                #endregion

                command.Parameters.AddRange(parameters);
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
            catch (SqlException ex) when (ex.Number == 50000)//Sql server prosodürü içinde RAISEEROR msg_str ile fırlatıldığında error number 50000 olacaktır. 
            {
                //Kullanıcı mesajıdır. Mesaj içeriğini kullanıcı görebilir.
            }
            catch (SqlException ex)
            {
                //Sistemsel hata oluşuştur. Logla ve kullanıcıya anlaşılır bir mesaj dön.
            }
        }
    }
}
