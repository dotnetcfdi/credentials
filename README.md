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

## Instalación

Usa [nuget](https://www.nuget.org/)

```shell
Install-Package Credentials
```

## Ejemplo básico de uso

```csharp
// algun codigo demo...

```

## Acerca de los archivos de certificado y llave privada

Los archivos de certificado vienen en formato `X.509 DER` y los de llave privada en formato `PKCS#8 DER`.
Ambos formatos no se pueden interpretar directamente en PHP (con `ext-openssl`), sin embargo sí lo pueden hacer
en el formato compatible [`PEM`](https://en.wikipedia.org/wiki/Privacy-Enhanced_Mail).

Esta librería tiene la capacidad de hacer esta conversión internamente (sin `openssl`), pues solo consiste en codificar
a `base64`, en renglones de 64 caracteres y con cabeceras específicas para certificado y llave privada.

De esta forma, para usar el certificado `AAA010101AAA.cer` o la llave privada `AAA010101AAA.key` provistos por
el SAT, no es necesario convertirlos con `openssl` y la librería los detectará correctamente.

### Crear un objeto de certificado `Certificate`

El objeto `Certificate` no se creará si contiene datos no válidos.

El SAT entrega el certificado en formato `X.509 DER`, por lo que internamente se puede convertir a `X.509 PEM`.
También es frecuente usar el formato `X.509 DER base64`, por ejemplo, en el atributo `Comprobante@Certificado`
o en las firmas XML, por este motivo, los formatos soportados para crear un objeto `Certificate` son
`X.509 DER`, `X.509 DER base64` y `X.509 PEM`.

- Para abrir usando un archivo local: `$certificate = Certificate::openFile($filename);`
- Para abrir usando una cadena de caracteres: `$certificate = new Certificate($content);`
  - Si `$content` es un certificado en formato `X.509 PEM` con cabeceras ese se utiliza.
  - Si `$content` está totalmente en `base64`, se interpreta como `X.509 DER base64` y se formatea a `X.509 PEM`
  - En otro caso, se interpreta como formato `X.509 DER`, por lo que se formatea a `X.509 PEM`.

### Crear un objeto de llave privada `PrivateKey`

El objeto `PrivateKey` no se creará si contiene datos no válidos.

En SAT entrega la llave en formato `PKCS#8 DER`, por lo que internamente se puede convertir a `PKCS#8 PEM`
(con la misma contraseña) y usarla desde PHP.

Una vez abierta la llave también se puede cambiar o eliminar la contraseña, creando así un nuevo objeto `PrivateKey`.

- Para abrir usando un archivo local: `$key = PrivateKey::openFile($filename, $passPhrase);`
- Para abrir usando una cadena de caracteres: `$key = new PrivateKey($content, $passPhrase);`
  - Si `$content` es una llave privada en formato `PEM` (`PKCS#8` o `PKCS#5`) se utiliza.
  - En otro caso, se interpreta como formato `PKCS#8 DER`, por lo que se formatea a `PKCS#8 PEM`.

Notas de tratamiento de archivos `DER`:

- Al convertir `PKCS#8 DER` a `PKCS#8 PEM` se determina si es una llave encriptada si se estableció
  una contraseña, si no se estableció se tratará como una llave plana (no encriptada).
- No se sabe reconocer de forma automática si se trata de un archivo `PKCS#5 DER` por lo que este
  tipo de llave se deben convertir *manualmente* antes de intentar abrirlos, su cabecera es `RSA PRIVATE KEY`.
- A diferencia de los certificados que pueden interpretar un formato `DER base64`, la lectura de llave
  privada no hace esta distinción, si desea trabajar con un formato sin caracteres especiales use `PEM`.

Para entender más de los formatos de llaves privadas se puede consultar la siguiente liga:
<https://github.com/kjur/jsrsasign/wiki/Tutorial-for-PKCS5-and-PKCS8-PEM-private-key-formats-differences>

## Compatibilidad

Esta librería se mantendrá compatible con al menos la versión con
[soporte LTS de dotnet](https://dotnet.microsoft.com/en-us/download/dotnet) más reciente.

También utilizamos [Versionado Semántico 2.0.0](docs/SEMVER.md) por lo que puedes usar esta librería
sin temor a romper tu aplicación.

## Contribuciones

Las contribuciones con bienvenidas. Por favor lee [CONTRIBUTING][] para más detalles
y recuerda revisar el archivo de tareas pendientes [TODO][] y el archivo [CHANGELOG][].

## Roadmap Features 
- [x] Convertir certificados del formato original SAT   (X.509 DER ) a X.509 PEM
- [x] Convertir clave privada  del formato original SAT (PKCS#8 DER) a PKCS#8 PEM
- [x] Crear archivo .PFX (PKCS#12) a partir de los archivos X.509 PEM y PKCS#8 PEM  (Útil para consumir ws descarga masiva XML y en algunos PACs para cancelar CFDIs)
- [x] Firmar datos (sellar con el algotitmo `SHA256withRSA`)
- [x] Verificar datos firmados
- [ ] Persistencia de los archivos CSD y FIEL utilizando entity framework core y bases de datos.


## Copyright and License

The `dotnet/Credentials` library is copyright © [dotnetCfdi](https://www.dotnetcfdi.com/)
and licensed for use under the MIT License (MIT). Please see [LICENSE][] for more information.

