Comando de build para windows
dotnet publish -f net8.0-windows10.0.19041.0 -c Release -p:WindowsPackageType=MSIX -p:AppxBundle=Always -p:AppxPackageSigningEnabled=true


Gerar certificado digital no powershel

$cert = New-SelfSignedCertificate -DnsName "CN=MoisesFerreiraApp" -CertStoreLocation "Cert:\LocalMachine\My" -KeyUsage DigitalSignature -Type CodeSigningCert -KeyLength 2048 -KeyAlgorithm "RSA" -HashAlgorithm "SHA256" -KeyExportPolicy Exportable -NotAfter (Get-Date).AddYears(10)
$pwd = ConvertTo-SecureString -String "123456" -Force -AsPlainText
Export-PfxCertificate -Cert $cert -FilePath "C:\Users\moisesferreira\Downloads\MoisesFerreiraApp.pfx" -Password $pwd

