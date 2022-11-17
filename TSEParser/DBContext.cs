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
        public MotorBanco motorBanco { get; set; }
        public TSEContext() : base()
        {
            connectionString = @"Server=.\SQL2019DEV;Database=TSEParser_T1B;Trusted_Connection=True;";
            motorBanco = MotorBanco.SqlServer;
        }

        public TSEContext(string _connectionString, MotorBanco _motorBanco)
        {
            connectionString = _connectionString;
            motorBanco = _motorBanco;
        }

        public DbSet<Regiao> Regiao { get; set; }
        public DbSet<UnidadeFederativa> UnidadeFederativa { get; set; }
        public DbSet<Municipio> Municipio { get; set; }
        public DbSet<SecaoEleitoral> SecaoEleitoral { get; set; }
        public DbSet<VotosSecao> VotosSecao { get; set; }
        public DbSet<DefeitosSecao> DefeitosSecao { get; set; }
        public DbSet<VotosSecaoRDV> VotosSecaoRDV { get; set; }
        public DbSet<VotosLog> VotosLog { get; set; }
        public DbSet<VotosMunicipio> VotosMunicipio { get; set; }
        public DbSet<Candidato> Candidato { get; set; }
        public DbSet<Partido> Partido { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (motorBanco == MotorBanco.SqlServer)
                optionsBuilder.UseSqlServer(connectionString, sqlServerOptions =>
                {
                    sqlServerOptions.CommandTimeout(120);
                    //sqlServerOptions.EnableRetryOnFailure(2);
                });
            else if (motorBanco == MotorBanco.Postgres)
                optionsBuilder.UseNpgsql(connectionString);
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
            modelBuilder.Entity<Regiao>().Property(e => e.Nome).IsUnicode(false).HasMaxLength(20);

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

            modelBuilder.Entity<VotosSecaoRDV>(entity =>
            {
                entity.HasKey(o => new
                {
                    o.SecaoEleitoralMunicipioCodigo,
                    o.SecaoEleitoralCodigoZonaEleitoral,
                    o.SecaoEleitoralCodigoSecao,
                    o.IdVotoRDV,
                });
            });

            modelBuilder.Entity<DefeitosSecao>(entity =>
            {
                entity.HasKey(o => new
                {
                    o.MunicipioCodigo,
                    o.CodigoZonaEleitoral,
                    o.CodigoSecao,
                });
            });

            modelBuilder.Entity<VotosLog>(entity =>
            {
                entity.HasKey(o => new
                {
                    o.SecaoEleitoralMunicipioCodigo,
                    o.SecaoEleitoralCodigoZonaEleitoral,
                    o.SecaoEleitoralCodigoSecao,
                    o.IdVotoLog,
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

            modelBuilder.Entity<Regiao>().HasData(
                new Regiao() { Id = 0, Nome = "Brasil", },
                new Regiao() { Id = 1, Nome = "Sul", },
                new Regiao() { Id = 2, Nome = "Sudeste", },
                new Regiao() { Id = 3, Nome = "Centro-oeste", },
                new Regiao() { Id = 4, Nome = "Norte", },
                new Regiao() { Id = 5, Nome = "Nordeste", },
                new Regiao() { Id = 6, Nome = "Exterior", }
            );

            modelBuilder.Entity<UnidadeFederativa>().HasData(
                new UnidadeFederativa() { Sigla = "PR", Nome = "PARANÁ", RegiaoId = 1 },
                new UnidadeFederativa() { Sigla = "RS", Nome = "RIO GRANDE DO SUL", RegiaoId = 1 },
                new UnidadeFederativa() { Sigla = "SC", Nome = "SANTA CATARINA", RegiaoId = 1 },
                new UnidadeFederativa() { Sigla = "ES", Nome = "ESPÍRITO SANTO", RegiaoId = 2 },
                new UnidadeFederativa() { Sigla = "MG", Nome = "MINAS GERAIS", RegiaoId = 2 },
                new UnidadeFederativa() { Sigla = "RJ", Nome = "RIO DE JANEIRO", RegiaoId = 2 },
                new UnidadeFederativa() { Sigla = "SP", Nome = "SÃO PAULO", RegiaoId = 2 },
                new UnidadeFederativa() { Sigla = "DF", Nome = "DISTRITO FEDERAL", RegiaoId = 3 },
                new UnidadeFederativa() { Sigla = "GO", Nome = "GOIÁS", RegiaoId = 3 },
                new UnidadeFederativa() { Sigla = "MS", Nome = "MATO GROSSO DO SUL", RegiaoId = 3 },
                new UnidadeFederativa() { Sigla = "MT", Nome = "MATO GROSSO", RegiaoId = 3 },
                new UnidadeFederativa() { Sigla = "AC", Nome = "ACRE", RegiaoId = 4 },
                new UnidadeFederativa() { Sigla = "AM", Nome = "AMAZONAS", RegiaoId = 4 },
                new UnidadeFederativa() { Sigla = "AP", Nome = "AMAPÁ", RegiaoId = 4 },
                new UnidadeFederativa() { Sigla = "PA", Nome = "PARÁ", RegiaoId = 4 },
                new UnidadeFederativa() { Sigla = "RO", Nome = "RONDÔNIA", RegiaoId = 4 },
                new UnidadeFederativa() { Sigla = "RR", Nome = "RORAIMA", RegiaoId = 4 },
                new UnidadeFederativa() { Sigla = "TO", Nome = "TOCANTINS", RegiaoId = 4 },
                new UnidadeFederativa() { Sigla = "AL", Nome = "ALAGOAS", RegiaoId = 5 },
                new UnidadeFederativa() { Sigla = "BA", Nome = "BAHIA", RegiaoId = 5 },
                new UnidadeFederativa() { Sigla = "CE", Nome = "CEARÁ", RegiaoId = 5 },
                new UnidadeFederativa() { Sigla = "MA", Nome = "MARANHÃO", RegiaoId = 5 },
                new UnidadeFederativa() { Sigla = "PB", Nome = "PARAÍBA", RegiaoId = 5 },
                new UnidadeFederativa() { Sigla = "PE", Nome = "PERNAMBUCO", RegiaoId = 5 },
                new UnidadeFederativa() { Sigla = "PI", Nome = "PIAUÍ", RegiaoId = 5 },
                new UnidadeFederativa() { Sigla = "RN", Nome = "RIO GRANDE DO NORTE", RegiaoId = 5 },
                new UnidadeFederativa() { Sigla = "SE", Nome = "SERGIPE", RegiaoId = 5 },
                new UnidadeFederativa() { Sigla = "ZZ", Nome = "EXTERIOR", RegiaoId = 6 },
                new UnidadeFederativa() { Sigla = "BR", Nome = "FED - BRASIL", RegiaoId = 0 }
            );



            base.OnModelCreating(modelBuilder);
        }
    }
}
