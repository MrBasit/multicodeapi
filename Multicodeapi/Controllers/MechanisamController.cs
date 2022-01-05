using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;

using Multicodeapi.Classes;
using Multicodeapi.Models;

namespace Multicodeapi.Controllers
{
    public class MechanisamController : ApiController
    {
        public DBHelper DBHelper;
        public MechanisamController()
        {
            DBHelper = new DBHelper();
        }
        [Route("api/getteam/{SecretNumber}")]
        [HttpGet]
        public IHttpActionResult GetTeam(string SecretNumber)
        {
            //this.DBHelper = new DBHelper("select * from teams where SecretNumber='1212'");
            //this.DBHelper.Con.Open();
            //SqlDataReader rdr = this.DBHelper.Cmd.ExecuteReader();
            //Teams teams = new Teams();
            //while (rdr.Read())
            //{
            //    teams.Id = Convert.ToInt32(rdr[0]);
            //    teams.TeamName = rdr[1].ToString();
            //}
            //return Ok(teams);
            Teams team = null;
            List<InputParameter> parameters = new List<InputParameter>();
            parameters.Add(new InputParameter { Name = "@secretnumber", Value = SecretNumber });
            using (var rdr=DBHelper.ExecuteReader("select * from teams where SecretNumber=@secretnumber",parameters)) {
                while (rdr.Read())
                {
                    team = new Teams()
                    {
                        Id = Convert.ToInt32(rdr["Id"]),
                        TeamName = rdr["TeamName"].ToString()
                    };
                }
            }
            if (team == null) return NotFound();
            return Ok(team);
            
            //return Ok();
        }
    }
}
