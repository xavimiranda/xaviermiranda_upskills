using Core;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Data
{
    public class HospDB
    {
        private readonly string _connectionString = "Data source=(local); Initial Catalog = teste_mod2_parteII;Integrated Security = true";
                      
        public HospDB()
        {
        }
        #region Medicos
        public List<Medico> GetMedicos() => GetAll<Medico>("SELECT * FROM Medicos");
        
        public Medico GetMedicoById(int medicoId) =>  GetMedicos().FirstOrDefault(m => m.Id == medicoId);

        public void UpdateMedico(Medico m) => Update<Medico>("Medicos", m);

        public void AddMedico(Medico m) => Add<Medico>("Medicos", m);

        #endregion Medicos
        private List<T> GetAll<T>(string sqlQuery)
        {
            var result = new List<T>();

            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(sqlQuery, conn))
                    {
                        using (SqlDataReader dataReader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            PropertyInfo[] propInfo = typeof(T).GetProperties();
                            while(dataReader.Read())
                            {
                                var rowModel = Activator.CreateInstance<T>();
                                foreach(var prop in propInfo)
                                {
                                    prop.SetValue(rowModel, dataReader.IsDBNull(dataReader.GetOrdinal(prop.Name)) ? null : dataReader.GetValue(dataReader.GetOrdinal(prop.Name)));
                                }
                                result.Add(rowModel);
                            }
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            return result;
        }
        
        private void Update<T>(string tableName, T model)
        {
            int ID;
            string[] columns, values;
            GetColumnsAndValuesFromModel(model, out ID, out columns, out values);

            UpdateById(tableName, columns, values, ID);
        }


        private void UpdateById(string tableName, string[] columns, string[] values, int id) 
        {
            string sqlQuery = $"UPDATE {tableName} SET ";
            for(int i = 0; i < columns.Length; i++)
            {
                sqlQuery += $"{columns[i]} = '{values[i]}', ";
            }
            sqlQuery = sqlQuery.Trim().Trim(',');
            sqlQuery += $"WHERE Id = {id}";

            ExecuteNonQuery(sqlQuery);
        }
        private void Add<T>(string tableName, T model)
        {
            int id;
            string[] columns, values;
            GetColumnsAndValuesFromModel(model, out id, out columns, out values);
            string sqlQuery = $"INSERT INTO {tableName} (";

            foreach (string column in columns)
                sqlQuery += $"{column}, ";

            sqlQuery = sqlQuery.Trim().Trim(',');
            sqlQuery += ") Values (";

            foreach (string value in values)
                sqlQuery += $"'{value}', ";

            sqlQuery = sqlQuery.Trim().Trim(',');
            sqlQuery += ")";

            ExecuteNonQuery(sqlQuery);
        }


        private void ExecuteNonQuery(string sqlQuery)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(sqlQuery, conn))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        private static void GetColumnsAndValuesFromModel<T>(T model, out int ID, out string[] columns, out string[] values)
        {
            PropertyInfo[] propInfo = model.GetType().GetProperties();

            var id = (PropertyInfo)propInfo.Where(pI => pI.Name == "Id").FirstOrDefault();
            ID = (int)id.GetValue(model);
            columns = propInfo.Where(propInfo => propInfo.Name != "Id")
                                       .Select(propInfo => propInfo.Name)
                                       .ToArray();
            values = propInfo.Where(propInfo => propInfo.Name != "Id")
                            .Select(propInfo =>
                            {
                                string cast = "";
                                if (propInfo.PropertyType == typeof(bool))
                                {
                                    cast = propInfo.GetValue(model).ToString() == "True" ? "1" : "0";
                                }
                                else if (propInfo.PropertyType == typeof(DateTime?))
                                {
                                    var date = propInfo.GetValue(model) as DateTime?;
                                    cast = date.HasValue ? date.Value.ToString("yyyy-MM-dd") : null;
                                }
                                else
                                    cast = propInfo.GetValue(model).ToString();
                                return cast;
                            })
                            .ToArray();
        }

    }
}
