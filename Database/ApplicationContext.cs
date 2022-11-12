﻿using Database.Models;
using Microsoft.EntityFrameworkCore;


namespace Database
{
    public class ApplicationContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<AppointmentModel> Appointments { get; set; }
        public DbSet<DoctorModel> Doctors { get; set; }
        public DbSet<ScheduleModel> Schedlues { get; set; }
        public DbSet<SpecializationModel> Specializations { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>().HasKey(model => model.Id);
            modelBuilder.Entity<UserModel>().HasIndex(model => model.UserName);
            modelBuilder.Entity<UserModel>().HasIndex(model => model.Password);
            modelBuilder.Entity<UserModel>().HasIndex(model => model.Fio);
            modelBuilder.Entity<UserModel>().HasIndex(model => model.PhoneNumber);
            modelBuilder.Entity<UserModel>().HasIndex(model => model.Role);

            modelBuilder.Entity<DoctorModel>().HasKey(model => model.Id);

            modelBuilder.Entity<AppointmentModel>().HasKey(model => model.Id);

            modelBuilder.Entity<ScheduleModel>().HasKey(model => model.Id);

            modelBuilder.Entity<SpecializationModel>().HasKey(model => model.Id);


            base.OnModelCreating(modelBuilder);
        }
    }
}
