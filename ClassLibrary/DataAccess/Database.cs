using Dapper;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ClassLibrary.DataAccess
{
    public class Database
    {
        static readonly string ToolRoomSchedulerDB = "SQLServerToolRoomSchedulerDB";
        static readonly string ProgramManagerDB = "SQLServerProgramManagerDB";

        public static List<int> GetPressTonnages()
        {
            using (IDbConnection connection = new SqlConnection(Helper.CnnValue(ProgramManagerDB)))
            {
                string queryString = "SELECT DISTINCT Tonnage FROM Presses";

                return connection.Query<int>(queryString).ToList();
            }

            //List<string> tonnages = new List<string>();
            //tonnages.Add("350");
            //tonnages.Add("400");
            //tonnages.Add("450");

            //return tonnages;
        }
    }
}
