﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FindIt.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class GuessDBEntities : DbContext
    {
        public GuessDBEntities()
            : base("name=GuessDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Achievements> Achievements { get; set; }
        public virtual DbSet<Challenge> Challenge { get; set; }
        public virtual DbSet<Games> Games { get; set; }
        public virtual DbSet<PlayedGames> PlayedGames { get; set; }
        public virtual DbSet<Questions> Questions { get; set; }
        public virtual DbSet<Skills> Skills { get; set; }
        public virtual DbSet<UserAchievements> UserAchievements { get; set; }
        public virtual DbSet<UserInfo> UserInfo { get; set; }
        public virtual DbSet<UserSkills> UserSkills { get; set; }
    }
}
