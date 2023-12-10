﻿using GreatProj.Core.Models.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreatProj.Core.Models.Employee
{
    public class GetAllEmployeeInput : PagedAndSortedDTO
    {
        public string Role { get; set; }
        public string Mail { get; set; }
        public string Number { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
