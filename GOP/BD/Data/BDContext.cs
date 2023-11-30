using GOP.BD.Data.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.BD.Data
{
    public class BDContext : IdentityDbContext<GOPUser>
    {
        public DbSet<Calle> Calles { get; set; }
        public DbSet<Certificado> Certificados { get; set; }
        public DbSet<CertificadoDoc> CertificadoDocs { get; set; }
        public DbSet<CertificadoItem> CertificadoItems { get; set; }
        public DbSet<CertificadoItemDef> CertificadoItemDefs { get; set; }
        public DbSet<CertificadoItemControl> CertificadoItemControles { get; set; }
        public DbSet<CertificadoItemControlDoc> CertificadoItemControlDocs { get; set; }
        public DbSet<CertificadoItemControlParam> CertificadoItemControlParams { get; set; }
        public DbSet<CertificadoItemControlParamDoc> CertificadoItemControlParamDocs { get; set; }
        public DbSet<Contrato> Contratos { get; set; }
        public DbSet<ContratoDoc> ContratoDocs { get; set; }
        public DbSet<ContratoEstructura> ContratoEstructuras { get; set; }
        public DbSet<ContratoEstructuraDoc> ContratoEstructuraDocs { get; set; }
        public DbSet<ContratoItem> ContratoItems { get; set; }
        public DbSet<ContratoItemControl> ContratoItemControles { get; set; }
        public DbSet<ContratoItemControlDoc> ContratoItemControlDocs { get; set; }
        public DbSet<ContratoItemControlParam> ContratoItemControlParams { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<EmpresaProfesional> EmpresaProfesionales { get; set; }
        public DbSet<FrenteObra> FrenteObras { get; set; }
        public DbSet<FrenteObraProfesional> FrenteObraProfesionales { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemDoc> ItemDocs { get; set; }
        public DbSet<ItemControl> ItemControles { get; set; }
        public DbSet<ItemControlDoc> ItemControlDocs { get; set; }
        public DbSet<ItemControlParam> ItemControlParams { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<EventoDoc> EventoDocs { get; set; }
        //public DbSet<EventoRelacionado> EventoRelacionados { get; set; }
        public DbSet<EventoParam> EventoParams { get; set; }
        public DbSet<EventoParamDoc> EventoParamDocs { get; set; }
        public DbSet<EventoTipo> EventoTipos { get; set; }
        public DbSet<EstructuraTipo> EstructuraTipos { get; set; }
        public DbSet<ParamCatalog> Parametros { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Unidad> Unidades { get; set; }
        public DbSet<Zona> Zonas { get; set; }
        public DbSet<ZonaProfesional> ZonaProfesionales { get; set; }

        public BDContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CertificadoItemDef>(o =>
            {
                o.Property(b => b.CantidadTotal).HasColumnType("Decimal(10,3)");
                o.Property(b => b.CantidadTotalEjecutado).HasColumnType("Decimal(10,3)");
                o.Property(b => b.CantidadInformada).HasColumnType("Decimal(10,3)");
                o.Property(b => b.CantidadTotalInformada).HasColumnType("Decimal(10,3)");
                o.Property(b => b.CantidadAcumulada).HasColumnType("Decimal(10,3)");
                o.Property(b => b.Coeficiente).HasColumnType("Decimal(10,3)");
            });
            builder.Entity<CertificadoItem>(o =>
            {
                o.Property(b => b.CantidadTotal).HasColumnType("Decimal(10,3)");
                o.Property(b => b.CantidadMedida).HasColumnType("Decimal(10,3)");
                o.Property(b => b.CantidadInformada).HasColumnType("Decimal(10,3)");
                o.Property(b => b.Coeficiente).HasColumnType("Decimal(10,3)");
            });
            builder.Entity<ContratoEstructura>(o =>
            {
                o.Property(b => b.Ancho).HasColumnType("Decimal(10,3)");
                o.Property(b => b.Longitud).HasColumnType("Decimal(10,3)");
            });
            builder.Entity<ContratoItem>(o =>
            {
                o.Property(b => b.CantidadTotal).HasColumnType("Decimal(10,3)");
                o.Property(b => b.Coeficiente).HasColumnType("Decimal(10,3)");
            });
            builder.Entity<ItemControlParam>(o =>
            {
                o.Property(b => b.ValorMinimo).HasColumnType("Decimal(10,3)");
                o.Property(b => b.ValorMaximo).HasColumnType("Decimal(10,3)");
            });
            builder.Entity<ContratoItemControlParam>(o =>
            {
                o.Property(b => b.ValorDato).HasColumnType("Decimal(10,3)");
                o.Property(b => b.ValorMinimo).HasColumnType("Decimal(10,3)");
                o.Property(b => b.ValorMaximo).HasColumnType("Decimal(10,3)");
            });
            builder.Entity<CertificadoItemControlParam>(o =>
            {
                o.Property(b => b.ValorMedido).HasColumnType("Decimal(10,3)");
            });
            builder.Entity<EventoParam>(o =>
            {
                o.Property(b => b.ValorMedido).HasColumnType("Decimal(10,3)");
            });
            builder.Entity<ParamCatalog>(o =>
            {
                o.Property(b => b.ValorMinimo).HasColumnType("Decimal(10,3)");
                o.Property(b => b.ValorMaximo).HasColumnType("Decimal(10,3)");
            });

            var cascadeFKs = builder.Model.G­etEntityTypes()
                                          .SelectMany(t => t.GetForeignKeys())
                                          .Where(fk => !fk.IsOwnership 
                                                       && fk.DeleteBehavior == DeleteBehavior.Casca­de);
            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restr­ict;
            }

            base.OnModelCreating(builder);
        }
    }
}
