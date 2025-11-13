using BehaviorDataSheetApp.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace BehaviorDataSheetApp.Services
{
    public class BehaviorDataPdfService
    {
    public byte[] GenerateBehaviorDataSheetPdf(BehaviorDataSheet data, ScipRManeuversModel scipRManeuvers)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.Letter);
                    page.Margin(0.5f, Unit.Inch);
                    
                    page.Content().Column(column =>
                    {
                        // Header with Logo
                        column.Item().Row(row =>
                        {
                            // Logo in upper left
                            row.ConstantItem(80).Height(40).Image("wwwroot/images/tcfd-logo.png");
                            
                            // Title in center
                            row.RelativeItem().Column(headerCol =>
                            {
                                headerCol.Item().AlignCenter().PaddingTop(10).Text("BEHAVIOR DATA SHEET")
                                    .FontSize(16)
                                    .Bold();
                            });
                            
                            // Empty space on right for balance
                            row.ConstantItem(80);
                        });
                        
                        column.Item().PaddingVertical(10);
                        
                        // Client Information
                        column.Item().Row(row =>
                        {
                            row.RelativeItem().Text($"Client: {data.ClientName}").FontSize(10);
                            row.RelativeItem().Text($"Setting: {data.Setting}").FontSize(10);
                        });
                        
                        column.Item().PaddingVertical(10);
                        
                        // Create data grid based on setting
                        if (data.Setting == "Education")
                        {
                            column.Item().ExtendVertical().Table(table =>
                            {
                                // Define columns for Education - first column for labels, then data columns
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.ConstantColumn(80); // Label column - fixed width to fit content
                                    foreach (var day in data.EducationDays)
                                    {
                                        columns.RelativeColumn(1); // Data columns
                                    }
                                });
                                
                                // Header row
                                table.Header(header =>
                                {
                                    header.Cell().BorderBottom(1).BorderRight(1).Padding(2).Text("").FontSize(8); // Empty first column - no top/left borders
                                    foreach (var day in data.EducationDays)
                                    {
                                        header.Cell().Border(1).Padding(2).Background("#E8E8E8")
                                            .Text(day).FontSize(8).Bold().AlignCenter();
                                    }
                                });
                                
                                // Date row
                                table.Cell().Border(1).Padding(2).AlignMiddle().Text("Date:").FontSize(8).Bold();
                                foreach (var day in data.EducationDays)
                                {
                                    table.Cell().Border(1).Padding(2).MinHeight(20).Text("").FontSize(8);
                                }
                                
                                // Data rows - include main behaviors and their dimensions
                                foreach (var behavior in data.TargetBehaviors)
                                {
                                    // Determine background color based on behavior type
                                    string backgroundColor = "#E8E8E8"; // Default gray
                                    if (behavior.Equals("Aggression", StringComparison.OrdinalIgnoreCase))
                                        backgroundColor = "#FFE6E6"; // Light red
                                    else if (behavior.Equals("Self-Injury", StringComparison.OrdinalIgnoreCase))
                                        backgroundColor = "#E6F3FF"; // Light blue
                                    
                                    // Behavior name row spanning all columns with conditional background
                                    table.Cell().ColumnSpan((uint)(data.EducationDays.Count + 1)).Border(1).Padding(2).Background(backgroundColor)
                                        .Text(behavior).FontSize(8).Bold().AlignCenter();
                                    
                                    // Add dimension rows for this behavior
                                    if (data.BehaviorDimensions.ContainsKey(behavior))
                                    {
                                        foreach (var dimension in data.BehaviorDimensions[behavior])
                                        {
                                            if (data.AvailableDimensions.ContainsKey(dimension))
                                            {
                                                foreach (var subDimension in data.AvailableDimensions[dimension])
                                                {
                                                    var dimensionKey = $"{behavior} - {subDimension}";
                                                    
                                                    // Dimension label in first column
                                                    table.Cell().Background("#E8E8E8").Border(1).Padding(4).PaddingLeft(8).AlignMiddle()
                                                        .Text(subDimension).FontSize(8).Italic();
                                                    
                                                    // Data columns for this dimension
                                                    foreach (var day in data.EducationDays)
                                                    {
                                                        bool hasData = data.EducationData.ContainsKey(dimensionKey) && 
                                                                     data.EducationData[dimensionKey].ContainsKey(day) && 
                                                                     data.EducationData[dimensionKey][day];
                                                        table.Cell().Border(1).Padding(4).AlignMiddle().Text(hasData ? "✓" : "")
                                                            .FontSize(8).AlignCenter();
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                
                                // SCIP-R maneuvers header and rows
                                if (scipRManeuvers.SelectedManeuvers.Any())
                                {
                                    table.Cell().ColumnSpan((uint)(data.EducationDays.Count + 1)).Border(1).Padding(2).Background("#FFF9C4")
                                        .Text("SCIP-R Maneuvers").FontSize(8).Bold().AlignCenter();
                                    foreach (var category in scipRManeuvers.ManeuverCategories)
                                    {
                                        var selectedInCategory = category.Value.Where(m => scipRManeuvers.SelectedManeuvers.Contains(m)).ToList();
                                        foreach (var maneuver in selectedInCategory)
                                        {
                                            table.Cell().Background("#FFF9C4").Border(1).Padding(4).PaddingLeft(8).AlignMiddle()
                                                .Text(maneuver).FontSize(8).Italic();
                                            foreach (var day in data.EducationDays)
                                            {
                                                table.Cell().Border(1).Padding(4).AlignMiddle().Text("").FontSize(8).AlignCenter();
                                            }
                                        }
                                    }
                                }

                                // Comments row - dynamic height to fill remaining space
                                table.Cell().Border(1).Padding(2).Text("Comments:").FontSize(8).Bold();
                                foreach (var day in data.EducationDays)
                                {
                                    table.Cell().Border(1).Padding(8).MinHeight(80).Text("").FontSize(8);
                                }
                            });
                        }
                        else if (data.Setting == "Residential")
                        {
                            column.Item().ExtendVertical().Table(table =>
                            {
                                // Define columns for Residential - first column for labels, then data columns
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.ConstantColumn(80); // Label column - fixed width to fit content
                                    foreach (var shift in data.ResidentialShifts)
                                    {
                                        columns.RelativeColumn(1); // Data columns
                                    }
                                });
                                
                                // Header row
                                table.Header(header =>
                                {
                                    header.Cell().BorderBottom(1).BorderRight(1).Padding(2).Text("").FontSize(8); // Empty first column - no top/left borders
                                    foreach (var shift in data.ResidentialShifts)
                                    {
                                        header.Cell().Border(1).Padding(2).Background("#E8E8E8")
                                            .Text(shift).FontSize(8).Bold().AlignCenter();
                                    }
                                });
                                
                                // Date row
                                table.Cell().Border(1).Padding(2).AlignMiddle().Text("Date:").FontSize(8).Bold();
                                foreach (var shift in data.ResidentialShifts)
                                {
                                    table.Cell().Border(1).Padding(2).MinHeight(20).Text("").FontSize(8);
                                }
                                
                                // Data rows - include main behaviors and their dimensions
                                foreach (var behavior in data.TargetBehaviors)
                                {
                                    // Determine background color based on behavior type
                                    string backgroundColor = "#E8E8E8"; // Default gray
                                    if (behavior.Equals("Aggression", StringComparison.OrdinalIgnoreCase))
                                        backgroundColor = "#FFE6E6"; // Light red
                                    else if (behavior.Equals("Self-Injury", StringComparison.OrdinalIgnoreCase))
                                        backgroundColor = "#E6F3FF"; // Light blue
                                    
                                    // Behavior name row spanning all columns with conditional background
                                    table.Cell().ColumnSpan((uint)(data.ResidentialShifts.Count + 1)).Border(1).Padding(2).Background(backgroundColor)
                                        .Text(behavior).FontSize(8).Bold().AlignCenter();
                                    
                                    // Add dimension rows for this behavior
                                    if (data.BehaviorDimensions.ContainsKey(behavior))
                                    {
                                        foreach (var dimension in data.BehaviorDimensions[behavior])
                                        {
                                            if (data.AvailableDimensions.ContainsKey(dimension))
                                            {
                                                foreach (var subDimension in data.AvailableDimensions[dimension])
                                                {
                                                    var dimensionKey = $"{behavior} - {subDimension}";
                                                    
                                                    // Dimension label in first column
                                                    table.Cell().Background("#E8E8E8").Border(1).Padding(4).PaddingLeft(8).AlignMiddle()
                                                        .Text(subDimension).FontSize(8).Italic();
                                                    
                                                    // Data columns for this dimension
                                                    foreach (var shift in data.ResidentialShifts)
                                                    {
                                                        bool hasData = data.ResidentialData.ContainsKey(dimensionKey) && 
                                                                     data.ResidentialData[dimensionKey].ContainsKey(shift) && 
                                                                     data.ResidentialData[dimensionKey][shift];
                                                        table.Cell().Border(1).Padding(4).AlignMiddle().Text(hasData ? "✓" : "")
                                                            .FontSize(8).AlignCenter();
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                
                                // SCIP-R maneuvers header and rows
                                if (scipRManeuvers.SelectedManeuvers.Any())
                                {
                                    table.Cell().ColumnSpan((uint)(data.ResidentialShifts.Count + 1)).Border(1).Padding(2).Background("#FFF9C4")
                                        .Text("SCIP-R Maneuvers").FontSize(8).Bold().AlignCenter();
                                    foreach (var category in scipRManeuvers.ManeuverCategories)
                                    {
                                        var selectedInCategory = category.Value.Where(m => scipRManeuvers.SelectedManeuvers.Contains(m)).ToList();
                                        foreach (var maneuver in selectedInCategory)
                                        {
                                            table.Cell().Background("#FFF9C4").Border(1).Padding(4).PaddingLeft(8).AlignMiddle()
                                                .Text(maneuver).FontSize(8).Italic();
                                            foreach (var shift in data.ResidentialShifts)
                                            {
                                                table.Cell().Border(1).Padding(4).AlignMiddle().Text("").FontSize(8).AlignCenter();
                                            }
                                        }
                                    }
                                }

                                // Comments row - dynamic height to fill remaining space
                                table.Cell().Border(1).Padding(2).Text("Comments:").FontSize(8).Bold();
                                foreach (var shift in data.ResidentialShifts)
                                {
                                    table.Cell().Border(1).Padding(8).MinHeight(80).Text("").FontSize(8);
                                }
                            });
                        }
                    });
                });
            });
            
            return document.GeneratePdf();
        }
    }
}
