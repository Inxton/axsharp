// ix-integration-plc
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

//#define dummy

using AXSharp.Connector;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AXSharp.Connector.S71500.WebApi;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace ix_integration_plc
{
    public class TwinConnectorSelector
    {
        public static string TargetIp { get; } = "192.168.100.85";
        private static string Pass => @"123ABCDabcd$#!"; //Environment.GetEnvironmentVariable("AX_TARGET_PWD");       //Environment.GetEnvironmentVariable("AX_TARGET_PWD"); // <- Pass in the password that you have set up for the user. NOT AS PLAIN TEXT! Use user secrets instead.
        private static string UserName = "admin"; //Environment.GetEnvironmentVariable("AX_USERNAME"); //<- replace by username you have set up in your WebAPI settings        
        private const bool IgnoreSslErrors = true; // <- When you have your certificates in order set this to false.
        private static string CertificatePath = "..\\..\\..\\..\\ix-integration-plc\\certs\\plc_line\\plc_line.cer";

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

        public static ix_integration_plcTwinController SecurePlc { get; }
            = new(ConnectorAdapterBuilder.Build()
                //.CreateDummy());
            .CreateWebApi(TargetIp, UserName, Pass, CertificateValidation, IgnoreSslErrors));
    }

    public static class Entry
    {
        public static ix_integration_plcTwinController Plc { get; } = TwinConnectorSelector.SecurePlc;
    }

//    public static class Entry
//    {
//#if !dummy
//        public static ix_integration_plcTwinController Plc { get; } = new (ConnectorAdapterBuilder.Build().CreateWebApi(Environment.GetEnvironmentVariable("AXTARGET"), "Everybody", "", true));
//#else
//        public static ix_integration_plcTwinController Plc { get; } = new(ConnectorAdapterBuilder.Build().CreateDummy());
//#endif
//    }


    //public static class PlcResources
    //{
    //    private static System.Resources.ResourceManager _resourceManger;
    //    private static volatile object mutex = new object();
    //    internal static System.Resources.ResourceManager ResourceManager
    //    {
    //        get
    //        {
    //            lock (mutex)
    //            {
    //                if (_resourceManger == null)
    //                {
    //                    var defaultResourceType = Assembly.GetAssembly(typeof(PlcResources))
    //                        .GetType("Resources.PlcStringResources");
    //                    if (defaultResourceType != null)
    //                    {
    //                        _resourceManger = new ResourceManager(defaultResourceType);
    //                    }
    //                }

    //                return _resourceManger;
    //            }
    //        }
    //    }
    //}
}
