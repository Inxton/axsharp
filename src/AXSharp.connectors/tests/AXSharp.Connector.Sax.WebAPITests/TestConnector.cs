using System;
using System.IO;
using System.Net.Http;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using AXSharp.Connector.S71500.WebApi;

namespace AXSharp.Connector.S71500.WebAPITests;

public static class TestConnector
{
    private static string TargetIp { get; } = "192.168.100.210";
    private static string UserName { get; } = "admin";
    private static string Password { get; } = "123ABCDabcd$#!";

    
    public static WebApiConnector TestApiConnector
    {
        get
        {
            return SecurePlc.Connector as WebApiConnector;
        }
    }

    private static string CertificatePath = "..\\..\\..\\..\\ax-test-project\\certs\\plc_line\\plc_line.cer"; 
        
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

    private static ax_test_projectTwinController securePlc;
    private static object mutex = new object();
    public static ax_test_projectTwinController SecurePlc
    {
        get
        {
            lock (mutex)
            {
                if (securePlc == null)
                {
                    securePlc = new(ConnectorAdapterBuilder.Build()
                        .CreateWebApi(TargetIp, UserName, Password
                            , CertificateValidation, true));
                    
                    SecurePlc.Connector.BuildAndStart();
                }
                return securePlc;
            }
        }
    }
       


}
