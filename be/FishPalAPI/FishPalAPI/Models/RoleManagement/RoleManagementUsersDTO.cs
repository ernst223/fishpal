using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FishPalAPI.Models.RoleManagement
{
    public class RoleManagementUsersDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Facet { get; set; }
        public string Province { get; set; }
        public string Club { get; set; }
        public string MemberNumber { get; set; }
        public string Role { get; set; }
        public bool Status { get; set; }
    }
}
