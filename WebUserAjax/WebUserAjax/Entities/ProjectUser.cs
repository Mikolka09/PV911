﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUserAjax.Entities
{
    public class ProjectUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Avatar { get; set; }
    }
}
