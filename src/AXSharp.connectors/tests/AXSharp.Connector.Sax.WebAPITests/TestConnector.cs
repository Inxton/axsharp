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
    private static string TargetIp { get; } = Environment.GetEnvironmentVariable("AX_WEBAPI_TARGET") ?? "10.222.6.1";

    public static WebApiConnector TestApiConnector { get; } 
        = new WebApiConnector(TargetIp, "adm", Environment.GetEnvironmentVariable("AX_TARGET_PWD"),CertificateValidation, true).BuildAndStart() as WebApiConnector;
        
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
     
    public static ax_test_projectTwinController SecurePlc { get; }
        = new(ConnectorAdapterBuilder.Build()
            .CreateWebApi(TargetIp, "adm", Environment.GetEnvironmentVariable("AX_TARGET_PWD"), CertificateValidation, true));
    
    
}