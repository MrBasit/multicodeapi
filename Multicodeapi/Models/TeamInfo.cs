using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Multicodeapi.Models;
namespace Multicodeapi.Models
{
    public class TeamInfo
    {
        public Team Team { get; set; }
        public List<Member> Members { get; set; }
    }
}