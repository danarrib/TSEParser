using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace TSEParser
{
    public class TSEContext : DbContext
    {
        public string connectionString { get; set; }
        public TSEContext(string _connectionString)
        {
            connectionString = _connectionString;
        }
        public TSEContext()
        {
            connectionString = @"Server=.\SQLEXPRESS;Database=Eleicoes2022T1;Trusted_Connection=True;";
        }

        public DbSet<UnidadeFederativa> UnidadeFederativa { get; set; }
        public DbSet<Municipio> Municipio { get; set; }
        public DbSet<SecaoEleitoral> SecaoEleitoral { get; set; }
        public DbSet<VotosSecao> VotosSecao { get; set; }
        public DbSet<VotosMunicipio> VotosMunicipio { get; set; }
        public DbSet<Candidato> Candidato { get; set; }
        public DbSet<Partido> Partido { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UnidadeFederativa>().Property(e => e.Sigla).IsUnicode(false).HasMaxLength(2).IsFixedLength();
            modelBuilder.Entity<UnidadeFederativa>().Property(e => e.Nome).IsUnicode(false).HasMaxLength(20);
            modelBuilder.Entity<Municipio>().Property(e => e.Nome).IsUnicode(false).HasMaxLength(40);
            modelBuilder.Entity<Municipio>().Property(e => e.UFSigla).IsUnicode(false).HasMaxLength(2).IsFixedLength();
            modelBuilder.Entity<Candidato>().Property(e => e.Nome).IsUnicode(false).HasMaxLength(30);
            modelBuilder.Entity<Candidato>().Property(e => e.UFSigla).IsUnicode(false).HasMaxLength(2).IsFixedLength();
            modelBuilder.Entity<Partido>().Property(e => e.Nome).IsUnicode(false).HasMaxLength(30);

            modelBuilder.Entity<SecaoEleitoral>(entity =>
            {
                entity.HasKey(o => new
                {
                    o.MunicipioCodigo,
                    o.CodigoZonaEleitoral,
                    o.CodigoSecao,
                });
            });

            modelBuilder.Entity<VotosSecao>(entity =>
            {
                entity.HasKey(o => new
                {
                    o.SecaoEleitoralMunicipioCodigo,
                    o.SecaoEleitoralCodigoZonaEleitoral,
                    o.SecaoEleitoralCodigoSecao,
                    o.Cargo,
                    o.NumeroCandidato,
                });
            });

            modelBuilder.Entity<VotosMunicipio>(entity =>
            {
                entity.HasKey(o => new
                {
                    o.MunicipioCodigo,
                    o.Cargo,
                    o.NumeroCandidato,
                });
            });

            modelBuilder.Entity<Candidato>(entity =>
            {
                entity.HasKey(o => new
                {
                    o.Cargo,
                    o.NumeroCandidato,
                    o.UFSigla,
                });
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
