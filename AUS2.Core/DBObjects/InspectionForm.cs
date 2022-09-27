using System;
using System.Collections.Generic;
using System.Text;

namespace AUS2.Core.DBObjects
{
    public class InspectionForm
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string PlantManagerName { get; set; }
        public string FireFightingFacilities { get; set; }
        public string PowerSource { get; set; }
        public string AirCompressor { get; set; }
        public string FirstAidBox { get; set; }
        public string AdequateHouseKeeping { get; set; }
        public string ProvisionOfWater { get; set; }
        public string MechanicalLeakTester { get; set; }
        public string GasDetector { get; set; }
        public string MaintenanceFacilities { get; set; }
        public string AdequateSafetyFacilities { get; set; }
        public string ProtectiveWears { get; set; }
        public string SecurityCheckList { get; set; }
        public string EarthingProtectiveDevices { get; set; }
        public string EmergencyShutDownSystem { get; set; }
        public string PerimeterFence { get; set; }
        public string TrainingForOperators { get; set; }
        public string SupervisorComment { get; set; }
        public string LicenseType { get; set; }
        public int ApplicationType { get; set; }
        public string InspectionBy { get; set; }
        public string DistanceToPublicBuilding { get; set; }
        public string ProximityToAccessRoad { get; set; }
        public string ProximityToAdjourningFeatures { get; set; }
        public string SizeOfLand { get; set; }
        public string HighTensionRightofWay { get; set; }
        public string AccessibilityToSite { get; set; }
        public string InspectorComment { get; set; }
        public string AnnualCngcompression { get; set; }
        public string PipeLineRightofWays { get; set; }
        public string NbrLoadingPoint { get; set; }
        public string UploadedImage { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string ThirdPartyContactNamePhoneNo { get; set; }
        public string ThirdPartyCompanyName { get; set; }
        public decimal? StorageCapacity { get; set; }
    }
}
