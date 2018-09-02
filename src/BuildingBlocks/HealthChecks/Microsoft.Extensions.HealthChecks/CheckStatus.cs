using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.HealthChecks
{
    public enum CheckStatus
    {
        Unknown,
        Unhealthy,
        Healthy,
        Warning
    }
}
