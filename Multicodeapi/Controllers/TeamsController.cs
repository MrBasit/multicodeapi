using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Multicodeapi.Helpers;
using Multicodeapi.Models;
using Multicodeapi.Service;

namespace Multicodeapi.Controllers
{
    public class TeamsController : ApiController
    {
        public DBHelper DBHelper;
        public OPHelper OPHelper;
        public TeamService TeamService;
        public MemberService MemberService;
        public TeamsController()
        {
            DBHelper = new DBHelper();
            OPHelper = new OPHelper();
            TeamService = new TeamService();
            MemberService = new MemberService();
        }

        [Route("api/getteam/{SecretNumber}")]
        [HttpGet]
        public IHttpActionResult GetTeam(string SecretNumber)
        {
            Team team = null;
            team = TeamService.GetTeam(SecretNumber);

            if (team == null) return NotFound();
            return Ok(team);
        }

        //"api/createteam?TeamName=any&CreatedBy=name"//
        [Route("api/createteam")]
        [HttpPost]
        public IHttpActionResult CreateTeam(string TeamName, string CreatedBy)
        {
            string SecretNumber = TeamService.GenerateSecurityCode();

            //check TeamName is uniqness

            List<InputParameter> parameters = new List<InputParameter>();
            parameters.Add(new InputParameter { Name = "@teamname", Value = TeamName });
            parameters.Add(new InputParameter { Name = "@folderpath", Value = HttpContext.Current.Server.MapPath($"~/Projects/{TeamName}") });
            parameters.Add(new InputParameter { Name = "@secretnumber", Value = SecretNumber });
            parameters.Add(new InputParameter { Name = "@createdon", Value = DateTime.Now });

            string query = "insert into Teams(TeamName,FolderPath,SecretNumber,CreatedDate) values(@teamname,@folderpath,@secretnumber,@createdon)";

            DBHelper.ExecuteNonQuery(query, parameters);

            //add folder
            OPHelper.CreateFolder(TeamName);

            //call method for Adding Member to project
            MemberService.JoinTeam(SecretNumber, CreatedBy);

            return Ok();
        }

        [Route("api/GetTeamInfo/{SecretNumber}")]
        [HttpGet]
        public IHttpActionResult GetTeamInfo(string SecretNumber)
        {
            try
            {
                TeamInfo TeamInfo = TeamService.GetTeamInfo(SecretNumber);
                return Ok(TeamInfo);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}