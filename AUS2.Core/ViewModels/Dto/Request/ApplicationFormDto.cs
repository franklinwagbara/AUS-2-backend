using System;

namespace AUS2.Core.ViewModels.Dto.Request
{
    public class ApplicationFormDto
    {
        public int? LandSize { get; set; }
        public string WellLocationCategory { get; set; }
        public string Field { get; set; }
        public string Block { get; set; }
        public string Terrain { get; set; }
        public string WellSpudName { get; set; }
        public string WellPreSpudName { get; set; }
        public string WellSurfaceCoordinates { get; set; }
        public string WellClassApplied { get; set; }
        public string ProposedRig { get; set; }
        public string ExpectedVolumes { get; set; }
        public string TargetReserves { get; set; }
        public string Afe { get; set; }
        public int EstimatedOperationsDays { get; set; }
        public string WellName { get; set; }
        public string NatureOfOperation { get; set; }
        public string WellCompletionInterval { get; set; }
        public string RigForOperation { get; set; }
        public string PreOperationProductionRate { get; set; }
        public string PostOperationProductionRate1 { get; set; }
        public string InitialReservesAllocationOfWell { get; set; }
        public string CumulativeProductionForWell { get; set; }
        public string PlugbackInterval { get; set; }
        public string LastProductionRate { get; set; }
        public DateTime? SpudDate { get; set; }
    }
}
