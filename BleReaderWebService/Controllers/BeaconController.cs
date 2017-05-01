using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using BleReaderWebService.Hubs;
using Microsoft.Ajax.Utilities;
using BleReaderWebService.Services;
using Microsoft.AspNet.SignalR;

namespace BleReaderWebService.Controllers
{
    public class BeaconController : ApiController
    {
        private readonly BeaconDbContext _beaconsDbContext = new BeaconDbContext();

        [HttpGet]
        public object GetAllBeacons()
        {
            var notFound = new {Error = "No Beacons were found"};

            var beacons = _beaconsDbContext.Beacons.ToList();

            if (!beacons.Any()) return notFound;

            return beacons;
        }

        [HttpGet]
        public object GetByBeaconId(int? beaconId)
        {
            var notFound = new { Error = "No Beacons were found by the specified beaconId" };

            if (!beaconId.HasValue) return notFound;

            var beacon = _beaconsDbContext.Beacons.FirstOrDefault(u => u.BeaconId == beaconId.Value);

            if(beacon == null) return NotFound();

            return beacon;
        }

        [HttpGet]
        public object GetByBuildingName(string buildingName)
        {
            var notFound = new { Error = "No Beacons were found by the specified building name" };

            if (buildingName.IsNullOrWhiteSpace()) return notFound;

            var beacons = _beaconsDbContext.Beacons.Where(b => b.BuildingName.ToLower() == buildingName.ToLower()).ToList();

            if (!beacons.Any()) return notFound;

            return beacons;
        }

        [HttpPost]
        [Route("rest/1.0/beacon")]
        public object AddNewBeacon(string beaconName, string beaconAddress, int? beaconServiceId, int? beaconTxPowerLevel, string beaconScanDateTimeString, string buildingName, string imeiNumber)
        {
            var failure = new { Error = "Failed to add new Beacon with the provided data" };

            if (beaconName.IsNullOrWhiteSpace() || beaconAddress.IsNullOrWhiteSpace() || buildingName.IsNullOrWhiteSpace()) return failure;

            DateTime beaconScanDateTime;

            try
            {
                beaconScanDateTime = DateTime.ParseExact(beaconScanDateTimeString, "yyyy-MM-dd HH-mm-ss", CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                beaconScanDateTime = DateTime.Now;
            }

            var beacon = new Beacon
            {
                BeaconName = beaconName,
                BeaconAddress = beaconAddress,
                BeaconServiceId = beaconServiceId,
                BeaconTxPowerLevel = beaconTxPowerLevel,
                BeaconScanDateTime = beaconScanDateTime,
                BuildingName = buildingName,
                ImeiNumber = imeiNumber
            };

            _beaconsDbContext.Beacons.Add(beacon);
            _beaconsDbContext.SaveChanges();

            var notifier = GlobalHost.ConnectionManager.GetHubContext<BeaconHub>();

            notifier.Clients.All.newBeaconScanned(beacon.ImeiNumber, beacon.BeaconServiceId);

            return new StatusCodeResult(HttpStatusCode.OK, this);
        }

        [HttpPost]
        [Route("rest/1.0/beacon-generate")]
        public object AddGeneratedBeacon()
        {
            var beacon = new Beacon
            {
                BeaconName = "Auto-Generated",
                BeaconAddress = "00:00:00:00:00",
                BeaconServiceId = new Random().Next(0, 10),
                BeaconTxPowerLevel = -10,
                BeaconScanDateTime = DateTime.Now,
                BuildingName = "SIWB N533",
                ImeiNumber = "111111111111"
            };

            _beaconsDbContext.Beacons.Add(beacon);
            _beaconsDbContext.SaveChanges();

            var notifier = GlobalHost.ConnectionManager.GetHubContext<BeaconHub>();

            notifier.Clients.All.newBeaconScanned(beacon.ImeiNumber, beacon.BeaconServiceId);

            return new StatusCodeResult(HttpStatusCode.OK, this);
        }

    }   
}
