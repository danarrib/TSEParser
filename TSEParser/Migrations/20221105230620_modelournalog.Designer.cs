﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TSEParser;

namespace TSEParser.Migrations
{
    [DbContext(typeof(TSEContext))]
    [Migration("20221105230620_modelournalog")]
    partial class modelournalog
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.29")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TSEParser.Candidato", b =>
                {
                    b.Property<byte>("Cargo")
                        .HasColumnType("tinyint");

                    b.Property<int>("NumeroCandidato")
                        .HasColumnType("int");

                    b.Property<string>("UFSigla")
                        .HasColumnType("char(2)")
                        .IsFixedLength(true)
                        .HasMaxLength(2)
                        .IsUnicode(false);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(30)")
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.HasKey("Cargo", "NumeroCandidato", "UFSigla");

                    b.HasIndex("UFSigla");

                    b.ToTable("Candidato");
                });

            modelBuilder.Entity("TSEParser.Municipio", b =>
                {
                    b.Property<int>("Codigo")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(40)")
                        .HasMaxLength(40)
                        .IsUnicode(false);

                    b.Property<string>("UFSigla")
                        .IsRequired()
                        .HasColumnType("char(2)")
                        .IsFixedLength(true)
                        .HasMaxLength(2)
                        .IsUnicode(false);

                    b.HasKey("Codigo");

                    b.HasIndex("UFSigla");

                    b.ToTable("Municipio");
                });

            modelBuilder.Entity("TSEParser.Partido", b =>
                {
                    b.Property<byte>("Numero")
                        .HasColumnType("tinyint");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(30)")
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.HasKey("Numero");

                    b.ToTable("Partido");
                });

            modelBuilder.Entity("TSEParser.SecaoEleitoral", b =>
                {
                    b.Property<int>("MunicipioCodigo")
                        .HasColumnType("int");

                    b.Property<short>("CodigoZonaEleitoral")
                        .HasColumnType("smallint");

                    b.Property<short>("CodigoSecao")
                        .HasColumnType("smallint");

                    b.Property<DateTime>("AberturaUrnaEletronica")
                        .HasColumnType("datetime2");

                    b.Property<int>("CodigoIdentificacaoUrnaEletronica")
                        .HasColumnType("int");

                    b.Property<short>("CodigoLocalVotacao")
                        .HasColumnType("smallint");

                    b.Property<short>("Comparecimento")
                        .HasColumnType("smallint");

                    b.Property<short>("DE_Brancos")
                        .HasColumnType("smallint");

                    b.Property<short>("DE_EleitoresAptos")
                        .HasColumnType("smallint");

                    b.Property<short>("DE_Nulos")
                        .HasColumnType("smallint");

                    b.Property<short>("DE_Total")
                        .HasColumnType("smallint");

                    b.Property<short>("DE_VotosLegenda")
                        .HasColumnType("smallint");

                    b.Property<short>("DE_VotosNominais")
                        .HasColumnType("smallint");

                    b.Property<short>("DF_Brancos")
                        .HasColumnType("smallint");

                    b.Property<short>("DF_EleitoresAptos")
                        .HasColumnType("smallint");

                    b.Property<short>("DF_Nulos")
                        .HasColumnType("smallint");

                    b.Property<short>("DF_Total")
                        .HasColumnType("smallint");

                    b.Property<short>("DF_VotosLegenda")
                        .HasColumnType("smallint");

                    b.Property<short>("DF_VotosNominais")
                        .HasColumnType("smallint");

                    b.Property<short>("EleitoresAptos")
                        .HasColumnType("smallint");

                    b.Property<short>("EleitoresFaltosos")
                        .HasColumnType("smallint");

                    b.Property<DateTime>("FechamentoUrnaEletronica")
                        .HasColumnType("datetime2");

                    b.Property<short>("GO_Brancos")
                        .HasColumnType("smallint");

                    b.Property<short>("GO_EleitoresAptos")
                        .HasColumnType("smallint");

                    b.Property<short>("GO_Nulos")
                        .HasColumnType("smallint");

                    b.Property<short>("GO_Total")
                        .HasColumnType("smallint");

                    b.Property<short>("GO_VotosNominais")
                        .HasColumnType("smallint");

                    b.Property<short>("HabilitadosPorAnoNascimento")
                        .HasColumnType("smallint");

                    b.Property<bool>("LogUrnaInconsistente")
                        .HasColumnType("bit");

                    b.Property<short>("ModeloUrnaEletronica")
                        .HasColumnType("smallint");

                    b.Property<short>("PR_Brancos")
                        .HasColumnType("smallint");

                    b.Property<short>("PR_EleitoresAptos")
                        .HasColumnType("smallint");

                    b.Property<short>("PR_Nulos")
                        .HasColumnType("smallint");

                    b.Property<short>("PR_Total")
                        .HasColumnType("smallint");

                    b.Property<short>("PR_VotosNominais")
                        .HasColumnType("smallint");

                    b.Property<short>("SE_Brancos")
                        .HasColumnType("smallint");

                    b.Property<short>("SE_EleitoresAptos")
                        .HasColumnType("smallint");

                    b.Property<short>("SE_Nulos")
                        .HasColumnType("smallint");

                    b.Property<short>("SE_Total")
                        .HasColumnType("smallint");

                    b.Property<short>("SE_VotosNominais")
                        .HasColumnType("smallint");

                    b.Property<DateTime>("Zeresima")
                        .HasColumnType("datetime2");

                    b.HasKey("MunicipioCodigo", "CodigoZonaEleitoral", "CodigoSecao");

                    b.ToTable("SecaoEleitoral");
                });

            modelBuilder.Entity("TSEParser.UnidadeFederativa", b =>
                {
                    b.Property<string>("Sigla")
                        .HasColumnType("char(2)")
                        .IsFixedLength(true)
                        .HasMaxLength(2)
                        .IsUnicode(false);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.HasKey("Sigla");

                    b.ToTable("UnidadeFederativa");
                });

            modelBuilder.Entity("TSEParser.VotosLog", b =>
                {
                    b.Property<int>("SecaoEleitoralMunicipioCodigo")
                        .HasColumnName("MunicipioCodigo")
                        .HasColumnType("int");

                    b.Property<short>("SecaoEleitoralCodigoZonaEleitoral")
                        .HasColumnName("CodigoZonaEleitoral")
                        .HasColumnType("smallint");

                    b.Property<short>("SecaoEleitoralCodigoSecao")
                        .HasColumnName("CodigoSecao")
                        .HasColumnType("smallint");

                    b.Property<short>("IdVotoLog")
                        .HasColumnType("smallint");

                    b.Property<byte>("DedoBiometria")
                        .HasColumnType("tinyint");

                    b.Property<bool>("EleitorSuspenso")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FimVoto")
                        .HasColumnType("datetime2");

                    b.Property<bool>("HabilitacaoCancelada")
                        .HasColumnType("bit");

                    b.Property<DateTime>("HabilitacaoUrna")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("InicioVoto")
                        .HasColumnType("datetime2");

                    b.Property<short>("LinhaLog")
                        .HasColumnType("smallint");

                    b.Property<short>("LinhaLogFim")
                        .HasColumnType("smallint");

                    b.Property<short>("ModeloUrnaEletronica")
                        .HasColumnType("smallint");

                    b.Property<bool>("PossuiBiometria")
                        .HasColumnType("bit");

                    b.Property<byte>("QtdTeclasIndevidas")
                        .HasColumnType("tinyint");

                    b.Property<short>("ScoreBiometria")
                        .HasColumnType("smallint");

                    b.Property<bool>("VotoComputado")
                        .HasColumnType("bit");

                    b.Property<bool>("VotoNuloSuspensaoDE")
                        .HasColumnType("bit");

                    b.Property<bool>("VotoNuloSuspensaoDF")
                        .HasColumnType("bit");

                    b.Property<bool>("VotoNuloSuspensaoGO")
                        .HasColumnType("bit");

                    b.Property<bool>("VotoNuloSuspensaoPR")
                        .HasColumnType("bit");

                    b.Property<bool>("VotoNuloSuspensaoSE")
                        .HasColumnType("bit");

                    b.Property<bool>("VotouDE")
                        .HasColumnType("bit");

                    b.Property<bool>("VotouDF")
                        .HasColumnType("bit");

                    b.Property<bool>("VotouGO")
                        .HasColumnType("bit");

                    b.Property<bool>("VotouPR")
                        .HasColumnType("bit");

                    b.Property<bool>("VotouSE")
                        .HasColumnType("bit");

                    b.HasKey("SecaoEleitoralMunicipioCodigo", "SecaoEleitoralCodigoZonaEleitoral", "SecaoEleitoralCodigoSecao", "IdVotoLog");

                    b.ToTable("VotosLog");
                });

            modelBuilder.Entity("TSEParser.VotosMunicipio", b =>
                {
                    b.Property<int>("MunicipioCodigo")
                        .HasColumnName("MunicipioCodigo")
                        .HasColumnType("int");

                    b.Property<byte>("Cargo")
                        .HasColumnType("tinyint");

                    b.Property<int>("NumeroCandidato")
                        .HasColumnType("int");

                    b.Property<long>("QtdVotos")
                        .HasColumnType("bigint");

                    b.Property<bool>("VotoLegenda")
                        .HasColumnType("bit");

                    b.HasKey("MunicipioCodigo", "Cargo", "NumeroCandidato");

                    b.ToTable("VotosMunicipio");
                });

            modelBuilder.Entity("TSEParser.VotosSecao", b =>
                {
                    b.Property<int>("SecaoEleitoralMunicipioCodigo")
                        .HasColumnName("MunicipioCodigo")
                        .HasColumnType("int");

                    b.Property<short>("SecaoEleitoralCodigoZonaEleitoral")
                        .HasColumnName("CodigoZonaEleitoral")
                        .HasColumnType("smallint");

                    b.Property<short>("SecaoEleitoralCodigoSecao")
                        .HasColumnName("CodigoSecao")
                        .HasColumnType("smallint");

                    b.Property<byte>("Cargo")
                        .HasColumnType("tinyint");

                    b.Property<int>("NumeroCandidato")
                        .HasColumnType("int");

                    b.Property<short>("QtdVotos")
                        .HasColumnType("smallint");

                    b.Property<bool>("VotoLegenda")
                        .HasColumnType("bit");

                    b.HasKey("SecaoEleitoralMunicipioCodigo", "SecaoEleitoralCodigoZonaEleitoral", "SecaoEleitoralCodigoSecao", "Cargo", "NumeroCandidato");

                    b.ToTable("VotosSecao");
                });

            modelBuilder.Entity("TSEParser.VotosSecaoRDV", b =>
                {
                    b.Property<int>("SecaoEleitoralMunicipioCodigo")
                        .HasColumnName("MunicipioCodigo")
                        .HasColumnType("int");

                    b.Property<short>("SecaoEleitoralCodigoZonaEleitoral")
                        .HasColumnName("CodigoZonaEleitoral")
                        .HasColumnType("smallint");

                    b.Property<short>("SecaoEleitoralCodigoSecao")
                        .HasColumnName("CodigoSecao")
                        .HasColumnType("smallint");

                    b.Property<short>("IdVotoRDV")
                        .HasColumnType("smallint");

                    b.Property<byte>("Cargo")
                        .HasColumnType("tinyint");

                    b.Property<int>("NumeroCandidato")
                        .HasColumnType("int");

                    b.Property<short>("QtdVotos")
                        .HasColumnType("smallint");

                    b.Property<bool>("VotoBranco")
                        .HasColumnType("bit");

                    b.Property<bool>("VotoLegenda")
                        .HasColumnType("bit");

                    b.Property<bool>("VotoNulo")
                        .HasColumnType("bit");

                    b.HasKey("SecaoEleitoralMunicipioCodigo", "SecaoEleitoralCodigoZonaEleitoral", "SecaoEleitoralCodigoSecao", "IdVotoRDV");

                    b.ToTable("VotosSecaoRDV");
                });

            modelBuilder.Entity("TSEParser.Candidato", b =>
                {
                    b.HasOne("TSEParser.UnidadeFederativa", "UF")
                        .WithMany()
                        .HasForeignKey("UFSigla")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TSEParser.Municipio", b =>
                {
                    b.HasOne("TSEParser.UnidadeFederativa", "UF")
                        .WithMany()
                        .HasForeignKey("UFSigla")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TSEParser.SecaoEleitoral", b =>
                {
                    b.HasOne("TSEParser.Municipio", "Municipio")
                        .WithMany()
                        .HasForeignKey("MunicipioCodigo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TSEParser.VotosLog", b =>
                {
                    b.HasOne("TSEParser.SecaoEleitoral", "SecaoEleitoral")
                        .WithMany()
                        .HasForeignKey("SecaoEleitoralMunicipioCodigo", "SecaoEleitoralCodigoZonaEleitoral", "SecaoEleitoralCodigoSecao")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TSEParser.VotosMunicipio", b =>
                {
                    b.HasOne("TSEParser.Municipio", "Municipio")
                        .WithMany()
                        .HasForeignKey("MunicipioCodigo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TSEParser.VotosSecao", b =>
                {
                    b.HasOne("TSEParser.SecaoEleitoral", "SecaoEleitoral")
                        .WithMany()
                        .HasForeignKey("SecaoEleitoralMunicipioCodigo", "SecaoEleitoralCodigoZonaEleitoral", "SecaoEleitoralCodigoSecao")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TSEParser.VotosSecaoRDV", b =>
                {
                    b.HasOne("TSEParser.SecaoEleitoral", "SecaoEleitoral")
                        .WithMany()
                        .HasForeignKey("SecaoEleitoralMunicipioCodigo", "SecaoEleitoralCodigoZonaEleitoral", "SecaoEleitoralCodigoSecao")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
