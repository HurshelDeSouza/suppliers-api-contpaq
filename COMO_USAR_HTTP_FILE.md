# Cómo Usar el Archivo Suppliers.API.http

## ✅ Verificación: El archivo funciona correctamente

Todas las peticiones han sido probadas y funcionan al 100%:
- ✅ ExampleExcel Report - Status 200
- ✅ ContpaqExcel Report (sin filtros) - Status 200
- ✅ ContpaqExcel Report (con filtros) - Status 200

## 📋 Métodos para Probar el Archivo

### **Método 1: REST Client en VS Code (Recomendado)**

#### Paso 1: Instalar la extensión
1. Abre VS Code
2. Presiona `Ctrl+Shift+X` (o `Cmd+Shift+X` en Mac)
3. Busca "REST Client"
4. Instala la extensión de **Huachao Mao**

#### Paso 2: Usar el archivo
1. Abre el archivo `Suppliers.API/Suppliers.API.http`
2. Verás botones **"Send Request"** sobre cada petición HTTP
3. Haz clic en cualquier botón "Send Request"
4. La respuesta aparecerá en una nueva pestaña

**Ejemplo visual:**
```http
### ContpaqExcel Report - Sin filtros
POST {{SuppliersAPI_HostAddress}}/Report/ContpaqExcel/  👈 Click "Send Request" aquí
Content-Type: application/json

{
  "FolioFiscal": "",
  ...
}
```

#### Ventajas:
- ✅ Interfaz visual integrada en VS Code
- ✅ Historial de peticiones
- ✅ Formato automático de respuestas JSON
- ✅ Variables reutilizables

---

### **Método 2: PowerShell**

Copia y pega estos comandos en PowerShell:

#### Test 1: ExampleExcel
```powershell
$body = '{}'
Invoke-WebRequest -Uri "http://localhost:5010/Report/ExampleExcel/" `
    -Method POST `
    -Body $body `
    -ContentType "application/json" `
    -UseBasicParsing
```

#### Test 2: ContpaqExcel (sin filtros)
```powershell
$body = @{
    FolioFiscal = ""
    FechaInicial = ""
    FechaFinal = ""
    TipoComprobante = $null
    Rfc = ""
    Serie = ""
    Folio = ""
    Download = $true
} | ConvertTo-Json

Invoke-WebRequest -Uri "http://localhost:5010/Report/ContpaqExcel/" `
    -Method POST `
    -Body $body `
    -ContentType "application/json" `
    -UseBasicParsing `
    -OutFile "reporte.xlsx"
```

#### Test 3: ContpaqExcel (con filtros)
```powershell
$body = @{
    FolioFiscal = ""
    FechaInicial = "01/01/2024"
    FechaFinal = "31/12/2024"
    TipoComprobante = "I"
    Rfc = ""
    Serie = ""
    Folio = ""
    Download = $true
} | ConvertTo-Json

Invoke-WebRequest -Uri "http://localhost:5010/Report/ContpaqExcel/" `
    -Method POST `
    -Body $body `
    -ContentType "application/json" `
    -UseBasicParsing `
    -OutFile "reporte_filtrado.xlsx"
```

---

### **Método 3: cURL (Terminal)**

#### Test 1: ExampleExcel
```bash
curl -X POST http://localhost:5010/Report/ExampleExcel/ \
  -H "Content-Type: application/json" \
  -d '{}'
```

#### Test 2: ContpaqExcel (sin filtros)
```bash
curl -X POST http://localhost:5010/Report/ContpaqExcel/ \
  -H "Content-Type: application/json" \
  -d '{"FolioFiscal":"","FechaInicial":"","FechaFinal":"","TipoComprobante":null,"Rfc":"","Serie":"","Folio":"","Download":true}' \
  --output reporte.xlsx
```

#### Test 3: ContpaqExcel (con filtros)
```bash
curl -X POST http://localhost:5010/Report/ContpaqExcel/ \
  -H "Content-Type: application/json" \
  -d '{"FolioFiscal":"","FechaInicial":"01/01/2024","FechaFinal":"31/12/2024","TipoComprobante":"I","Rfc":"","Serie":"","Folio":"","Download":true}' \
  --output reporte_filtrado.xlsx
```

---

### **Método 4: Postman**

1. Abre Postman
2. Crea una nueva petición POST
3. URL: `http://localhost:5010/Report/ContpaqExcel/`
4. Headers: `Content-Type: application/json`
5. Body (raw, JSON):
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
6. Click "Send"

---

## 📝 Estructura del Archivo .http

```http
# Variable global
@SuppliersAPI_HostAddress = http://localhost:5010

# Separador de peticiones
###

# Comentario
# TipoComprobante: "I" = Ingreso, "E" = Egreso, etc.

# Petición HTTP
POST {{SuppliersAPI_HostAddress}}/Report/ContpaqExcel/
Content-Type: application/json

# Body JSON
{
  "FolioFiscal": "",
  "Download": true
}
```

## 🎯 Filtros Disponibles

| Filtro | Tipo | Formato | Ejemplo |
|--------|------|---------|---------|
| FolioFiscal | string | UUID | "A1B2C3D4-..." |
| FechaInicial | string | dd/MM/yyyy | "01/01/2024" |
| FechaFinal | string | dd/MM/yyyy | "31/12/2024" |
| TipoComprobante | string | I, E, T, N, P | "I" |
| Rfc | string | RFC | "XAXX010101000" |
| Serie | string | Texto | "CC" |
| Folio | string | Texto | "523987" |
| Download | boolean | true/false | true |

### Tipos de Comprobante:
- **"I"** = Ingreso
- **"E"** = Egreso
- **"T"** = Traslado
- **"N"** = Nómina
- **"P"** = Pago

## 🔍 Solución de Problemas

### Error: "Connection refused"
**Causa:** El servidor no está corriendo  
**Solución:** Ejecuta `dotnet run --project Suppliers.API`

### Error: "404 Not Found"
**Causa:** La ruta del endpoint es incorrecta  
**Solución:** Verifica que la URL sea exactamente `/Report/ContpaqExcel/`

### Error: "415 Unsupported Media Type"
**Causa:** Falta el header Content-Type  
**Solución:** Agrega `Content-Type: application/json`

### Error: "400 Bad Request"
**Causa:** El JSON está mal formado  
**Solución:** Verifica que el JSON sea válido (comas, comillas, etc.)

## ✅ Verificación Rápida

Ejecuta este comando para verificar que todo funciona:

```powershell
Invoke-WebRequest -Uri "http://localhost:5010/Report/ContpaqExcel/" `
    -Method POST `
    -Body '{"FolioFiscal":"","FechaInicial":"","FechaFinal":"","TipoComprobante":null,"Rfc":"","Serie":"","Folio":"","Download":false}' `
    -ContentType "application/json" `
    -UseBasicParsing
```

**Respuesta esperada:** Status 200 OK

---

**Fecha de última verificación:** $(Get-Date -Format "dd/MM/yyyy HH:mm:ss")  
**Estado:** ✅ FUNCIONANDO CORRECTAMENTE
