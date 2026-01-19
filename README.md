# 🎯 C# Event Handling Examples

This repository contains practical examples demonstrating **Event Handling** in C#. Events are a powerful feature in C# that allow objects to communicate with each other in a loosely coupled way.

---

## 📚 What You'll Learn

- ✅ How to define and raise events
- ✅ How to subscribe to events
- ✅ Using `EventHandler` and `EventHandler<T>` delegates
- ✅ Creating custom `EventArgs` classes
- ✅ Memory-efficient event handling with `EventHandlerList`

---

## 📁 Projects Overview

| #  | Project Name | Description |
|----|--------------|-------------|
| 1  | [EventHandling](https://github.com/KartikZCoding/csharp-event-handling/tree/7c2c98ba7b7fba4f59848a53d5bebe2bc807193b/EventHandling) | Basic event handling with video encoder example |
| 2  | [TemperatureEventHandling](https://github.com/KartikZCoding/csharp-event-handling/blob/7c2c98ba7b7fba4f59848a53d5bebe2bc807193b/TemperatureEventHandling/Program.cs) | Multiple events with temperature sensor simulation |
| 3  | [EventHandlerListUses](https://github.com/KartikZCoding/csharp-event-handling/blob/7c2c98ba7b7fba4f59848a53d5bebe2bc807193b/EventHandlerListUses/Program.cs) | Memory-optimized event handling with `EventHandlerList` |

---

## 1️⃣ EventHandling

### 📝 Description
A simple example showing how a **VideoEncoder** publishes an event when encoding is complete, and multiple services (**MailService** & **MessageService**) subscribe to receive the notification.

### 🔑 Key Concepts
- **Publisher**: `VideoEncoder` - raises `VideoEncoded` event after encoding
- **Subscribers**: `MailService` & `MessageService` - listen and respond to the event

### 📂 Files
- `Video.cs` - Simple model class with `Title` property
- `VideoEncoder.cs` - Publisher class that defines and raises the event
- `Program.cs` - Main program that wires everything together

### 💻 Code Flow
```
1. Create a Video object
2. Create VideoEncoder (Publisher)
3. Create MailService & MessageService (Subscribers)
4. Subscribe services to VideoEncoded event
5. Call Encode() → Event fires → All subscribers notified
```

### ▶️ Run the Project
```bash
cd EventHandling
dotnet run
```

### 📤 Sample Output
```
Video Vlog-1 encoding...!
MailService: Sending an email...
MessageService: Sending a message...
```

---

## 2️⃣ TemperatureEventHandling

### 📝 Description
A real-world simulation of a **Heat Sensor** that monitors temperature and raises different events based on temperature levels:
- 🔵 **Normal** - Temperature is safe
- 🟡 **Warning** - Temperature exceeds warning threshold (27°C)
- 🔴 **Emergency** - Temperature exceeds emergency threshold (75°C)

### 🔑 Key Concepts
- **Multiple Events** from a single publisher
- **Custom EventArgs** (`TemperatureEventArgs`) with additional data
- **Conditional Event Raising** based on business logic
- Using `EventHandler<T>` generic delegate

### 📂 Files
- `Program.cs` - Contains all classes:
  - `TemperatureEventArgs` - Custom event data with Temperature and Time
  - `HeatSensor` - Publisher with multiple events
  - `Thermostat` - Subscriber that responds to temperature changes

### 💻 Event Flow
```
HeatSensor reads temperature data
    ↓
If temp >= 75°C → Raise EmergencyReached 🔴
If temp >= 27°C → Raise WarningReached 🟡
If temp back to normal → Raise TemperatureNormal 🔵
    ↓
Thermostat receives event and takes action
```

### ▶️ Run the Project
```bash
cd TemperatureEventHandling
dotnet run
```

### 📤 Sample Output
```
Temperature: 16
Temperature: 17
...
Temperature: 28.7
⚠ 28.7 Warning: Cooling ON
Temperature: 27.6
⚠ 27.6 Warning: Cooling ON
Temperature: 26
ℹ Temperature Normal: Cooling OFF
...
Temperature: 86
🚨 Emergency: Device Shutdown
```

---

## 3️⃣ EventHandlerListUses

### 📝 Description
Demonstrates **memory-efficient event handling** using `EventHandlerList` from `System.ComponentModel`. This approach is ideal when you have **many events** but most may never have subscribers.

### 🔑 Key Concepts
- **EventHandlerList** - Stores event handlers in a dictionary-like structure
- **Lazy Event Storage** - Memory allocated only when subscribers exist
- **Custom add/remove accessors** for events
- **Publisher-Subscriber Pattern** for download simulation

### 📂 Files
- `Program.cs` - Contains all classes:
  - `UrlEventArgs` / `FileEventArgs` - Custom event data
  - `DownloadCreatorPub` - Publisher with 3 events (Start, Progress, End)
  - `DonwloadListenSub` - Subscriber that tracks download lifecycle

### 💻 Download Flow
```
DownloadCreatorPub                      DonwloadListenSub
       |                                        |
       |--- Start() called ------------------->|
       |                                        |
       |--- OnStartDownload() fires ---------->| "File downloading..."
       |                                        |
       |--- OnProgressDownload() fires ------->| "Download progress..."
       |                                        |
       |--- OnEndDownload() fires ------------>| "Download completed."
```

### ▶️ Run the Project
```bash
cd EventHandlerListUses
dotnet run
```

### 📤 Sample Output
```
Download Manager Start..!
File downloading....
File can't download!
Download Manager Stoped..!

Download Manager Start..!
File downloading....
Donwload progress, File : main.csv
File download completed.
Download Manager Stoped..!
```

---

## 🏃 How to Run All Projects

### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download) or later

### Run Individual Projects
```bash
# Project 1
cd EventHandling
dotnet run

# Project 2
cd TemperatureEventHandling
dotnet run

# Project 3
cd EventHandlerListUses
dotnet run
```

---

## 📖 Event Handling Pattern Summary

```
┌─────────────────────────────────────────────────────────────────┐
│                     EVENT HANDLING PATTERN                       │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  STEP 1: Define Event (in Publisher)                            │
│  ────────────────────────────────────                            │
│  public event EventHandler<MyEventArgs> MyEvent;                 │
│                                                                  │
│  STEP 2: Raise Event (in Publisher)                              │
│  ────────────────────────────────────                            │
│  MyEvent?.Invoke(this, new MyEventArgs { ... });                 │
│                                                                  │
│  STEP 3: Subscribe (in Subscriber)                               │
│  ────────────────────────────────────                            │
│  publisher.MyEvent += OnMyEventHandler;                          │
│                                                                  │
│  STEP 4: Handle Event (in Subscriber)                            │
│  ────────────────────────────────────                            │
│  void OnMyEventHandler(object sender, MyEventArgs e) { ... }     │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
```

---

## 👨‍💻 Author

Kartik Ahir - Created as part of C# learning journey.

---

## 📄 License

This project is for educational purposes. Feel free to use!
