using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeCapture.Models;

namespace TimeCapture.Context
{
    public class TimeRecordingContext : DbContext
    {
        public TimeRecordingContext(DbContextOptions<TimeRecordingContext> options) : base(options) { }

        public DbSet<TimeRecording> TimeRecordings { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Matter> Matters { get; set; }
        public DbSet<Lawyer> Lawyers { get; set; }
    }
}
