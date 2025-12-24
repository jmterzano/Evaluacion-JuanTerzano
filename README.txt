USO:

La API recibe un JSON con las rutas de entrada y salida.

El campo inputJsonPath puede enviarse vacío. En ese caso, la aplicación
utiliza un archivo de entrada configurado por defecto. También es posible
especificar un archivo diferente indicando su ruta.

-------------------------------------------------------------------------------

EJEMPLO DE REQUEST:

{
  "inputJsonPath": "",
  "summaryJsonPath": "C:\\ruta\\de\\salida\\summary.json",
  "csvOutputPath": "C:\\ruta\\de\\salida\\output.csv",
  "xmlOutputPath": "C:\\ruta\\de\\salida\\output.xml"
}

-------------------------------------------------------------------------------

NOTAS:

- inputJsonPath puede dejarse vacío para usar el archivo de entrada por defecto.
- Los archivos de salida solo se generan si se envía su path.
- El nombre y la ubicación de cada archivo son completamente configurables.

-------------------------------------------------------------------------------

TECNOLOGÍAS:

- C#
- .NET 8
- Clean Architecture
- Programación asíncrona
- Visual Studio