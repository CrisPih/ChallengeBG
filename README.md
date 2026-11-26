# ChallengeBG - Inventario de productos - Cristina Pihuave

Resolución del challenge Full Stack con arquitectura hexagonal. Backend en .NET 10 y frontend en Angular 22.

## Arquitectura del backend

- ChallengeBG.Domain: entidades.
- ChallengeBG.Application: DTOs, puertos (interfaces) y servicios con las reglas de negocio.
- ChallengeBG.Infrastructure: Entity Framework, repositorios, JWT y BCrypt.
- ChallengeBG.Api: controladores, Swagger, CORS, Serilog y manejo de errores.

## Cómo ejecutar

1. Ejecutar `database/script.sql` en SQL Server.
2. Revisar la cadena de conexión en `ChallengeBGApi/ChallengeBG.Api/appsettings.json`.
3. Abrir `ChallengeBGApi/ChallengeBGApi.slnx` en Visual Studio y ejecutar el perfil http. Swagger se abre en `http://localhost:5257/swagger`.
4. En `ChallengeBGApp` ejecutar `npm install` y luego `ng serve -o`.

Usuario de prueba: `admin` - Clave: `Password12345#`