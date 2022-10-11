using Motives.WebTest.BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motives.WebTest.BackEnd.Interfaces
{
    public interface IContextService
    {
        void SetCookie(string key, string value);
        TableUser CurrentUser { get; set; }
    }
}
