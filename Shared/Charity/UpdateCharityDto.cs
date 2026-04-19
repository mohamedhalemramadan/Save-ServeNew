using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Charity
{
    public record UpdateCharityDto
    {
        public string? CoverageArea { get; init; }
        public string? RegistrationNo { get; init; }
        public string? Mission { get; init; }
    }
}
