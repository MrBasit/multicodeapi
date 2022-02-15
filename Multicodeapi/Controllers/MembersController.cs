using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Multicodeapi.Service;

namespace Multicodeapi.Controllers
{
    public class MembersController : ApiController
    {
        MemberService MemberService;
        public MembersController()
        {
            MemberService = new MemberService();
        }

        //api/jointeam? SecretNumber=secretnumber & MemberName=memebrename
        [Route("api/jointeam")]
        [HttpPost]
        public IHttpActionResult JoinTeam(string SecretNumber, string MemberName)
        {
            try
            {
                MemberService.JoinTeam(SecretNumber, MemberName);
                return Ok();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Route("api/leaveteam/{MemberId}")]
        [HttpDelete]
        public IHttpActionResult LeaveTeam(int MemberId)
        {
            try
            {
                MemberService.LeaveTeam(MemberId);
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
    }
}