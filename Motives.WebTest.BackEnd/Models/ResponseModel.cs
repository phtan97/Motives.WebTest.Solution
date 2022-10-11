using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motives.WebTest.BackEnd.Models
{
    public class ResponseModel
    {
        public EStatusModel Status { get; set; }
        public string Message { get; set; }
    }

    public enum EStatusModel
    {
        Success,
        Failed
    }
}
