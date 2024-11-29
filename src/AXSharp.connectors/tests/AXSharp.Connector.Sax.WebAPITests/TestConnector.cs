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
    private static string TargetIp { get; } = Environment.GetEnvironmentVariable("AXTARGET") ?? "10.222.6.1";
    private static string UserName { get; } = Environment.GetEnvironmentVariable("AX_USERNAME") ?? "adm";
    private static string Password { get; } = Environment.GetEnvironmentVariable("AX_TARGET_PWD");

    
    public static WebApiConnector TestApiConnector
    {
        get
        {
            return SecurePlc.Connector as WebApiConnector;
        }
    }

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
                        .CreateWebApi(TargetIp, Environment.GetEnvironmentVariable("AX_USERNAME"),
                            Environment.GetEnvironmentVariable("AX_TARGET_PWD"), CertificateValidation, true));
                }
                return securePlc;
            }
        }
    }
       


}