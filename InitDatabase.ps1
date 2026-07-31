# Script para inicializar o resetear la base de datos con seeding
# Uso: .\InitDatabase.ps1
# Uso con reset: .\InitDatabase.ps1 -Reset

param(
	[switch]$Reset = $false
)

$ErrorActionPreference = "Stop"

Write-Host "╔════════════════════════════════════════════════════════╗" -ForegroundColor Cyan
Write-Host "║     Inicializador de Base de Datos - GOP Project      ║" -ForegroundColor Cyan
Write-Host "╚════════════════════════════════════════════════════════╝" -ForegroundColor Cyan
Write-Host ""

# Verificar si dotnet está instalado
try {
	$dotnetVersion = dotnet --version
	Write-Host "✓ .NET instalado: $dotnetVersion" -ForegroundColor Green
}
catch {
	Write-Host "✗ .NET no está instalado o no está en el PATH" -ForegroundColor Red
	exit 1
}

Write-Host ""
Write-Host "📁 Ubicación del proyecto: $(Get-Location)" -ForegroundColor Yellow
Write-Host ""

# Cambiar a la carpeta del Server
Set-Location "GOP\Server"

if ($Reset) {
	Write-Host "🔄 MODO RESET - Se eliminará y recreará la base de datos" -ForegroundColor Yellow
	Write-Host ""
	Write-Host "Eliminando base de datos..." -ForegroundColor Yellow

	try {
		dotnet ef database drop --force --project ..\BD\GOP.BD.csproj 2>&1 | ForEach-Object { Write-Host "  $_" }
		Write-Host "✓ Base de datos eliminada" -ForegroundColor Green
	}
	catch {
		Write-Host "⚠ No había base de datos previa (esto es normal)" -ForegroundColor Yellow
	}
}
else {
	Write-Host "📝 MODO INICIALIZACIÓN - Se creará la base de datos si no existe" -ForegroundColor Yellow
	Write-Host ""
}

Write-Host ""
Write-Host "Ejecutando migraciones..." -ForegroundColor Yellow
try {
	dotnet ef database update --project ..\BD\GOP.BD.csproj 2>&1 | ForEach-Object { Write-Host "  $_" }
	Write-Host "✓ Migraciones completadas" -ForegroundColor Green
}
catch {
	Write-Host "✗ Error al ejecutar las migraciones" -ForegroundColor Red
	Write-Host "$_" -ForegroundColor Red
	exit 1
}

Write-Host ""
Write-Host "╔════════════════════════════════════════════════════════╗" -ForegroundColor Green
Write-Host "║              ✓ BASE DE DATOS LISTA                    ║" -ForegroundColor Green
Write-Host "╚════════════════════════════════════════════════════════╝" -ForegroundColor Green
Write-Host ""

Write-Host "📊 DATOS QUE SE CARGARÁN AUTOMÁTICAMENTE:" -ForegroundColor Cyan
Write-Host ""
Write-Host "  Unidades de Medida:" -ForegroundColor Magenta
Write-Host "    • Metro (MTS), Metro² (MTS2), Metro³ (MTS3)" -ForegroundColor Gray
Write-Host "    • Kilómetro (KM), Litro (LT), Kilogramo (KG)" -ForegroundColor Gray
Write-Host "    • Tonelada (TON), Unidad (UN), Día (DIA), Hora (HRS)" -ForegroundColor Gray
Write-Host ""

Write-Host "  Tipos de Estructura:" -ForegroundColor Magenta
Write-Host "    • Pavimento rigido de Hormigón" -ForegroundColor Gray
Write-Host "    • Hormigón, Asfalto, Adoquín, Ripio" -ForegroundColor Gray
Write-Host ""

Write-Host "  Calles:" -ForegroundColor Magenta
Write-Host "    • Avenida Argentina, Calle San Juan, Avenida Diagonal" -ForegroundColor Gray
Write-Host ""

Write-Host "  Parámetros de Catálogo:" -ForegroundColor Magenta
Write-Host "    • Temperatura, Humedad, Precipitación, Viento, Compacidad" -ForegroundColor Gray
Write-Host ""

Write-Host "  Tipos de Evento:" -ForegroundColor Magenta
Write-Host "    • Clima, Accidente, Parada, HyS, Inspección, Recapacitación" -ForegroundColor Gray
Write-Host ""

Write-Host "  Zonas:" -ForegroundColor Magenta
Write-Host "    • Zona Centro, Zona Norte, Zona Sur" -ForegroundColor Gray
Write-Host ""

Write-Host "  Empresas:" -ForegroundColor Magenta
Write-Host "    • Constructora del Sur S.A." -ForegroundColor Gray
Write-Host "    • Infraestructuras del Neuquén" -ForegroundColor Gray
Write-Host "    • Obras Públicas Patagonia" -ForegroundColor Gray
Write-Host ""

Write-Host "  Roles:" -ForegroundColor Magenta
Write-Host "    • Admin, Usuario, Consultor, BaseDatos, HyS, Zona1, Zona2, Frente, Consulta1, Consulta2" -ForegroundColor Gray
Write-Host ""

Write-Host "  Usuario Admin de Prueba (opcional):" -ForegroundColor Magenta
Write-Host "    • No se crea con datos hardcodeados. Configuralo antes con User Secrets:" -ForegroundColor Gray
Write-Host "      dotnet user-secrets set `"AdminSeed:DNI`" `"...`" --project GOP\Server\GOP.Server.csproj" -ForegroundColor Gray
Write-Host "      dotnet user-secrets set `"AdminSeed:Nombre`" `"...`" --project GOP\Server\GOP.Server.csproj" -ForegroundColor Gray
Write-Host "      dotnet user-secrets set `"AdminSeed:Apellido`" `"...`" --project GOP\Server\GOP.Server.csproj" -ForegroundColor Gray
Write-Host "      dotnet user-secrets set `"AdminSeed:Email`" `"...`" --project GOP\Server\GOP.Server.csproj" -ForegroundColor Gray
Write-Host "      dotnet user-secrets set `"AdminSeed:Telefono`" `"...`" --project GOP\Server\GOP.Server.csproj" -ForegroundColor Gray
Write-Host "      dotnet user-secrets set `"AdminSeed:UserName`" `"...`" --project GOP\Server\GOP.Server.csproj" -ForegroundColor Gray
Write-Host "      dotnet user-secrets set `"AdminSeed:Password`" `"...`" --project GOP\Server\GOP.Server.csproj" -ForegroundColor Gray
Write-Host "    • Si no lo configurás, el seeding omite este paso." -ForegroundColor Gray
Write-Host ""

Write-Host "🚀 PRÓXIMOS PASOS:" -ForegroundColor Yellow
Write-Host "  1. Navega a la carpeta raíz: cd .." -ForegroundColor Gray
Write-Host "  2. Ejecuta la aplicación: dotnet run --project GOP.Server.csproj" -ForegroundColor Gray
Write-Host "  3. Accede a la aplicación en: https://localhost:7297" -ForegroundColor Gray
Write-Host "  4. Inicia sesión con el usuario/contraseña que hayas configurado en AdminSeed" -ForegroundColor Gray
Write-Host ""

Write-Host "💡 NOTAS IMPORTANTES:" -ForegroundColor Cyan
Write-Host "  • El seeding se ejecuta automáticamente al iniciar la aplicación en Development" -ForegroundColor Gray
Write-Host "  • Los datos se crean solo si no existen (operación idempotente)" -ForegroundColor Gray
Write-Host "  • Para resetear nuevamente, usa: .\InitDatabase.ps1 -Reset" -ForegroundColor Gray
Write-Host ""

Set-Location "..\.."
