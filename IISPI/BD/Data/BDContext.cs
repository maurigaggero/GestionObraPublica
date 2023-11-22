using IISPI.BD.Data.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IISPI.BD.Data
{
    public class BDContext : IdentityDbContext<IISPIUser>
    {
        public DbSet<Certificado> Certificados { get; set; }
        public DbSet<CertificadoItem> CertificadoItems { get; set; }
        public DbSet<CertificadoItemControl> CertificadoItemControles { get; set; }
        public DbSet<CertificadoItemControlDoc> CertificadoItemControlDocs { get; set; }
        public DbSet<CertificadoItemControlParam> CertificadoItemControlParams { get; set; }
        public DbSet<Contrato> Contratos { get; set; }
        public DbSet<ContratoItem> ContratoItems { get; set; }
        public DbSet<ContratoItemControl> ContratoItemControles { get; set; }
        public DbSet<ContratoItemControlDoc> ContratoItemControlDocs { get; set; }
        public DbSet<ContratoItemControlParam> ContratoItemControlParams { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<EmpresaProfesional> EmpresaProfesionales { get; set; }
        public DbSet<FrenteObra> FrenteObras { get; set; }
        public DbSet<FrenteObraProfesional> FrenteObraProfesionales { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemControl> ItemControles { get; set; }
        public DbSet<ItemControlDoc> ItemControlDocs { get; set; }
        public DbSet<ItemControlParam> ItemControlParams { get; set; }
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
            builder.Entity<CertificadoItem>(o =>
            {
                o.Property(b => b.CantidadTotal).HasColumnType("Decimal(10,3)");
                o.Property(b => b.CantidadMedida).HasColumnType("Decimal(10,3)");
                o.Property(b => b.CantidadInformada).HasColumnType("Decimal(10,3)");
                o.Property(b => b.Coeficiente).HasColumnType("Decimal(10,3)");
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
                o.Property(b => b.ValorMinimo).HasColumnType("Decimal(10,3)");
                o.Property(b => b.ValorMaximo).HasColumnType("Decimal(10,3)");
            });
            builder.Entity<CertificadoItemControlParam>(o =>
            {
                o.Property(b => b.ValorMedido).HasColumnType("Decimal(10,3)");
                o.Property(b => b.ValorMinimo).HasColumnType("Decimal(10,3)");
                o.Property(b => b.ValorMaximo).HasColumnType("Decimal(10,3)");
            });
            builder.Entity<EventoParam>(o =>
            {
                o.Property(b => b.ValorMedido).HasColumnType("Decimal(10,3)");
                o.Property(b => b.ValorMinimo).HasColumnType("Decimal(10,3)");
                o.Property(b => b.ValorMaximo).HasColumnType("Decimal(10,3)");
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
