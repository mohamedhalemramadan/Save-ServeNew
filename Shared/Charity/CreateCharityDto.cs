using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Charity
{
    public record CreateCharityDto
    {
        public string CoverageArea { get; init; } = string.Empty;
        public string RegistrationNo { get; init; } = string.Empty;
        public string Mission { get; init; } = string.Empty;
    }
}
