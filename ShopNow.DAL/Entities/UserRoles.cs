﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.DAL.Entities
{
    public class UserRoles
    {
       public Guid Id { get; set; }
       public string? RoleId { get; set; }
       public string? UserRole { get; set; }

    }
}