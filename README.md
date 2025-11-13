# Behavior Data Sheet Generator

A professional Blazor web application for generating behavior data sheets in PDF format. This application is designed for behavioral analysts, therapists, and educators to track and document behavioral data collection sessions.

## Features

- **Interactive Data Collection**: Easy-to-use web interface for recording behavioral observations
- **Real-time Calculations**: Automatic calculation of frequency counts and percentages
- **Professional PDF Output**: Generate formatted, printable behavior data sheets
- **Partial-Interval Recording**: Support for time-based interval data collection
- **Customizable Behaviors**: Add and remove target behaviors dynamically
- **Session Information**: Track client details, observer, setting, and session notes

## Getting Started

### Prerequisites

- .NET 8.0 SDK or later
- Modern web browser

### Running the Application

1. Clone or download this repository
2. Navigate to the project directory
3. Run the application:
   ```bash
   dotnet run
   ```
4. Open your browser and navigate to the displayed URL (typically `https://localhost:5001`)

### Using the Application

1. **Navigate to Behavior Data Sheet**: Click on "Behavior Data Sheet" in the navigation menu
2. **Enter Session Information**: Fill in client name, date, observer, setting, and activity details
3. **Configure Intervals**: Set interval duration and total number of intervals
4. **Add Target Behaviors**: Use the input field to add behaviors you want to track
5. **Record Data**: Use the data collection grid to mark intervals where behaviors occurred
6. **Add Notes**: Include any relevant observations or notes
7. **Generate PDF**: Click "Generate PDF" to create and download the behavior data sheet

## Technical Details

### Project Structure

- **Models/**: Data models for behavior data sheets
- **Services/**: PDF generation service using QuestPDF
- **Components/Pages/**: Blazor components for the user interface
- **wwwroot/**: Static files including JavaScript for file downloads

### Dependencies

- **QuestPDF**: Professional PDF generation library
- **Bootstrap**: UI framework for responsive design
- **Blazor Server**: Interactive web UI framework

## PDF Output Features

The generated PDF includes:
- Professional header and formatting
- Client and session information
- Data collection grid with interval markings
- Summary statistics (totals and percentages)
- Notes section
- Single-page format optimized for printing

## Contributing

This project is designed for educational and clinical use. Feel free to modify and extend the functionality to meet your specific needs.

## License

This project is provided as-is for educational and professional use.
