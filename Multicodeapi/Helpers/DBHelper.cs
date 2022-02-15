using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Multicodeapi.Models;
using System.Text;

namespace Multicodeapi.Helpers
{
    public enum objects
    {
        Team,
        Member
    }
    public class DBHelper
    {
        private string Connectionstring;
        private SqlConnection Con;
        private SqlCommand Cmd;
        private SqlDataReader rdr;

        public DBHelper()
        {
            this.Connectionstring = ConfigurationManager.ConnectionStrings["localdb"].ConnectionString;
            Con = new SqlConnection(this.Connectionstring);
            Cmd = new SqlCommand("",Con);
        }
        
        public void ExecuteNonQuery(string SqlQuery, List<InputParameter> InputParameters,CommandType commandtype = CommandType.Text)
        {
            try
            {
                Cmd.CommandText = SqlQuery;
                Cmd.CommandType = commandtype;

                if (InputParameters != null && InputParameters.Count != 0)
                {
                    foreach (var inputpara in InputParameters)
                        Cmd.Parameters.AddWithValue(inputpara.Name, inputpara.Value);
                }

                Con.Open();
                Cmd.ExecuteNonQuery();
                Con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<object> ExecuteReader(string SqlQuery, List<InputParameter> InputParameters,objects objectname, CommandType commandtype = CommandType.Text)
        {
            try
            {
                Cmd.CommandText = SqlQuery;
                Cmd.CommandType = commandtype;

                if (InputParameters != null && InputParameters.Count != 0)
                {
                    foreach (var inputpara in InputParameters)
                        Cmd.Parameters.AddWithValue(inputpara.Name, inputpara.Value);
                }

                Con.Open();
                rdr = Cmd.ExecuteReader();
                List<object> dataobject = new List<object>();
                while (rdr.Read())
                {
                    dataobject.Add(Convert(rdr, objectname));
                }
                Con.Close();
                return dataobject;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object Convert(SqlDataReader rdr, objects objectname)
        {
            object obj = new object();
            switch (objectname)
            {

                case objects.Team:
                    obj = new Team()
                    {
                        Id = (int)rdr[0],
                        TeamName = rdr[1].ToString(),
                        FolderPath = rdr[2].ToString(),
                        SecretNumber = rdr[3].ToString(),
                        CreatedOn = (DateTime)rdr[4],
                        ClosedOn = (rdr[5] == DBNull.Value) ? default : (DateTime)rdr[5]
                    };
                    break;
                case objects.Member:
                    obj = new Member()
                    {
                        Id = (int)rdr[0],
                        Name = rdr[1].ToString(),
                        TeamId = (int)rdr[2]
                    };
                    break;
                default:
                    break;

            }
            return obj;
        }
        
    }
}