# NewVoyager

> ⚠️ **DEPRECATION NOTICE**: This project is an older implementation and is currently deprecated. A complete rework with a modern tech stack and enhanced features is planned for the near future.

---

## Overview

**NewVoyager** is a comprehensive trip planning platform that was hosted on Azure, designed to make travel planning with friends seamless and collaborative. The application combines social features with intelligent travel assistance to create the ultimate trip planning experience.

## Features

| Category | Feature | Description |
|----------|---------|-------------|
| **Social** | Friend Management | Add friends and build your travel network |
| **Social** | Collaborative Planning | Create and manage trips with multiple participants |
| **Social** | Real-time Chat | Communicate with your travel group directly within the app |
| **Organization** | Activity Planning | Schedule and organize activities for your trips |
| **Organization** | Map Integration | Visualize your destinations and activities with interactive maps |
| **Organization** | Pin Locations | Mark important places and points of interest |
| **Organization** | Cost Planning | Budget your trips and track expenses collaboratively |
| **AI Assistant** | Flight Search | Find the cheapest plane tickets for your dates |
| **AI Assistant** | Hotel Recommendations | Discover affordable accommodation options |
| **AI Assistant** | Activity Suggestions | Get personalized recommendations for things to do |
| **AI Assistant** | Travel Optimization | Smart suggestions to maximize your travel experience |

## Technology Stack

- **Backend**: ASP.NET Core MVC
- **Database**: SQL Server (Azure SQL Database)
- **Authentication**: ASP.NET Core Identity
- **Frontend**: Razor Pages with JavaScript
- **AI Integration**: OpenAI API
- **Hosting**: Microsoft Azure

## Getting Started

### Prerequisites
- .NET SDK
- SQL Server or Azure SQL Database
- OpenAI API Key (for AI features)

### Configuration
Update `appsettings.json` with your connection strings and API keys:
```json
{
  "ConnectionStrings": {
    "VoyagerContext": "Your SQL Server connection string"
  },
  "OpenAIKey": "Your OpenAI API key",
  "ApiKey": "Your application API key"
}
```

### Running the Application
```bash
dotnet restore
dotnet ef database update
dotnet run
```

## License

This project was developed as part of an academic assignment.

---

<div align="center">
  <sub>Built with ❤️ for travelers everywhere</sub>
</div>
