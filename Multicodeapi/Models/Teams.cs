using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Multicodeapi.Models
{
    public class Teams
    {
        public int Id { get; set; }
        public string TeamName { get; set; }
        public string SecretNumber { get; set; }
        public string FolderPath { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ClosedOn { get; set; }
    }
}
