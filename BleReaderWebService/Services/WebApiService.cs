using System;
using System.Collections.Generic;
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
    }

    public class BeaconModel
    {
        public string BeaconName { get; set; }
        public string BeaconAddress { get; set; }
        public int? BeaconServiceId { get; set; }
        public int? BeaconTxPowerLevel { get; set; }
        public DateTime? BeaconScanDateTime { get; set; }
        public string BuildingName { get; set; }
    }

    //[Table("Photo", Schema = "dbo")]
    //public class Photo
    //{
    //    [Key]
    //    public int PhotoId { get; set; }
    //    public string Guid { get; set; }
    //    public string UserId { get; set; }
    //    public string UserFullName { get; set; }
    //    public string Extension { get; set; }
    //    public string ImageSize { get; set; }
    //    public string Title { get; set; }
    //    public bool IsVisible { get; set; }
    //    public decimal Price { get; set; }
    //    public string Description { get; set; }
    //    public DateTime UploadDate { get; set; }
    //}

}