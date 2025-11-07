# Suppliers API - Sistema de Reportes CFDI

API REST desarrollada en .NET 8 para la generación de reportes de CFDIs (Comprobantes Fiscales Digitales por Internet) del sistema fiscal mexicano.

## 🚀 Características

- **Generación de Reportes Excel**: Exportación de datos de CFDIs a formato Excel
- **Reporte CONTPAQ**: Reporte personalizado con filtros avanzados
- **Autenticación JWT**: Sistema de tokens para seguridad
- **Restricción por IP**: Control de acceso basado en direcciones IP
- **Base de datos SQL Server**: Almacenamiento de comprobantes fiscales

## 📋 Requisitos

- .NET 8.0 SDK
- SQL Server (Express o superior)
- Base de datos `DescargaCfdiGFP`

## 🔧 Configuración

### 1. Restaurar paquetes NuGet

El proyecto usa un feed personalizado de NuGet. El archivo `NuGet.config` ya está configurado.

```bash
dotnet restore
```

### 2. Configurar la base de datos

Actualiza la cadena de conexión en `Suppliers.API/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "SQLConnectionMain": "Server=localhost; Database=DescargaCfdiGFP; Integrated Security=true;TrustServerCertificate=true"
  }
}
```

### 3. Compilar el proyecto

```bash
dotnet build
```

### 4. Ejecutar la API

```bash
dotnet run --project Suppliers.API
```

La API estará disponible en: `http://localhost:5010`

## 📡 Endpoints

### Reporte CONTPAQ

**POST** `/Report/ContpaqExcel`

Genera un reporte Excel de CFDIs con filtros personalizados.

**Request Body:**
```json
{
  "FolioFiscal": "",
  "FechaInicial": "01/01/2024",
  "FechaFinal": "31/12/2024",
  "TipoComprobante": "I",
  "Rfc": "",
  "Serie": "",
  "Folio": "",
  "Download": true
}
```

**Tipos de Comprobante:**
- `I` - Ingreso
- `E` - Egreso
- `T` - Traslado
- `N` - Nómina
- `P` - Pago

**Response:**
- Si `Download: true` - Descarga el archivo Excel
- Si `Download: false` - Retorna la ruta del archivo generado

### Reporte de Ejemplo

**POST** `/Report/ExampleExcel`

Genera un reporte de ejemplo.

### Autenticación

**GET** `/Auth/Token/{usrId}`

Obtiene un token JWT (requiere IP autorizada).

## 🗂️ Estructura del Proyecto

```
suppliers.api/
├── Suppliers.API/          # Capa de presentación (API)
│   ├── Controllers/        # Controladores REST
│   ├── Attributes/         # Filtros personalizados
│   └── appsettings.json    # Configuración
│
└── Suppliers.BL/           # Capa de lógica de negocio
    ├── Bl/                 # Lógica de negocio
    ├── Entities/           # Entidades de base de datos
    ├── Reports/            # Generadores de reportes
    │   ├── Contpaq/        # Reporte CONTPAQ
    │   └── Example/        # Reporte de ejemplo
    ├── Helpers/            # Utilidades
    └── Interfaces/         # Contratos
```

## 🔐 Seguridad

### Restricción por IP

Configura las IPs autorizadas en `appsettings.json`:

```json
{
  "AllowedIpsConfig": [
    {
      "Name": "Local",
      "Ipv4": "127.0.0.1"
    }
  ]
}
```

### JWT

Configura el token JWT en `appsettings.json`:

```json
{
  "JWT": {
    "Key": "TU_CLAVE_SECRETA_AQUI",
    "Issuer": "http://btob.com.mx",
    "Expire": 480
  }
}
```

## 📊 Base de Datos

El proyecto utiliza Entity Framework Core con SQL Server. Las entidades principales son:

- `CfdiComprobante` - Comprobantes fiscales
- `CfdiEmisor` - Datos del emisor
- `CfdiReceptor` - Datos del receptor
- `CfdiConcepto` - Conceptos/productos
- `NominaDetalle` - Detalles de nómina
- `PagosDetalle` - Detalles de pagos
- `ComercioExteriorDetalle` - Comercio exterior

## 🛠️ Tecnologías

- **.NET 8.0** - Framework principal
- **Entity Framework Core** - ORM
- **SQL Server** - Base de datos
- **NPOI** - Generación de archivos Excel
- **QuestPDF** - Generación de PDFs
- **Serilog** - Logging
- **JWT** - Autenticación

## 📝 Uso con archivo .http

El proyecto incluye `Suppliers.API.http` con ejemplos de peticiones. Usa la extensión REST Client en VS Code.

## 🤝 Contribuir

1. Fork el proyecto
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

## 📄 Licencia

Este proyecto es privado y confidencial.

## 👥 Autores

- B2B Consultores

## 📞 Soporte

Para soporte, contacta al equipo de desarrollo.
