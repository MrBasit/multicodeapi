using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Multicodeapi.Models;
using Multicodeapi.Helpers;
using Multicodeapi.Service;

namespace Multicodeapi.Service
{
    public class TeamService
    {
        DBHelper DBHelper;
        MemberService MemberService;
        public TeamService()
        {
            DBHelper = new DBHelper();
        }
        public Team GetTeam(string SecretNumber)
        {
            List<InputParameter> parameters = new List<InputParameter>();
            parameters.Add(new InputParameter { Name = "@secretnumber", Value = SecretNumber });
            List<object> dataobject = this.DBHelper.ExecuteReader("select * from teams where SecretNumber=@secretnumber", parameters, objects.Team);
            return (Team)dataobject[0];
        }
        public TeamInfo GetTeamInfo(string SecretNumber)
        {
            MemberService = new MemberService();
            TeamInfo teaminfo = new TeamInfo();

            teaminfo.Team = GetTeam(SecretNumber);
            teaminfo.Members = MemberService.GetTeamMembers(teaminfo.Team.Id);

            return teaminfo;
        }
        public string GenerateSecurityCode()
        {
            bool isunique = true;
            var sb = new StringBuilder();

            do
            {
                const string src = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                Random RNG = new Random();
                for (var i = 0; i < 8; i++)
                {
                    var c = src[RNG.Next(0, src.Length)];
                    sb.Append(c);
                }
                //check uniqness of code from database
                //isunique = false;
            } while (!isunique);

            return sb.ToString();
        }
    }
}