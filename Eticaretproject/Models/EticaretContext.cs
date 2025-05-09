﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Eticaretproject.Models;

public partial class EticaretContext : DbContext
{
    public EticaretContext()
    {
    }

    public EticaretContext(DbContextOptions<EticaretContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Kategoriler> Kategorilers { get; set; }

    public virtual DbSet<Kullanicilar> Kullanicilars { get; set; }

    public virtual DbSet<Oneriler> Onerilers { get; set; }

    public virtual DbSet<Siparisdetayi> Siparisdetayis { get; set; }

    public virtual DbSet<Siparisler> Siparislers { get; set; }

    public virtual DbSet<Urunler> Urunlers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-3V3DAFM\\SQLEXPRESS;Database=ETicaret;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Kategoriler>(entity =>
        {
            entity.HasKey(e => e.KategoriId).HasName("PK__kategori__1D9181BD6E26DBAC");

            entity.ToTable("kategoriler");

            entity.Property(e => e.KategoriId)
                .ValueGeneratedNever()
                .HasColumnName("kategoriID");
            entity.Property(e => e.KategoriAdi).HasMaxLength(100);
        });

        modelBuilder.Entity<Kullanicilar>(entity =>
        {
            entity.HasKey(e => e.KullaniciId).HasName("PK__kullanic__848DC54B054226AB");

            entity.ToTable("kullanicilar");

            entity.Property(e => e.KullaniciId)
                .ValueGeneratedNever()
                .HasColumnName("kullaniciID");
            entity.Property(e => e.AdSoyad).HasMaxLength(100);
            entity.Property(e => e.Eposta).HasMaxLength(100);
            entity.Property(e => e.SifreHash).HasMaxLength(250);
        });

        modelBuilder.Entity<Oneriler>(entity =>
        {
            entity.HasKey(e => e.OneriId).HasName("PK__oneriler__E5ADEA17741AB017");

            entity.ToTable("oneriler");

            entity.Property(e => e.OneriId)
                .ValueGeneratedNever()
                .HasColumnName("OneriID");
            entity.Property(e => e.Gerekce).HasMaxLength(255);
            entity.Property(e => e.KullaniciId).HasColumnName("KullaniciID");
            entity.Property(e => e.UrunId).HasColumnName("UrunID");

            entity.HasOne(d => d.Kullanici).WithMany(p => p.Onerilers)
                .HasForeignKey(d => d.KullaniciId)
                .HasConstraintName("FK__oneriler__Kullan__45F365D3");

            entity.HasOne(d => d.Urun).WithMany(p => p.Onerilers)
                .HasForeignKey(d => d.UrunId)
                .HasConstraintName("FK__oneriler__UrunID__46E78A0C");
        });

        modelBuilder.Entity<Siparisdetayi>(entity =>
        {
            entity.HasKey(e => e.SiparisDetayiId).HasName("PK__siparisd__1F859F8CA5896165");

            entity.ToTable("siparisdetayi");

            entity.Property(e => e.SiparisDetayiId)
                .ValueGeneratedNever()
                .HasColumnName("SiparisDetayiID");
            entity.Property(e => e.SiparisAnindakiFiyat).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.SiparisId).HasColumnName("SiparisID");
            entity.Property(e => e.UrunId).HasColumnName("UrunID");

            entity.HasOne(d => d.Siparis).WithMany(p => p.Siparisdetayis)
                .HasForeignKey(d => d.SiparisId)
                .HasConstraintName("FK__siparisde__Sipar__4222D4EF");

            entity.HasOne(d => d.Urun).WithMany(p => p.Siparisdetayis)
                .HasForeignKey(d => d.UrunId)
                .HasConstraintName("FK__siparisde__UrunI__4316F928");
        });

        modelBuilder.Entity<Siparisler>(entity =>
        {
            entity.HasKey(e => e.SiparisId).HasName("PK__siparisl__C3F03BDD28544E49");

            entity.ToTable("siparisler");

            entity.Property(e => e.SiparisId)
                .ValueGeneratedNever()
                .HasColumnName("SiparisID");
            entity.Property(e => e.KullaniciId).HasColumnName("KullaniciID");
            entity.Property(e => e.SiparisTarihi)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Kullanici).WithMany(p => p.Siparislers)
                .HasForeignKey(d => d.KullaniciId)
                .HasConstraintName("FK__siparisle__Kulla__3F466844");
        });

        modelBuilder.Entity<Urunler>(entity =>
        {
            entity.HasKey(e => e.UrunId).HasName("PK__urunler__30BE53A3AAD167F7");

            entity.ToTable("urunler");

            entity.Property(e => e.UrunId)
                .ValueGeneratedNever()
                .HasColumnName("urunID");
            entity.Property(e => e.Fiyat).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.KategoriId).HasColumnName("KategoriID");
            entity.Property(e => e.ResimUrl)
                .HasMaxLength(255)
                .HasColumnName("ResimURL");
            entity.Property(e => e.UrunAdi).HasMaxLength(100);

            entity.HasOne(d => d.Kategori).WithMany(p => p.Urunlers)
                .HasForeignKey(d => d.KategoriId)
                .HasConstraintName("FK__urunler__ResimUR__3B75D760");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
