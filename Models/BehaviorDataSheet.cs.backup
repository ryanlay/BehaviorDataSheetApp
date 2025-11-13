namespace BehaviorDataSheetApp.Models
{
    public class BehaviorDataSheet
    {
        public string ClientName { get; set; } = string.Empty;
        public string Setting { get; set; } = string.Empty;
        
        // Target behaviors
        public List<string> TargetBehaviors { get; set; } = new();
        
        // Behavior dimensions - tracks which dimensions are selected for each behavior
        public Dictionary<string, List<string>> BehaviorDimensions { get; set; } = new();
        
        // Interval data (for partial-interval recording)
        public int IntervalDuration { get; set; } = 30; // seconds
        public int TotalIntervals { get; set; } = 60; // default 30 minutes
        
        // Data collection grid - each behavior gets tracked per interval
        public Dictionary<string, List<bool>> IntervalData { get; set; } = new();
        
        // New time period tracking based on setting
        // For Education: Monday-Friday data - now includes dimension tracking
        public Dictionary<string, Dictionary<string, bool>> EducationData { get; set; } = new();
        // For Residential: Shift data (7-3, 3-11, 11-7) - now includes dimension tracking
        public Dictionary<string, Dictionary<string, bool>> ResidentialData { get; set; } = new();
        
        // Summary data
        public Dictionary<string, double> BehaviorPercentages { get; set; } = new();
        public Dictionary<string, int> BehaviorFrequencies { get; set; } = new();
        
        public string Notes { get; set; } = string.Empty;
        
        // Helper properties for time periods
        public List<string> EducationDays => new() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
        public List<string> ResidentialShifts => new() { "7-3", "3-11", "11-7" };
        
        // Available dimensions and their sub-categories
        public Dictionary<string, List<string>> AvailableDimensions => new()
        {
            { "Intensity", new List<string> { "Level 1", "Level 2", "Level 3" } },
            { "Duration", new List<string> { "<1 min", "1-5 min", "5+ min" } },
            { "Frequency", new List<string> { "Frequency" } },
            { "Attempts", new List<string> { "Attempts" } },
            { "Successes", new List<string> { "Successes" } }
        };
        
        // Get all behavior rows (behavior + its dimension sub-rows)
        public List<string> GetAllBehaviorRows()
        {
            var rows = new List<string>();
            foreach (var behavior in TargetBehaviors)
            {
                rows.Add(behavior);
                if (BehaviorDimensions.ContainsKey(behavior))
                {
                    foreach (var dimension in BehaviorDimensions[behavior])
                    {
                        if (AvailableDimensions.ContainsKey(dimension))
                        {
                            foreach (var subRow in AvailableDimensions[dimension])
                            {
                                rows.Add($"{behavior} - {subRow}");
                            }
                        }
                    }
                }
            }
            return rows;
        }
    }
}
