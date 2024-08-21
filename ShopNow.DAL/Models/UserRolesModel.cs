using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.DAL.Models
{
    public class UserRolesModel
    {
        public Guid Id { get; set; }
        public Guid? CustomerId { get; set; }
        public string? RoleId { get; set; }

    }
}
