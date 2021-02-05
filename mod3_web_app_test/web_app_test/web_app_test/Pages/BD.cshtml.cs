using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace web_app_test.Pages
{
    public class BDModel : PageModel
    {
        public List<string> Linhas { get; set; } = new List<string>();
        public void OnGet()
        {
            string connectionString = "Data source=(local); Initial Catalog=biblioteca_xpto;" + "Integrated Security=true";
            string query = "SELECT * FROM obras";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand(query, connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string row = "";
                    row += reader[1] + " " + reader[0];
                    Linhas.Add(row);
                }
            } 
        }

        public int CmdExecute(string sQry)
        {
            string connectionString = "Data source=(local); Initial Catalog=biblioteca_xpto;Integrated Security=true";
            SqlConnection cn = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(sQry, cn);
            cn.Open();
            return command.ExecuteNonQuery();
        }
    }
}
