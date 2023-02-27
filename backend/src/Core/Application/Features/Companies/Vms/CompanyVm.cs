﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.Application.Features.SocialImages.Vms;
using Tickets.Domain.Common;

namespace Tickets.Application.Features.Companies.Vms
{
    public class CompanyVm
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? CellPhone { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? MapUrl { get; set; }
        public Status? Status { get; set; }
        public virtual ICollection<CompanyImageVm>? CompanyImages { get; set; }
    }
}
