namespace BehaviorDataSheetApp.Models
{
    public class ScipRManeuversModel
    {
        // Define properties as needed
        // Example:
            public Dictionary<string, List<string>> ManeuverCategories { get; set; } = new()
            {
                { "Core Techniques", new List<string> {
                    "Touch/Touch with a Grasp", "Back Hair Pull Stabilization / Release", "Front Deflection", "Back Hair Pull Stabilization / Release with Assistance", "Bite Release", "Blocking Punches", "One Arm Release", "Approach Prevention", "Two Arm Release", "Front Arm Catch", "Front Choke Windmill Release", "Front Kick Avoidance/Deflection", "Back Choke Release", "Protection from Thrown Objects", "Front Hair Pull Stabilization / Release"
                } },
                { "Specialized Techniques", new List<string> {
                    "One Person Escort", "Standing Wrap", "One Person Escort – Seated Variation", "Bite Prevention Front Hold", "Two Person Escort", "Front Choke Release", "Two Person Escort – Seated Variation", "One Person Wrap / Removal", "Arm Control by One Person or With Assistance", "Two Person Removal"
                } },
                { "Restrictive Techniques", new List<string> {
                    "Two Person Take Down", "Two or Three Person Supine Control"
                } }
            };

            // Track selected maneuvers for PDF
            public HashSet<string> SelectedManeuvers { get; set; } = new();
    }
}
