using System;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace BleReaderWebService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings
                .Add(new RequestHeaderMapping("Accept",
                    "text/html",
                    StringComparison.InvariantCultureIgnoreCase,
                    true,
                    "application/json"));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "GetAllBeaconData",
                "rest/1.0/beacons",
                new { controller = "Beacon", action = "GetAllBeacons" }
            );

            config.Routes.MapHttpRoute(
                "GetBeaconDataById",
                "rest/1.0/beacons/beacon-id/{beaconId}",
                new { controller = "Beacon", action = "GetByBeaconId", beaconId = "" }
            );

            config.Routes.MapHttpRoute(
                "GetBeaconDataByBuildingName",
                "rest/1.0/beacons/building-name/{buildingName}",
                new { controller = "Beacon", action = "GetByBuildingName", buildingName = "" }
            );

            config.Routes.MapHttpRoute(
                "AddNewBeacon",
                "rest/1.0/beacon/{beaconName}/{beaconAddress}/{beaconServiceId}/{beaconTxPowerLevel}/{beaconScanDateTime}/{buildingName}",
                new { controller = "Beacon", action = "AddNewBeacon", beaconName = "", beaconAddress = "", beaconServiceId = "", beaconTxPowerLevel = "", beaconScanDateTime = "", buildingName = "" }
            );
        }
    }
}
