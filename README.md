# dotnetcfdi/credentials

> Library to use eFirma (fiel) and CSD (sellos) from SAT

:us: The documentation of this project is in spanish as this is the natural language for intended audience.

:mexico: La documentación del proyecto está en español porque ese es el lenguaje principal de los usuarios.

Esta librería ha sido creada para poder trabajar con los archivos CSD y FIEL del SAT. De esta forma,
se simplifica el proceso de firmar, verificar firma y obtener datos particulares del archivo de certificado
así como de la llave pública.

- El CSD (Certificado de Sello Digital) es utilizado para firmar Comprobantes Fiscales Digitales.

- La FIEL (o eFirma) es utilizada para firmar electrónicamente documentos (generalmente usando XML-SEC) y
  está reconocida por el gobierno mexicano como una manera de firma legal de una persona física o moral.

Con esta librería no es necesario convertir los archivos generados por el SAT a otro formato,
se pueden utilizar tal y como el SAT los entrega.

## Acerca de dotnetcfdi/credentials

Contiene 3 clases relevantes para usted.

`Certificate` Representa un envoltorio del archivo `.cer`, con esta clase se gestiona todo lo relacionado con el certificado, como obtener número de certificado, versión, fecha de vigencia inicial y fecha de vigencia final, así como la conversión del certificado PKCS#8 DER a PKCS#8 PEM y otros datos asociados al certificado. 

`PrivateKey` Representa un envoltorio del archivo `.key`, con esta clase se gestiona todo lo relacionado con la clave privada, dentro de lo más relevante la conversión de la clave privada PKCS#8 DER a PKCS#8 PEM otros datos asociados. 
 
`Credential` Representa un envoltorio que une tanto el Certificate, como el PrivateKey, de tal forma que lo podemos ver como la FIEL o los CSD en conjunto, es decir un objeto `Credencial` reúne las características del `Certificate`, `PrivateKey` y adiciona algunas otras como firmar, validar una firma y crear archivos PFX. 

Tanto la clase `Certificate` como la clase `PrivateKey` implementan el método `GetPemRepresentation();` que hace la conversión del formato  `PKCS#8 DER` a `PKCS#8 PEM`. Los archivos PEM, son los que utiliza la case `Credential` para construir un archivo `PFX(PKCS#12)`.

## Instalación

