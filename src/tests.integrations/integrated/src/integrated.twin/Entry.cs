// integrated
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using AXSharp.Connector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AXSharp.Connector.S71500.WebApi;

namespace integrated
{
    public static class Entry
    {
        private static string TargetIp { get; } = Environment.GetEnvironmentVariable("AXTARGET") ?? "10.222.6.1";
               
        private static string CertificatePath = "certs\\Communication.cer"; 
        
        static string GetCertPath()
        {
            var fp = new FileInfo(Path.Combine(Assembly.GetExecutingAssembly().Location));
            return Path.Combine(fp.DirectoryName, CertificatePath);
        }

        static readonly X509Certificate2 Certificate = new X509Certificate2(GetCertPath());

        private static bool CertificateValidation(HttpRequestMessage requestMessage, X509Certificate2 certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return certificate.Thumbprint == Certificate.Thumbprint;
        }
     
        public static integratedTwinController Plc { get; }
            = new(ConnectorAdapterBuilder.Build()
                .CreateWebApi(TargetIp, Environment.GetEnvironmentVariable("AX_USER_NAME"), Environment.GetEnvironmentVariable("AX_TARGET_PWD"), CertificateValidation, true));                
    }
}