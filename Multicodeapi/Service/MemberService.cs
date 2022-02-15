using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Multicodeapi.Helpers;
using Multicodeapi.Models;

namespace Multicodeapi.Service
{
    public class MemberService
    {
        public DBHelper DBHelper;
        public TeamService TeamService;
        public MemberService()
        {
            DBHelper = new DBHelper();
        }
        public void JoinTeam(string SecretNumber,string MemberName)
        {
            TeamService = new TeamService();
            Team team = TeamService.GetTeam(SecretNumber);
            if (team != null)
            {
                List<InputParameter> parameters = new List<InputParameter>();
                parameters.Add(new InputParameter() { Name = "Name", Value = MemberName });
                parameters.Add(new InputParameter() { Name = "TeamId", Value = team.Id });

                DBHelper.ExecuteNonQuery("insert into Members(Name,TeamId) values(@Name,@TeamId)", parameters);

            }
        }
        public void LeaveTeam(int MemberId)
        {
            List<InputParameter> parameters = new List<InputParameter>
            {
                new InputParameter{Name="MemberId",Value=MemberId}
            };
            DBHelper.ExecuteNonQuery("delete from Members where Id=@MemberId", parameters);
        }

        public List<Member> GetTeamMembers(int TeamId)
        {
            List<InputParameter> parameters = new List<InputParameter>
            {
                new InputParameter{Name="TeamId",Value=TeamId}
            };
            var members = new List<Member>();
            var dataobject = DBHelper.ExecuteReader("select * from Members where TeamId=@TeamId", parameters,objects.Member);
            foreach (var item in dataobject)
                members.Add((Member)item);

            return members;
        }
        
    }
}