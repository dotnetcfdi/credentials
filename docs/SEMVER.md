# SEMVER

Respetamos el estándar [Versionado Semántico 2.0.0](https://semver.org/lang/es/).

En resumen, [SemVer](https://semver.org/) es un sistema de versiones de tres componentes `X.Y.Z`
que nombraremos ` Breaking . Feature . Fix `, donde:

- `Breaking`: Rompe la compatibilidad de código con versiones anteriores.
- `Feature`: Agrega una nueva característica que es compatible con lo anterior.
- `Fix`: Incluye algún cambio (generalmente correcciones) que no agregan nueva funcionalidad.

## nuget

El gestor de dependencias en proyectos para .NET [nuget](https://www.nuget.org/) 
usa las [reglas](https://docs.microsoft.com/en-us/nuget/concepts/package-versioning) de versionado semántico
para instalar y actualizar paquetes.

Dicho esto, los desarrolladores de paquetes generalmente siguen las convenciones de nomenclatura reconocidas

- `Major`: Rompe la compatibilidad de código con versiones anteriores.
- `Minor`: Agrega una nueva característica que es compatible con lo anterior.
- `Patch`: Incluye algún cambio (generalmente correcciones) que no agregan nueva funcionalidad.
- `-Suffix`: (opcional) un guión seguido de una cadena que indica una versión preliminar.

Esto significa que:

| Version       | Descripción                                          |
| ------------- |:----------------------------------------------------:|
| 1.0.1         | Esta versión no es actualizable a 2.3.20             |
| 2.3.20        | Esta versión si es actualizable a 2.13.1             |
| 2.5.1         | Esta versión si es actualizable a 3.6.44-rc          |
| 3.6.44-rc     | Esta versión version preliminar (release candidate)  | 


## Versiones 0.x.y no rompen compatibilidad

Las versiones que inician con cero, por ejemplo `0.y.z`, no se ajustan a las reglas de versionado.
Se considera que estas versiones son previas a la madurez del proyecto y por lo tanto
introducen cambios sin previo aviso.

Sin embargo, nos apegaremos a `[ 0 ] . [ Breaking ] . [ Feature || Fix ]`. Lo que significa que `0.3.0`
no es compatible con `0.2.15` pero `0.3.4` sí es compatible con `0.3.0`.


