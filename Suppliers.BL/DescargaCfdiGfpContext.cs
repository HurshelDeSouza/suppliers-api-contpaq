using Microsoft.EntityFrameworkCore;
using Suppliers.BL.Entities;

namespace Suppliers.BL;

public partial class DescargaCfdiGfpContext : DbContext
{
    public string Connection { get; set; }
    private readonly string _host;
    private readonly DbContextOptions<DescargaCfdiGfpContext> _options;
    public DescargaCfdiGfpContext(string instancia, string user = "user", string password = "*******",
        string host = "localhost")
    {
        _host = host;
        SetDataConnection(instancia, user, password);
    }

    public DescargaCfdiGfpContext(DbContextOptions<DescargaCfdiGfpContext> options)
        : base(options)
    {
        _options = options;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(Connection).EnableSensitiveDataLogging().EnableDetailedErrors();
        }
    }

    public DescargaCfdiGfpContext GetCopy() => new(_options);

    /// <summary>
    /// Build the connection string
    /// </summary>
    /// <param name="instancia"></param>
    /// <param name="user"></param>
    /// <param name="password"></param>
    private void SetDataConnection(string instancia, string user, string password)
    {
        Connection = $"Server={_host}; Database={instancia}; User ID={user}; Password={password}; TrustServerCertificate=true";
    }

    public virtual DbSet<CfdiComprobante> CfdiComprobantes { get; set; }

    public virtual DbSet<CfdiConcepto> CfdiConceptos { get; set; }

    public virtual DbSet<CfdiEmisor> CfdiEmisors { get; set; }

    public virtual DbSet<CfdiReceptor> CfdiReceptors { get; set; }

    public virtual DbSet<CfdiRetencionConcepto> CfdiRetencionConceptos { get; set; }

    public virtual DbSet<CfdiTrasladoConcepto> CfdiTrasladoConceptos { get; set; }

    public virtual DbSet<ComercioExteriorDetalle> ComercioExteriorDetalles { get; set; }

    public virtual DbSet<ComercioExteriorDomicilio> ComercioExteriorDomicilios { get; set; }

    public virtual DbSet<ComercioExteriorMercancium> ComercioExteriorMercancia { get; set; }

    public virtual DbSet<NominaDeduccione> NominaDeducciones { get; set; }

    public virtual DbSet<NominaDetalle> NominaDetalles { get; set; }

    public virtual DbSet<NominaOtrosPago> NominaOtrosPagos { get; set; }

    public virtual DbSet<NominaPercepcione> NominaPercepciones { get; set; }

    public virtual DbSet<PagosDetalle> PagosDetalles { get; set; }

    public virtual DbSet<PagosDoctoRelacionado> PagosDoctoRelacionados { get; set; }

    public virtual DbSet<PagosPago> PagosPagos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CfdiComprobante>(entity =>
        {
            entity.HasKey(e => e.IdComprobante).HasName("PK__CFDI_Com__7DE63600EA946DEA");

            entity.ToTable("CFDI_Comprobante");

            entity.HasIndex(e => e.Uuid, "UQ__CFDI_Com__65A475E687655BAE").IsUnique();

            entity.Property(e => e.IdComprobante).HasColumnName("ID_Comprobante");
            entity.Property(e => e.Certificado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.Descuento).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Estatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.Folio)
                .HasMaxLength(40)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.FormaPago)
                .HasMaxLength(2)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.LugarExpedicion)
                .IsRequired()
                .HasMaxLength(5)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.MetodoPago)
                .HasMaxLength(3)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.Moneda)
                .IsRequired()
                .HasMaxLength(3)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.SelloDigital)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.Serie)
                .HasMaxLength(25)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.SubTotal).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TipoCambio).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.TipoDeComprobante)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Uuid)
                .IsRequired()
                .HasMaxLength(36)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS")
                .HasColumnName("UUID");
            entity.Property(e => e.Version)
                .IsRequired()
                .HasMaxLength(5)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
        });

        modelBuilder.Entity<CfdiConcepto>(entity =>
        {
            entity.HasKey(e => e.IdConcepto).HasName("PK__CFDI_Con__3D604791D9892870");

            entity.ToTable("CFDI_Concepto");

            entity.Property(e => e.IdConcepto).HasColumnName("ID_Concepto");
            entity.Property(e => e.Cantidad).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.ClaveProdServ)
                .IsRequired()
                .HasMaxLength(8)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.ClaveUnidad)
                .IsRequired()
                .HasMaxLength(3)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.Descripcion)
                .IsRequired()
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.Descuento).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.IdComprobante).HasColumnName("ID_Comprobante");
            entity.Property(e => e.Importe).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.NoIdentificacion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.ObjetoImp)
                .HasMaxLength(2)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.Unidad)
                .HasMaxLength(20)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.ValorUnitario).HasColumnType("decimal(18, 6)");

            entity.HasOne(d => d.IdComprobanteNavigation).WithMany(p => p.CfdiConceptos)
                .HasForeignKey(d => d.IdComprobante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CFDI_Conc__ID_Co__3F466844");
        });

        modelBuilder.Entity<CfdiEmisor>(entity =>
        {
            entity.HasKey(e => e.IdComprobante).HasName("PK__CFDI_Emi__7DE636003DE64ACD");

            entity.ToTable("CFDI_Emisor");

            entity.Property(e => e.IdComprobante)
                .ValueGeneratedNever()
                .HasColumnName("ID_Comprobante");
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.RegimenFiscal)
                .IsRequired()
                .HasMaxLength(3)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.RegistroPatronal)
                .HasMaxLength(20)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.Rfc)
                .IsRequired()
                .HasMaxLength(13)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");

            entity.HasOne(d => d.IdComprobanteNavigation).WithOne(p => p.CfdiEmisor)
                .HasForeignKey<CfdiEmisor>(d => d.IdComprobante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CFDI_Emis__ID_Co__398D8EEE");
        });

        modelBuilder.Entity<CfdiReceptor>(entity =>
        {
            entity.HasKey(e => e.IdComprobante).HasName("PK__CFDI_Rec__7DE63600FAF23403");

            entity.ToTable("CFDI_Receptor");

            entity.Property(e => e.IdComprobante)
                .ValueGeneratedNever()
                .HasColumnName("ID_Comprobante");
            entity.Property(e => e.DomicilioFiscalReceptor)
                .IsRequired()
                .HasMaxLength(5)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.NumRegIdTrib)
                .HasMaxLength(40)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.RegimenFiscalReceptor)
                .IsRequired()
                .HasMaxLength(3)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.ResidenciaFiscal)
                .HasMaxLength(5)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.Rfc)
                .IsRequired()
                .HasMaxLength(13)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.UsoCfdi)
                .IsRequired()
                .HasMaxLength(3)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS")
                .HasColumnName("UsoCFDI");

            entity.HasOne(d => d.IdComprobanteNavigation).WithOne(p => p.CfdiReceptor)
                .HasForeignKey<CfdiReceptor>(d => d.IdComprobante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CFDI_Rece__ID_Co__3C69FB99");
        });

        modelBuilder.Entity<CfdiRetencionConcepto>(entity =>
        {
            entity.HasKey(e => e.IdRetencion).HasName("PK__CFDI_Ret__9A009EBEDAAC0A0B");

            entity.ToTable("CFDI_RetencionConcepto");

            entity.Property(e => e.IdRetencion).HasColumnName("ID_Retencion");
            entity.Property(e => e.Base).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.IdConcepto).HasColumnName("ID_Concepto");
            entity.Property(e => e.Importe).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Impuesto)
                .IsRequired()
                .HasMaxLength(3)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.TasaOcuota)
                .HasColumnType("decimal(18, 6)")
                .HasColumnName("TasaOCuota");
            entity.Property(e => e.TipoFactor)
                .IsRequired()
                .HasMaxLength(8)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");

            entity.HasOne(d => d.IdConceptoNavigation).WithMany(p => p.CfdiRetencionConceptos)
                .HasForeignKey(d => d.IdConcepto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CFDI_Rete__ID_Co__44FF419A");
        });

        modelBuilder.Entity<CfdiTrasladoConcepto>(entity =>
        {
            entity.HasKey(e => e.IdTraslado).HasName("PK__CFDI_Tra__8F674787AA91C027");

            entity.ToTable("CFDI_TrasladoConcepto");

            entity.Property(e => e.IdTraslado).HasColumnName("ID_Traslado");
            entity.Property(e => e.Base).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.IdConcepto).HasColumnName("ID_Concepto");
            entity.Property(e => e.Importe).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Impuesto)
                .IsRequired()
                .HasMaxLength(3)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.TasaOcuota)
                .HasColumnType("decimal(18, 6)")
                .HasColumnName("TasaOCuota");
            entity.Property(e => e.TipoFactor)
                .IsRequired()
                .HasMaxLength(8)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");

            entity.HasOne(d => d.IdConceptoNavigation).WithMany(p => p.CfdiTrasladoConceptos)
                .HasForeignKey(d => d.IdConcepto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CFDI_Tras__ID_Co__4222D4EF");
        });

        modelBuilder.Entity<ComercioExteriorDetalle>(entity =>
        {
            entity.HasKey(e => e.IdComprobante).HasName("PK__Comercio__7DE63600AA95610B");

            entity.ToTable("ComercioExterior_Detalle");

            entity.Property(e => e.IdComprobante)
                .ValueGeneratedNever()
                .HasColumnName("ID_Comprobante");
            entity.Property(e => e.ClaveDePedimento)
                .HasMaxLength(2)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.Incoterm)
                .IsRequired()
                .HasMaxLength(3)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.TipoCambioUsd)
                .HasColumnType("decimal(18, 6)")
                .HasColumnName("TipoCambioUSD");
            entity.Property(e => e.TotalUsd)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("TotalUSD");

            entity.HasOne(d => d.IdComprobanteNavigation).WithOne(p => p.ComercioExteriorDetalle)
                .HasForeignKey<ComercioExteriorDetalle>(d => d.IdComprobante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ComercioE__ID_Co__5BE2A6F2");
        });

        modelBuilder.Entity<ComercioExteriorDomicilio>(entity =>
        {
            entity.HasKey(e => e.IdDomicilio).HasName("PK__Comercio__24312D3DEF9C3076");

            entity.ToTable("ComercioExterior_Domicilio");

            entity.Property(e => e.IdDomicilio).HasColumnName("ID_Domicilio");
            entity.Property(e => e.Calle)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.CodigoPostal)
                .HasMaxLength(10)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.Colonia)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.Estado)
                .HasMaxLength(30)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.IdComprobante).HasColumnName("ID_Comprobante");
            entity.Property(e => e.Localidad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.Municipio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.NumeroExterior)
                .HasMaxLength(20)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.Pais)
                .IsRequired()
                .HasMaxLength(3)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.TipoDomicilio)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");

            entity.HasOne(d => d.IdComprobanteNavigation).WithMany(p => p.ComercioExteriorDomicilios)
                .HasForeignKey(d => d.IdComprobante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ComercioE__ID_Co__5EBF139D");
        });

        modelBuilder.Entity<ComercioExteriorMercancium>(entity =>
        {
            entity.HasKey(e => e.IdMercancia).HasName("PK__Comercio__25F3CDDAC8C24915");

            entity.ToTable("ComercioExterior_Mercancia");

            entity.Property(e => e.IdMercancia).HasColumnName("ID_Mercancia");
            entity.Property(e => e.CantidadAduana).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.FraccionArancelaria)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.IdComprobante).HasColumnName("ID_Comprobante");
            entity.Property(e => e.NoIdentificacion)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.UnidadAduana)
                .IsRequired()
                .HasMaxLength(3)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.UnidadMedida)
                .HasMaxLength(10)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.ValorDolares).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ValorUnitarioAduana).HasColumnType("decimal(18, 6)");

            entity.HasOne(d => d.IdComprobanteNavigation).WithMany(p => p.ComercioExteriorMercancia)
                .HasForeignKey(d => d.IdComprobante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ComercioE__ID_Co__619B8048");
        });

        modelBuilder.Entity<NominaDeduccione>(entity =>
        {
            entity.HasKey(e => e.IdDeduccion).HasName("PK__Nomina_D__7CA6361AD5046F72");

            entity.ToTable("Nomina_Deducciones");

            entity.Property(e => e.IdDeduccion).HasColumnName("ID_Deduccion");
            entity.Property(e => e.Clave)
                .IsRequired()
                .HasMaxLength(3)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.Concepto)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.IdComprobante).HasColumnName("ID_Comprobante");
            entity.Property(e => e.Importe).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TipoDeduccion)
                .IsRequired()
                .HasMaxLength(3)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");

            entity.HasOne(d => d.IdComprobanteNavigation).WithMany(p => p.NominaDeducciones)
                .HasForeignKey(d => d.IdComprobante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Nomina_De__ID_Co__4D94879B");
        });

        modelBuilder.Entity<NominaDetalle>(entity =>
        {
            entity.HasKey(e => e.IdComprobante).HasName("PK__Nomina_D__7DE63600D33A1D84");

            entity.ToTable("Nomina_Detalle");

            entity.Property(e => e.IdComprobante)
                .ValueGeneratedNever()
                .HasColumnName("ID_Comprobante");
            entity.Property(e => e.Antiguedad)
                .HasMaxLength(10)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.ClaveEntFed)
                .HasMaxLength(2)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.CuentaBancaria)
                .HasMaxLength(20)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.CurpEmpleado)
                .IsRequired()
                .HasMaxLength(18)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.Departamento)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.NumDiasPagados).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.NumEmpleado)
                .HasMaxLength(15)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.NumSeguridadSocial)
                .HasMaxLength(15)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.PeriodicidadPago)
                .HasMaxLength(2)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.Puesto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.SalarioBaseCotApor).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SalarioDiarioIntegrado).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TipoContrato)
                .HasMaxLength(2)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.TipoNomina)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.TotalDeducciones).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalOtrosPagos).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalPercepciones).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdComprobanteNavigation).WithOne(p => p.NominaDetalle)
                .HasForeignKey<NominaDetalle>(d => d.IdComprobante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Nomina_De__ID_Co__47DBAE45");
        });

        modelBuilder.Entity<NominaOtrosPago>(entity =>
        {
            entity.HasKey(e => e.IdOtroPago).HasName("PK__Nomina_O__F43ECFD58912DB59");

            entity.ToTable("Nomina_OtrosPagos");

            entity.Property(e => e.IdOtroPago).HasColumnName("ID_OtroPago");
            entity.Property(e => e.Clave)
                .IsRequired()
                .HasMaxLength(3)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.Concepto)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.IdComprobante).HasColumnName("ID_Comprobante");
            entity.Property(e => e.Importe).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SubsidioCausado).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TipoOtroPago)
                .IsRequired()
                .HasMaxLength(3)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");

            entity.HasOne(d => d.IdComprobanteNavigation).WithMany(p => p.NominaOtrosPagos)
                .HasForeignKey(d => d.IdComprobante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Nomina_Ot__ID_Co__5070F446");
        });

        modelBuilder.Entity<NominaPercepcione>(entity =>
        {
            entity.HasKey(e => e.IdPercepcion).HasName("PK__Nomina_P__64652ED4EE4C50C0");

            entity.ToTable("Nomina_Percepciones");

            entity.Property(e => e.IdPercepcion).HasColumnName("ID_Percepcion");
            entity.Property(e => e.Clave)
                .IsRequired()
                .HasMaxLength(3)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.Concepto)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.IdComprobante).HasColumnName("ID_Comprobante");
            entity.Property(e => e.ImporteExento).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ImporteGravado).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TipoPercepcion)
                .IsRequired()
                .HasMaxLength(3)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");

            entity.HasOne(d => d.IdComprobanteNavigation).WithMany(p => p.NominaPercepciones)
                .HasForeignKey(d => d.IdComprobante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Nomina_Pe__ID_Co__4AB81AF0");
        });

        modelBuilder.Entity<PagosDetalle>(entity =>
        {
            entity.HasKey(e => e.IdComprobante).HasName("PK__Pagos_De__7DE63600F52BA6BE");

            entity.ToTable("CFDI_Pagos_Detalle");

            entity.Property(e => e.IdComprobante)
                .ValueGeneratedNever()
                .HasColumnName("ID_Comprobante");
            entity.Property(e => e.FormaDePago)
                .HasMaxLength(2)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.MontoTotalPagos).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalTrasladosBaseIva16)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("TotalTrasladosBaseIVA16");
            entity.Property(e => e.TotalTrasladosImpuestoIva16)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("TotalTrasladosImpuestoIVA16");

            entity.HasOne(d => d.IdComprobanteNavigation).WithOne(p => p.PagosDetalle)
                .HasForeignKey<PagosDetalle>(d => d.IdComprobante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pagos_Det__ID_Co__534D60F1");
        });

        modelBuilder.Entity<PagosDoctoRelacionado>(entity =>
        {
            entity.HasKey(e => e.IdDoctoRel).HasName("PK__Pagos_Do__357D1442DEE9E205");

            entity.ToTable("CFDI_Pagos_DoctoRelacionado");

            entity.Property(e => e.IdDoctoRel).HasColumnName("ID_DoctoRel");
            entity.Property(e => e.EquivalenciaDr)
                .HasColumnType("decimal(18, 6)")
                .HasColumnName("EquivalenciaDR");
            entity.Property(e => e.Folio)
                .HasMaxLength(40)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.IdDocumento)
                .IsRequired()
                .HasMaxLength(36)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.IdPago).HasColumnName("ID_Pago");
            entity.Property(e => e.ImpPagado).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ImpSaldoAnt).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ImpSaldoInsoluto).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MonedaDr)
                .IsRequired()
                .HasMaxLength(3)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS")
                .HasColumnName("MonedaDR");
            entity.Property(e => e.ObjetoImp)
                .HasMaxLength(2)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.ObjetoImpDr)
                .HasMaxLength(2)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS")
                .HasColumnName("ObjetoImpDR");
            entity.Property(e => e.Serie)
                .HasMaxLength(25)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");

            entity.HasOne(d => d.IdPagoNavigation).WithMany(p => p.PagosDoctoRelacionados)
                .HasForeignKey(d => d.IdPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pagos_Doc__ID_Pa__59063A47");
        });

        modelBuilder.Entity<PagosPago>(entity =>
        {
            entity.HasKey(e => e.IdPago).HasName("PK__Pagos_Pa__AE88B429F325A7A9");

            entity.ToTable("CFDI_Pagos_Pago");

            entity.Property(e => e.IdPago).HasColumnName("ID_Pago");
            entity.Property(e => e.FormaDePagoP)
                .IsRequired()
                .HasMaxLength(2)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.IdComprobante).HasColumnName("ID_Comprobante");
            entity.Property(e => e.MonedaP)
                .IsRequired()
                .HasMaxLength(3)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.Monto).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.NumOperacion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Modern_Spanish_CI_AS");
            entity.Property(e => e.TipoCambioP).HasColumnType("decimal(18, 6)");

            entity.HasOne(d => d.IdComprobanteNavigation).WithMany(p => p.PagosPagos)
                .HasForeignKey(d => d.IdComprobante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pagos_Pag__ID_Co__5629CD9C");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