Usa [nuget](https://www.nuget.org/)

```shell
Install-Package DotnetCfdi.Credentials -Version 1.1.2
```

## Uso básico del certificado

```csharp
//Creating a certificate instance
var cerPath = @"C:\Users\JESUSMENDOZA\Desktop\cer.cer";
var cerBytes = File.ReadAllBytes(cerPath);
var cerBase64 = Convert.ToBase64String(cerBytes); 
var certificate = new Certificate(cerBase64); //puedes guardar cerBase64 en la db, entonces omite las lineas anteriores y crea el objeto recuperando cerBase64 de la db

//show certificate basic information
MessageBox.Show($@"PlainBase64 {certificate.PlainBase64}");
MessageBox.Show($@"Rfc {certificate.Rfc}");
MessageBox.Show($@"Razón social {certificate.Organization}");
MessageBox.Show($@"SerialNumber {certificate.SerialNumber}");
MessageBox.Show($@"CertificateNumber {certificate.CertificateNumber}");
MessageBox.Show($@"ValidFrom {certificate.ValidFrom}");
MessageBox.Show($@"ValidTo {certificate.ValidTo}");
MessageBox.Show($@"IsFiel { certificate.IsFiel()}");
MessageBox.Show($@"IsValid { certificate.IsValid()}"); // ValidTo > Today

//Converts X.509 DER base64 or X.509 DER to X.509 PEM
var pemCertificate = certificate.GetPemRepresentation();
File.WriteAllText("MyPemCertificate.pem", pemCertificate);

```

## Uso básico de la clave privada

```csharp
//Creating a private key instance
var keyPath = @"C:\Users\JESUSMENDOZA\Desktop\key.key";
var keyBytes = File.ReadAllBytes(keyPath);
var keyBase64 = Convert.ToBase64String(keyBytes);
var privateKey = new PrivateKey(keyBase64, "YourPassword"); //puedes guardar keyBase64 en la db, entonces omite las lineas anteriores y crea el objeto recuperandolo db

//Converts PKCS#8 DER private key to PKCS#8 PEM
var PemPrivateKey = privateKey.GetPemRepresentation();
File.WriteAllText("MyPemPrivateKey.pem", PemPrivateKey);
```

## Uso básico del objeto credential

```csharp
//Create a credential instance, certificate and privatekey previously created.
var fiel = new Credential(certificate, privateKey);

var dataToSign = "Hello world"; //replace with cadena original

//SignData
var signedBytes = fiel.SignData(dataToSign);

//Verify signature
var originalDataBytes = Encoding.UTF8.GetBytes(dataToSign);
var isValid = fiel.VerifyData(originalDataBytes, signedBytes);

//Create pfx file
var pxfBytes = fiel.CreatePFX();
File.WriteAllBytes("MyPFX.pfx", pxfBytes);

//basic info
MessageBox.Show($@"CredentialType { fiel.CredentialType}");  // Enum: Fiel || Csd
MessageBox.Show($@"IsValidFiel { fiel.IsValidFiel()}");      // True when (certificate.ValidTo > Today and  CredentialType == Fiel)

```




## Acerca de los archivos de certificado y llave privada

Los archivos de certificado vienen en formato `X.509 DER` y los de llave privada en formato `PKCS#8 DER`.
Ambos formatos no se pueden interpretar directamente en C#, sin embargo sí lo pueden hacer
en el formato compatible [`PEM`](https://en.wikipedia.org/wiki/Privacy-Enhanced_Mail).

Esta librería tiene la capacidad de hacer esta conversión internamente (sin `openssl`), pues solo consiste en codificar
a `base64`, en renglones de 64 caracteres y con cabeceras específicas para certificado y llave privada.

De esta forma, para usar el certificado `AAA010101AAA.cer` o la llave privada `AAA010101AAA.key` provistos por
el SAT, no es necesario convertirlos con `openssl` y la librería los detectará correctamente.

Para entender más de los formatos de llaves privadas se puede consultar la siguiente liga:
<https://github.com/kjur/jsrsasign/wiki/Tutorial-for-PKCS5-and-PKCS8-PEM-private-key-formats-differences>

## Compatibilidad

Esta librería se mantendrá compatible con al menos la versión con
[soporte LTS de dotnet](https://dotnet.microsoft.com/en-us/download/dotnet) más reciente.

También utilizamos [Versionado Semántico 2.0.0](docs/SEMVER.md) por lo que puedes usar esta librería
sin temor a romper tu aplicación.

Actualmente compatible con `.NET 6`, winforms, console y web. 

## Contribuciones

Las contribuciones con bienvenidas. Por favor lee [CONTRIBUTING][] para más detalles
y recuerda revisar el archivo de tareas pendientes [TODO][] y el archivo [CHANGELOG][].

## Roadmap Features 
- [x] Convertir certificados del formato original SAT   (X.509 DER ) a X.509 PEM
- [x] Convertir clave privada  del formato original SAT (PKCS#8 DER) a PKCS#8 PEM
- [x] Crear archivo .PFX (PKCS#12) a partir de los archivos X.509 PEM y PKCS#8 PEM  (Útil para consumir ws descarga masiva XML y en algunos PACs para cancelar CFDIs)
- [x] Firmar datos (sellar con el algotitmo `SHA256withRSA`)
- [x] Verificar datos firmados
- [x] Computar y validar hash (digest usado en la descaraga masiva xml)
- [ ] Persistencia de los archivos CSD y FIEL utilizando entity framework core y bases de datos.

## Copyright and License

The `dotnet/credentials` library is copyright © [dotnetcfdi](https://www.dotnetcfdi.com/)
and licensed for use under the MIT License (MIT). Please see [LICENSE][] for more information.

