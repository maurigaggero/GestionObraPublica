# 🏗️ Inicialización de Base de Datos - GOP (Gestión Obra Pública)

## 📋 Descripción
Este documento explica cómo inicializar o resetear la base de datos del proyecto GOP con datos de prueba automáticos.

## 🚀 Inicio Rápido

### Opción 1: Script PowerShell (Recomendado)

**Inicializar por primera vez:**
```powershell
.\InitDatabase.ps1
```

**Resetear completamente:**
```powershell
.\InitDatabase.ps1 -Reset
```

### Opción 2: Comandos Manuales

**Crear/Actualizar la base de datos:**
```bash
cd GOP\Server
dotnet ef database update --project ..\BD\GOP.BD.csproj
cd ..\..
```

**Eliminar y recrear (Reset):**
```bash
cd GOP\Server
dotnet ef database drop --force --project ..\BD\GOP.BD.csproj
dotnet ef database update --project ..\BD\GOP.BD.csproj
cd ..\..
```

## 📊 Datos que se crean automáticamente

### ✅ Unidades de Medida (10)
- Metro (MTS)
- Metro Cuadrado (MTS2)
- Metro Cúbico (MTS3)
- Kilómetro (KM)
- Litro (LT)
- Kilogramo (KG)
- Tonelada (TON)
- Unidad (UN)
- Día (DIA)
- Hora (HRS)

### ✅ Tipos de Estructura (5)
- Pavimento rigido de Hormigón
- Hormigón
- Asfalto
- Adoquín
- Ripio

### ✅ Calles (3)
- Avenida Argentina
- Calle San Juan
- Avenida Diagonal

### ✅ Parámetros de Catálogo (5)
- TEMP: Temperatura ambiente (-40°C a 50°C)
- HUME: Humedad relativa (0-100%)
- PREC: Precipitación (0-500mm)
- VIEN: Velocidad del viento (0-100 km/h)
- COMP: Compacidad del suelo (0-100%)

### ✅ Tipos de Evento (7)
- CLIMA: Eventos climáticos
- ACCE: Accidente o incidente
- PARA: Parada de obra
- HyS: Higiene y Seguridad
- INSP: Inspección de obra
- RECA: Recapacitación
- OTRO: Otros eventos

### ✅ Zonas (3)
- Zona Centro (CodigoZona: Z001)
- Zona Norte (CodigoZona: Z002)
- Zona Sur (CodigoZona: Z003)

### ✅ Empresas (3)
- Constructora del Sur S.A. (CUIT: 30701234567)
- Infraestructuras del Neuquén (CUIT: 30702345678)
- Obras Públicas Patagonia (CUIT: 30703456789)

### ✅ Roles (10)
- Admin
- Usuario
- Consultor
- BaseDatos
- HyS
- Zona1
- Zona2
- Frente
- Consulta1
- Consulta2

### ✅ Usuario Admin de Prueba (opcional)
El usuario admin de prueba **no** se crea con datos hardcodeados: hay que configurarlo
con User Secrets (así no quedan datos personales en el repo) antes de correr el seeding.
Si no se configura, el seeding simplemente omite este paso.

```powershell
cd GOP\Server
dotnet user-secrets set "AdminSeed:DNI" "..."
dotnet user-secrets set "AdminSeed:Nombre" "..."
dotnet user-secrets set "AdminSeed:Apellido" "..."
dotnet user-secrets set "AdminSeed:Email" "..."
dotnet user-secrets set "AdminSeed:Telefono" "..."
dotnet user-secrets set "AdminSeed:UserName" "..."
dotnet user-secrets set "AdminSeed:Password" "..."
cd ..\..
```

- **Rol asignado:** Admin
- **Estado:** Activo

## 🔄 Proceso de Seeding

El seeding se ejecuta automáticamente cuando:

1. **En Development:**
   - Al iniciar la aplicación
   - Si la base de datos no existe
   - Si los datos no existen (verificación idempotente)

2. **Pasos del Seeding:**
   - Crear roles de Identity
   - Crear unidades de medida
   - Crear tipos de estructura
   - Crear calles
   - Crear parámetros de catálogo
   - Crear tipos de evento
   - Crear zonas
   - Crear empresas
   - Crear persona
   - Crear usuario admin

## 🛠️ Solución de Problemas

### Error: "The database provider was not specified"
```bash
# Asegúrate de ejecutar desde la raíz del proyecto
cd f:\repos\GOP
.\InitDatabase.ps1
```

### Error: "Unable to create an object of type 'BDContext'"
```bash
# Verifica que connectionString está en appsettings.json
# Verifica que SQL Server está corriendo
```

### Error: "The specified module could not be found"
```bash
# Necesitas tener .NET 10 SDK instalado
dotnet --version
```

### ¿Los datos no aparecen?
1. Verifica que el seeding completó sin errores en la consola
2. Ejecuta con `-Reset` para recrear completamente:
   ```powershell
   .\InitDatabase.ps1 -Reset
   ```

## 📝 Configuración de Conexión

La cadena de conexión se define en:
```json
// GOP/Server/appsettings.json
"ConnectionStrings": {
  "conn": "Server=(localdb)\\mssqllocaldb;Database=GOP;Trusted_Connection=true;"
}
```

Para cambiar la base de datos:
1. Edita `appsettings.json`
2. Actualiza la cadena de conexión
3. Ejecuta el script nuevamente

## 🎯 Próximos Pasos

1. **Inicializa la BD:**
   ```powershell
   .\InitDatabase.ps1
   ```

2. **Ejecuta la aplicación:**
   ```bash
   dotnet run --project GOP\Server\GOP.Server.csproj
   ```

3. **Accede a la aplicación:**
   - URL: https://localhost:7297
   - Usuario/contraseña: los que hayas configurado en `AdminSeed` vía User Secrets

4. **Comprobación:**
   - Deberías poder acceder sin errores
   - Verifica que los datos aparecen en las listas
   - Intenta crear una nueva Persona

## 📞 Soporte

Si encuentras problemas:
1. Revisa los logs en la consola
2. Verifica que tienes SQL Server instalado
3. Asegúrate de estar en la carpeta correcta
4. Verifica permisos de lectura/escritura en la carpeta del proyecto

---

**Última actualización:** 2024
**Versión:** .NET 10
**Base de Datos:** SQL Server (LocalDB)
