# Azure AD B2C

This repository stores sample collataral for Azure AD B2C for training / testing purposes only.

## SAMLTEST
The [SAMLTEST](./SAMLTEST) web application is a DotNetCore2 SAML Identity Provider adn Service Provier. 

This applicaiton is designed to be used with Azure AD B2C for testing / training of SAML Policies. 

For a working example of the application see [https://testmysaml.azurewebsites.net/](https://testmysaml.azurewebsites.net/)

To use the application click the "Service Provider" link at the top in the Navigation bar and enter your B2C Tenant name (With or without the .onmicrosoft.com) and then specify your Policy name (with or without the preceeding B2C_1A_)

A Sample B2C Policy can be Generated by clicking the "[B2C Policy](https://testmysaml.azurewebsites.net/B2CPolicy)" link in the Navigation bar at the top of the page. 
<strong>Note:</strong> to use the example policy you will need to upload a SAML Signing Certificate and call it B2C_1A_SAMLCERT)

to Create a Self-Signed Certificate to upload to B2C run the following powershell commands as an <strong>Administrator</strong>

```
$Certificate = New-SelfsignedCertificate -Subject "cn=mysamlsigningcert" -TextExtension @("2.5.29.37={text}1.3.6.1.5.5.7.3.2,1.3.6.1.5.5.7.3.1") -KeyUsage KeyEncipherment, DigitalSignature  -CertStoreLocation "cert:\LocalMachine\My" -Provider "Microsoft Enhanced RSA and AES Cryptographic Provider" -KeyLength 2048 -KeyAlgorithm RSA -KeyExportPolicy Exportable -HashAlgorithm SHA256 -NotAfter (Get-Date).Add(730d)
  
Export-PfxCertificate -Password (ConvertTo-SecureString -String "1234" -Force -AsPlainText) -FilePath "c:\temp\cert.pfx" -Cert $certificate
  
```
