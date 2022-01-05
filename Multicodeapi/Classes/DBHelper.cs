using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Multicodeapi.Models;

namespace Multicodeapi.Classes
{
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
        
        public SqlDataReader ExecuteReader(string SqlQuery, List<InputParameter> InputParameters, CommandType commandtype=CommandType.Text)
        {
            //try
            //{
            //    Cmd.CommandText = SqlQuery;
            //    Cmd.CommandType = commandtype;

            //    if (InputParameters != null && InputParameters.Count != 0)
            //    {
            //        foreach (var inputpara in InputParameters)
            //            Cmd.Parameters.AddWithValue(inputpara.Name, inputpara.Value);
            //    }

            //    Con.Open();
            //    rdr = Cmd.ExecuteReader();
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            Cmd.CommandText = SqlQuery;
            DataSet ds = testdataadapter();
            var a = ds.Tables["Teams"].Rows;
            
            return rdr;
        }
        public DataSet testdataadapter()
        {
            DataSet ds;
            using (SqlDataAdapter da = new SqlDataAdapter(Cmd.CommandText, Con))
            {
                da.SelectCommand.Parameters.AddWithValue("@secretnumber", "1212");
                da.SelectCommand = Cmd;
                ds= new DataSet();
                da.Fill(ds);
            }
            return ds;
        }

    }
}