-- ============================================================================
-- Script para crear las tablas de Nómina en la base de datos DescargaCfdiGFP
-- ============================================================================

USE DescargaCfdiGFP;
GO

-- Tabla: Nomina_Detalle
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Nomina_Detalle]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Nomina_Detalle](
        [ID_Comprobante] [int] NOT NULL,
        [TipoNomina] [varchar](10) NOT NULL,
        [FechaPago] [date] NOT NULL,
        [FechaInicialPago] [date] NOT NULL,
        [FechaFinalPago] [date] NOT NULL,
        [NumDiasPagados] [decimal](18, 2) NOT NULL,
        [TotalPercepciones] [decimal](18, 2) NULL,
        [TotalDeducciones] [decimal](18, 2) NULL,
        [TotalOtrosPagos] [decimal](18, 2) NULL,
        [CurpEmpleado] [varchar](18) NOT NULL,
        [NumSeguridadSocial] [varchar](15) NULL,
        [FechaInicioRelLaboral] [date] NULL,
        [Antiguedad] [varchar](20) NULL,
        [TipoContrato] [varchar](10) NULL,
        [NumEmpleado] [varchar](50) NULL,
        [Departamento] [varchar](100) NULL,
        [Puesto] [varchar](100) NULL,
        [SalarioBaseCotApor] [decimal](18, 2) NULL,
        [SalarioDiarioIntegrado] [decimal](18, 2) NULL,
        [PeriodicidadPago] [varchar](10) NULL,
        [CuentaBancaria] [varchar](50) NULL,
        [ClaveEntFed] [varchar](10) NULL,
        CONSTRAINT [PK_Nomina_Detalle] PRIMARY KEY CLUSTERED ([ID_Comprobante] ASC),
        CONSTRAINT [FK_Nomina_Detalle_CFDI_Comprobante] FOREIGN KEY([ID_Comprobante])
            REFERENCES [dbo].[CFDI_Comprobante] ([ID_Comprobante])
            ON DELETE CASCADE
    );
    PRINT 'Tabla Nomina_Detalle creada exitosamente';
END
ELSE
BEGIN
    PRINT 'Tabla Nomina_Detalle ya existe';
END
GO

-- Tabla: Nomina_Percepciones
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Nomina_Percepciones]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Nomina_Percepciones](
        [ID_Percepcion] [int] IDENTITY(1,1) NOT NULL,
        [ID_Comprobante] [int] NOT NULL,
        [TipoPercepcion] [varchar](10) NOT NULL,
        [Clave] [varchar](20) NOT NULL,
        [Concepto] [varchar](255) NOT NULL,
        [ImporteGravado] [decimal](18, 2) NULL,
        [ImporteExento] [decimal](18, 2) NULL,
        CONSTRAINT [PK_Nomina_Percepciones] PRIMARY KEY CLUSTERED ([ID_Percepcion] ASC),
        CONSTRAINT [FK_Nomina_Percepciones_CFDI_Comprobante] FOREIGN KEY([ID_Comprobante])
            REFERENCES [dbo].[CFDI_Comprobante] ([ID_Comprobante])
            ON DELETE CASCADE
    );
    PRINT 'Tabla Nomina_Percepciones creada exitosamente';
END
ELSE
BEGIN
    PRINT 'Tabla Nomina_Percepciones ya existe';
END
GO

-- Tabla: Nomina_Deducciones
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Nomina_Deducciones]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Nomina_Deducciones](
        [ID_Deduccion] [int] IDENTITY(1,1) NOT NULL,
        [ID_Comprobante] [int] NOT NULL,
        [TipoDeduccion] [varchar](10) NOT NULL,
        [Clave] [varchar](20) NOT NULL,
        [Concepto] [varchar](255) NOT NULL,
        [Importe] [decimal](18, 2) NOT NULL,
        CONSTRAINT [PK_Nomina_Deducciones] PRIMARY KEY CLUSTERED ([ID_Deduccion] ASC),
        CONSTRAINT [FK_Nomina_Deducciones_CFDI_Comprobante] FOREIGN KEY([ID_Comprobante])
            REFERENCES [dbo].[CFDI_Comprobante] ([ID_Comprobante])
            ON DELETE CASCADE
    );
    PRINT 'Tabla Nomina_Deducciones creada exitosamente';
END
ELSE
BEGIN
    PRINT 'Tabla Nomina_Deducciones ya existe';
END
GO

-- Tabla: Nomina_OtrosPagos
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Nomina_OtrosPagos]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Nomina_OtrosPagos](
        [ID_OtroPago] [int] IDENTITY(1,1) NOT NULL,
        [ID_Comprobante] [int] NOT NULL,
        [TipoOtroPago] [varchar](10) NOT NULL,
        [Clave] [varchar](20) NOT NULL,
        [Concepto] [varchar](255) NOT NULL,
        [Importe] [decimal](18, 2) NOT NULL,
        [SubsidioCausado] [decimal](18, 2) NULL,
        CONSTRAINT [PK_Nomina_OtrosPagos] PRIMARY KEY CLUSTERED ([ID_OtroPago] ASC),
        CONSTRAINT [FK_Nomina_OtrosPagos_CFDI_Comprobante] FOREIGN KEY([ID_Comprobante])
            REFERENCES [dbo].[CFDI_Comprobante] ([ID_Comprobante])
            ON DELETE CASCADE
    );
    PRINT 'Tabla Nomina_OtrosPagos creada exitosamente';
END
ELSE
BEGIN
    PRINT 'Tabla Nomina_OtrosPagos ya existe';
END
GO

PRINT '';
PRINT '============================================================================';
PRINT 'Script completado exitosamente';
PRINT 'Todas las tablas de Nómina han sido creadas o ya existían';
PRINT '============================================================================';
GO
