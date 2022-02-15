using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using System.Web;
using Multicodeapi.Helpers;
using Multicodeapi.Models;
using System.Text;

namespace Multicodeapi.Controllers
{
    public class MechanisamController : ApiController
    {
        public DBHelper DBHelper;
        public OPHelper OPHelper;
        public MechanisamController()
        {
            DBHelper = new DBHelper();
            OPHelper = new OPHelper();
        }
    }
}
