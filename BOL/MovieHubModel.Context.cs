﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BOL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MovieDbEntities : DbContext
    {
        public MovieDbEntities()
            : base("name=MovieDbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tbl_Actor> tbl_Actor { get; set; }
        public virtual DbSet<tbl_ActorMovie> tbl_ActorMovie { get; set; }
        public virtual DbSet<tbl_Movie> tbl_Movie { get; set; }
        public virtual DbSet<tbl_Producer> tbl_Producer { get; set; }
    }
}