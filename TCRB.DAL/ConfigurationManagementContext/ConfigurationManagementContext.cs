﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using TCRB.DAL.EntityModel;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TCRB.DAL.EntityModel;

namespace TCRB.DAL.ConfigurationManagementContext
{
    public partial class ConfigurationManagementContext : DbContext
    {
        public ConfigurationManagementContext()
        {
        }

        public ConfigurationManagementContext(DbContextOptions<ConfigurationManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ConfigurationDetail> ConfigurationDetail { get; set; }
        public virtual DbSet<ConfigurationMaster> ConfigurationMaster { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=172.17.9.83;Initial Catalog=ConfigurationManagement;User ID=sa;Password=p@ssw0rd");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConfigurationDetail>(entity =>
            {
                entity.Property(e => e.ID).HasDefaultValueSql("(newsequentialid())");
            });

            modelBuilder.Entity<ConfigurationMaster>(entity =>
            {
                entity.Property(e => e.ID).HasDefaultValueSql("(newsequentialid())");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}