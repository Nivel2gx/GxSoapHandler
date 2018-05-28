using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using GeneXus.Programs;
using GeneXus.Utils;

    public class GxSoapHandler
    {
        public void Setup(string eoName, GxLocation loc, object serviceClient)
        {
            String Parameters = loc.Configuration.ToString();
            string[] Parameter = Parameters.Split(';');
            String IdentStr = Parameter[1];
            String DGICert = Parameter[2];
            String ClientCert = Parameter[3];
            String Clientpassword = Parameter[4];
            String ServUri = Parameter[5];

            if (eoName == "WS_eFactura")   // Este es el nombre del external object para filtrar, en este ejemplo EfacturaUy
            {

                // Aca se implementa la configuracion del serviceClient segun corresponda, por ejemplo: 
                ClientBase<ISdtWS_eFactura> svc = serviceClient as ClientBase<ISdtWS_eFactura>;

                //Seteo el protection level por programa 
                svc.Endpoint.Contract.ProtectionLevel = System.Net.Security.ProtectionLevel.Sign;


                //Declaro el objeto donde voy a guardar el Certificado desde el path del archivo, Cliente y DGI 
                System.Security.Cryptography.X509Certificates.X509Certificate2 crtCLI;
                System.Security.Cryptography.X509Certificates.X509Certificate2 crtDGI;

                crtCLI = new System.Security.Cryptography.X509Certificates.X509Certificate2(ClientCert.Trim(), Clientpassword.Trim());
                crtDGI = new System.Security.Cryptography.X509Certificates.X509Certificate2(DGICert.Trim());

                //Seteo al WS los certificados
                svc.ClientCredentials.ClientCertificate.Certificate = crtCLI;
                svc.ClientCredentials.ServiceCertificate.DefaultCertificate = crtDGI;
                svc.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.None;


                //Creo la direccion del WS, que tiene la URI y el DNS(es el CN del certificado de la DGI)
                svc.Endpoint.Address = new System.ServiceModel.EndpointAddress(new Uri(ServUri.Trim()),
                                                                    EndpointIdentity.CreateDnsIdentity(IdentStr.Trim()));


            }
        }
    }