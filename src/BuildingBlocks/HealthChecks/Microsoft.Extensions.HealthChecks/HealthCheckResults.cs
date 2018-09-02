using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.HealthChecks
{
    public class HealthCheckResults
    {
        public IList<IHealthCheckResult> CheckResults { get; } = new List<IHealthCheckResult>();
    }
}
