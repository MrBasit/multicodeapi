using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace Multicodeapi.Helpers
{
    public class OPHelper
    {
        public DirectoryInfo CreateFolder(string FolderName)
        {
            
            try
            {
                string ParentFolderPath = HttpContext.Current.Server.MapPath("~/Projects");
                if (!Directory.Exists(ParentFolderPath))
                {
                    DirectoryInfo pdi = Directory.CreateDirectory(ParentFolderPath);
                }
                string ChildFolderPath = HttpContext.Current.Server.MapPath($"~/Projects/{FolderName}");
                DirectoryInfo cdi = Directory.CreateDirectory(ChildFolderPath);
                return cdi;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}