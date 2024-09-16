# Circuit-Breaker en C# (.NET Core) Javier Enrique Joya

![.NET Core-C#](https://img.shields.io/badge/.NET_Core-C%23-blue)

Prueba de concepto sobre el [patrón de arquitectura Circuit Breaker](https://learn.microsoft.com/es-es/azure/architecture/patterns/circuit-breaker) controla los errores cuya recuperación puede tardar una cantidad variable de tiempo durante la conexión a un recurso o servicio remoto. Puede mejorar la estabilidad y la resistencia de una aplicación.

## Para poder empezar

### Instalar [visual studio 2022](https://visualstudio.microsoft.com/es/vs/community/)

Esto es necesario para poder abrir la solución.

### Instalar [.Net Core (Version 8)](https://dotnet.microsoft.com/en-us/download)

Es el core usado para la aplicación y el api generada.

### Compilar y ejecutar aplicación

Una vez abierto el proyecto en el visual studio, se debe configurar la apertura en paralelo del api y el sitio web:
- Clic derecho en la Solución-> Propiedades -> Propiedades Comunes -> Proyecto de inicio -> Proyectos de inicio múltiples. Y debe quedar como se muestra en la imagen:
  ![imagen](https://github.com/user-attachments/assets/77f8ce80-d72e-4807-ab6e-e96a6cb6d078)

- Despues de configurar la ejecución de los 2 proyectos se debe dar clic en Iniciar:
![imagen](https://github.com/user-attachments/assets/91c3b7cf-6213-4a39-9ced-740b18a71e43)

Esta acción ejecutará los 2 proyectos al mismo tiempo (El sitio Web y el API)

**Nota: Se debe configurar la URL Base generada por el API (Se identifica por que usa Swagger) en el archivo appsettings.json**
![imagen](https://github.com/user-attachments/assets/cd2b3b34-1463-4d02-8db3-37d9564618a2)

![imagen](https://github.com/user-attachments/assets/064d26b5-7dbd-449a-9fa8-7d329efdf298)




## Pruebas

### Descripción de la prueba

La aplicación web muestra información random de usuarios que consume de un [api externa](https://randomuser.me/api/), la idea de esta prueba es simular el fallo de dicha api y evitar que se genere un ciclo de reintentos con error.

- El circuito estará abierto mientras no se capturen errores de el llamado al api por mas de 5 veces seguidas y el tiempo de ruptura del circuito será de 10 segundos.
  ![imagen](https://github.com/user-attachments/assets/221ead2e-7714-4750-b860-f359a1651263)


El circuito en la aplicación maneja 3 estados:
1. **Break**: Este estado nos indica que se han cumplido la cantidad de errores configurados capturados en el llamado del API.
2. **Closed**: Este estado nos indica que no se han capturado la cantidad de errores continuos configurados en el sistema y el API funciona correctamente.
3. **Half-Open**: Este estado es un intermedio en el cual se permiten ejecuciones al API para verificar su estado y su posible cierre de circuito.

### Objetivo de la prueba
El objetivo principal de estas pruebas es mostrar el funcionamiento interno en la prueba de concepto aplicando la configuración correspondiente con el patrón Circuit Breaker.

### Pasos implementados para llevar a cabo la prueba 
- Para ver el ciclo de pruebas funcionales puede ver este [video](https://github.com/javierjmva2/EvidenciaM1DiplomadoArquitectura/blob/main/Evidencia%20Modulo%201%20Circuit%20Breaker.mp4)
- 
- El primer paso es obtener la información Random de un usuario (En este caso todo está funcionando correctamente):
  ![imagen](https://github.com/user-attachments/assets/ad47dedb-611d-4a4c-8386-40556451749e)
  
- El siguiente paso es simular un error en el API externa, para esto desde el swagger se ejecuta el Endpoint encargado de asignar una variable que nos marcará un error simulado:
  ![imagen](https://github.com/user-attachments/assets/b756cabe-95fb-40c1-a15e-8d8219c1de09)

- El paso de verificación del circuito es ejecutar nuevamente la consulta de un usuario Random (Lo cual arrojará error), cabe recordar que se debe ejecutar 5 veces para que la aplicación rompa el circuito:
  ![imagen](https://github.com/user-attachments/assets/ec1c2ac6-c0a9-4691-a2d2-481615a2f4ee)

- El paso final es deshabilitar la simulación del error y ver como se comporta la aplicación al intentar obtener nuevamente un usuario Random (El circuito se volverá a cerrar y funcionará con normalidad)
  - Deshabilitar la simulación del error:
    ![imagen](https://github.com/user-attachments/assets/fb942118-e8a3-44fc-bc9d-e8a11d412742)
    
  - Ejecutar con normalidad la aplicación.



### Tecnologías usadas en la prueba (especifique lenguajes, librerías)

![.NET Core-C#](https://img.shields.io/badge/.NET_Core-C%23-blue)
- .NET Core 8
- Polly (Nuget para la configuración del Circuit Breaker)
- [Api Usuarios Random](https://randomuser.me/api/)

### Resultados
Despues de esta prueba de concepto se puede observar que este patrón permite tener un seguimiento constante al estado de nuestra api. Permitiendo tomar las medidas necesarias para su correcto funcionamiento y evitando sobre cargas innecesarias.

### Conclusiones 
Este patrón funciona muy bien como un proxy que supervisa los posibles errores en las operaciones y dependiendo su configuración tiene la posibilidad de permitir dichas ejecuciones o devolver inmediatamente un error.
Esto da a entender que el patrón es muy importante en la implementación de estrategias y medición de disponibilidad de uno o mas servicios, también incluyendo costos de operación (Ejecucinoes innecesarias a servicios en falla).
