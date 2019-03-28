﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace NuGetPe.Utility
{
    class VersionTelemetry : ITelemetryInitializer
    {
        private readonly string _wpfVersion;
        private readonly string _clrVersion;
        private readonly string _appVersion;

        public VersionTelemetry()
        {
            _wpfVersion = typeof(System.Windows.Application).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
            _clrVersion = typeof(string).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
            _appVersion = typeof(DiagnosticsClient).Assembly.GetCustomAttributes<AssemblyMetadataAttribute>()
                                                           .FirstOrDefault(ama => string.Equals(ama.Key, "CloudBuildNumber", StringComparison.OrdinalIgnoreCase))
                                                           ?.Value;
        }

        public void Initialize(ITelemetry telemetry)
        {
            telemetry.Context.GlobalProperties["wpfVersion"] = _wpfVersion;
            telemetry.Context.GlobalProperties["clrVersion"] = _clrVersion;
            telemetry.Context.Component.Version = _appVersion;
        }
    }

}