using Core;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Data
{
    public class HospDB
    {
        private readonly string _connectionString;// = "Data source=(local); Initial Catalog = teste_mod2_parteII;Integrated Security = true";
        

        public HospDB(IConfiguration config)
        {
            
            _connectionString = config.GetSection("ConnectionStrings")["HospDB"];
        }

        #region Medicos
        public List<Medico> GetMedicos() => GetMedicosFromDataTable(GetDataTable("Medicos"));
        //public List<Medico> GetMedicos() => GetAll<Medico>("SELECT * FROM Medicos");

        public Medico GetMedicoById(int medicoId) =>  GetMedicos().FirstOrDefault(m => m.Id == medicoId);

        public void UpdateMedico(Medico m) => Update<Medico>("Medicos", m);

        public void AddMedico(Medico m) => Add<Medico>("Medicos", m);

        private List<Medico> GetMedicosFromDataTable(DataTable dataTable)
        {
            var returnList = new List<Medico>();
            
            foreach ( var row in dataTable.AsEnumerable())
            {
                var curMedico = new Medico()
                {
                    Id = row.Field<int>("Id"),
                    Nome = row.Field<string>("Nome"),
                    DataNascimento = row.Field<DateTime?>("DataNascimento"),
                    Ativo = row.Field<bool>("Ativo")
                };
                returnList.Add(curMedico);
            }

            return returnList;
        }
        #endregion Medicos 
        #region Consultas
        public List<Consulta> GetConsultas() => GetConsultasFromDataTable(GetDataTable("Consultas"));

        public void UpdateConsulta(Consulta c)
        {
            string taxa = c.IncluirTaxa == true ? "1" : "0";
            string sqlQuery = $"UPDATE Consultas SET IncluirTaxa = {taxa} WHERE Id_Paciente = {c.Id_Paciente} AND Id_Medico = {c.Id_Medico} AND Id_Especialidade = {c.Id_Especialidade}";
            ExecuteNonQuery(sqlQuery);
        }

        public void AddConsulta(Consulta c) => Add<Consulta>("Consultas", c);

        public void GuardarHist(int ano)
        {
            ExecuteStoredProcedure("SP05_GuardarHistAno", new SqlParameter() { ParameterName = "@ano", Value = ano, SqlDbType = SqlDbType.Int, Direction=ParameterDirection.Input, });
        }

        private List<Consulta> GetConsultasFromDataTable(DataTable dataTable)
        {
            var returnList = new List<Consulta>();

            foreach (var row in dataTable.AsEnumerable())
            {
                var curConsulta = new Consulta()
                {
                    Id_Especialidade = row.Field<int>("Id_Especialidade"),
                    Id_Medico = row.Field<int>("Id_Medico"),
                    Id_Paciente = row.Field<int>("Id_Paciente"),
                    Data_Consulta = row.Field<DateTime>("Data_Consulta"),
                    IncluirTaxa = row.Field<bool>("IncluirTaxa")  
                };
                returnList.Add(curConsulta);
            }
            return returnList;
        }
        #endregion
        #region Pacientes
        public List<Paciente> GetPacientes() => GetPacientesFromDataTable(GetDataTable("Pacientes"));
        private List<Paciente> GetPacientesFromDataTable(DataTable dataTable)
        {
            var returnList = new List<Paciente>();

            foreach (var row in dataTable.AsEnumerable())
            {
                var curPaciente = new Paciente()
                {
                    Id = row.Field<int>("Id"),
                    Nome = row.Field<string>("Nome"),
                    DataNascimento = row.Field<DateTime>("DataNascimento"),
                    Beneficiario = row.Field<string>("Beneficiario")
                };
                returnList.Add(curPaciente);
            }

            return returnList;
        }
        #endregion
        #region Especialidades
        public List<Especialidade> GetEspecialidades() => GetEspecialdadesFromDataTable(GetDataTable("Especialidades"));

        private List<Especialidade> GetEspecialdadesFromDataTable(DataTable dataTable)
        {
            var returnList = new List<Especialidade>();

            foreach (var row in dataTable.AsEnumerable())
            {
                returnList.Add( new Especialidade 
                {
                    Id = row.Field<int>("Id"),
                    Nome = row.Field<string>("Nome"),
                    Valor = row.Field<decimal>("Valor"),
                    TaxaPercentual = row.Field<int>("TaxaPercentual")
                });
            }

            return returnList;
        }
        #endregion
        /// <summary>
        /// Devolve uma Lista genérica de objectos modelo que representam os agentes na base de dados
        /// </summary>
        /// <typeparam name="T">O tipo do modelo que representa a entidade na BD. As propriedades DEVEM ser iguais aos nomes das colunas na DB.</typeparam>
        /// <param name="sqlQuery">Query para retirar uma tabela da BD. Deverá ser SELECT</param>
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

        /// <summary>
        /// Devolve um DataTable que reflete a tabela do mesmo nome na base de dados
        /// </summary>
        /// <param name="TableName">A Tabela desejada</param>
        /// <returns></returns>
        public DataTable GetDataTable(string TableName)
        {
            DataTable dataTable = new DataTable();
            using (var connection = new SqlConnection(_connectionString))
            {
                string sqlQuery = $"SELECT * FROM {TableName}";
                
                using (var command = new SqlCommand(sqlQuery, connection))
                {
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            return dataTable;
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

        private void ExecuteStoredProcedure(string proc, params SqlParameter[] parameters)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(proc, conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        if(parameters.Length > 0)
                        {
                            foreach (SqlParameter parameter in parameters)
                            {
                                command.Parameters.Add(parameter);
                            }
                        }
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
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
