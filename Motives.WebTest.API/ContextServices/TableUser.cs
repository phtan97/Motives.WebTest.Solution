using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Motives.WebTest.API.ContextServices
{
    public class TableUser
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Username { get; set; }
        [StringLength(int.MaxValue)]
        public string Password { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string SubName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }
    }
}
