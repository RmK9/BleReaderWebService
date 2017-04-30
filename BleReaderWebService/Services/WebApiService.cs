using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace BleReaderWebService.Services
{
    //---------------------------------------------- EXIF/PHOTOS
    public class BeaconDbContext : DbContext
    {
        public BeaconDbContext()
            : base("DefaultConnection")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<BeaconDbContext>(null);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Beacon> Beacons { get; set; }
    }

    [Table("BeaconScan", Schema = "dbo")]
    public class Beacon
    {
        [Key]
        public int BeaconId { get; set; }
        public string BeaconName { get; set; }
        public string BeaconAddress { get; set; }
        public int? BeaconServiceId { get; set; }
        public int? BeaconTxPowerLevel { get; set; }
        public DateTime BeaconScanDateTime { get; set; }
        public string BuildingName { get; set; }
        public string ImeiNumber { get; set; }
    }

}