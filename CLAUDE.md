# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

MobileRobotAgent is a .NET 9 MAUI Blazor Android app that monitors incoming SMS messages from insurance companies and forwards them to a backend API. It filters SMS by a whitelist of trusted senders (`ExclusiveSmsListeners.cs`), validates authentication tokens, and posts matched messages to `https://prod-spmw.sigortapro.com/`.

## Build Commands

```bash
# Build the solution
dotnet build MobileRobotAgent.sln

# Build for Android release
dotnet build -c Release /p:TargetFramework=net9.0-android MobileRobotAgent.UI/MobileRobotAgent.UI.csproj
```

No test projects or linting configuration exist in this repo.

## Architecture

The app lives entirely in `MobileRobotAgent.UI/` and follows a service-based architecture with DI (configured in `MauiProgram.cs`).

**Services (`Services/`):**
- `ITokenService` / `TokenService` — validates and stores auth tokens via MAUI SecureStorage (base64, 150-512 chars)
- `ISmsService` / `SmsService` — platform-specific SMS permission handling and receiver registration
- `ISmsProcessingService` / `SmsProcessingService` — orchestrates the flow: validates sender against whitelist, posts to API with token + company ID

**Android platform layer (`Platforms/Android/`):**
- `SmsReceiver` — BroadcastReceiver capturing incoming SMS
- `SmsService` — Android-specific implementation of `ISmsService`

**UI (`Components/`):**
- Blazor Razor components using MudBlazor (Material Design)
- `Home.razor` — main page with token input, QR scanner (ZXing), start/stop controls

**Application flow:** Token input/validation → start SMS listener → SmsReceiver captures SMS → sender checked against `ExclusiveSmsListeners` whitelist → matched SMS posted to backend API.

## Key Dependencies

- **MudBlazor 8.0** — UI component library
- **ZXing.Net.Mobile** — QR code scanning for token input
- **Target**: Android API 24+, arm64
