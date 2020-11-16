using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKP_IntranetSideAPI.Models
{
    public class APIReqModel : IAPIReqModel
    {
        public string Json { get; set; }

    }
    public interface IAPIReqModel
    {
        public string Json { get; set; }
    }
    public class ProjectReq
    {
        public ProjectModel Content { get; set; }
        public string UserName { get; set; }
    }
}
