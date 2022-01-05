using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
namespace Multicodeapi.Models
{
    public class InputParameter
    {
        public string Name { get; set; }
        public object Value { get; set; }
    }
}