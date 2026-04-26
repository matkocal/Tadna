using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

// ── database models ───────────────────────────────────────────
// These are the tables in the database
// Each class = one table
// Each property = one column

public class SessionRecord
{
    public int    Id      { get; set; }  // primary key, auto incremented
    public string Species { get; set; } = "";
    public string Sex     { get; set; } = "";
    public float  Mass    { get; set; }
    public string CsvFile { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // one session has many bites
    // this is a navigation property — EF handles the join automatically
    public List<BiteRecord> Bites { get; set; } = new List<BiteRecord>();
}

public class BiteRecord
{
    public int   Id         { get; set; }  // primary key
    public int   SessionId  { get; set; }  // foreign key → SessionRecord
    public int   BiteNumber { get; set; }
    public float PeakForce  { get; set; }
    public int   Duration   { get; set; }
    public long  Timestamp  { get; set; }

    // navigation back to parent session
    public SessionRecord Session { get; set; } = null!;
}

// ── database context ──────────────────────────────────────────
// This is the main class EF uses to talk to the database
// Think of it as the connection + all the tables

public class TadnaDb : DbContext
{
    // DbSet<T> = a table you can query
    public DbSet<SessionRecord> Sessions { get; set; }
    public DbSet<BiteRecord>    Bites    { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // SQLite stores everything in a single file
        options.UseSqlite("Data Source=tadna.db");
    }
}