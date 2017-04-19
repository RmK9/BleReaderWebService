﻿using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.Ajax.Utilities;
using BleReaderWebService.Services;

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
        public object AddNewBeacon(string beaconName, string beaconAddress, int? beaconServiceId, int? beaconTxPowerLevel, string beaconScanDateTimeString, string buildingName)
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
                BuildingName = buildingName
            };

            _beaconsDbContext.Beacons.Add(beacon);
            _beaconsDbContext.SaveChanges();

            return new StatusCodeResult(HttpStatusCode.OK, this);
        }

    }   
}
