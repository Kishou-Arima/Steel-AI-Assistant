# 🧠 Steel — A Jarvis-style AI Assistant (WIP)

**Steel** is a lightweight, intelligent desktop assistant inspired by futuristic interfaces like *Jarvis* and *Max Steel*. This project is built using **.NET MAUI** and currently targets **Windows only**.

---

## 🚀 Current Features

- 💬 **Chat interface** powered by OpenAI GPT models  
- 📄 **MVVM architecture** with clean separation of logic and UI  
- 🌐 **Online mode** only (Offline mode planned)  
- 🎨 **Fluent UI styling** and minimal design  
- 🧪 **Voice recognition** support (in progress)

---

## 📦 Project Structure

Steel/

├── Converters/ # UI converters (e.g., visibility, binding)

├── Models/ # Data models (Message, etc.)

├── Platforms/ # Platform-specific code

├── Resources/ # Fonts, images, styles

├── Secrets/ # (API key config)

├── Services/ # Logic layer (OpenAI integration, voice input)

├── ViewModels/ # MVVM viewmodels

├── App.xaml, MainPage.xaml # Entry point UI

├── AppShell.xaml # Navigation shell

├── MauiProgram.cs # DI & startup logic


---

## 🗺️ Roadmap

- [x] GPT-4 Chat interface
- [ ] Online/Offline toggle in titlebar
- [ ] Local LLM support (e.g., LLaMA/Mistral via `llama.cpp`)
- [ ] File system access in offline mode
- [ ] Voice input toggle & microphone visualization
- [ ] Cross-platform support (Android/iOS/Linux)
- [ ] Assistant overlay for IDEs and design tools
- [ ] Command parsing and OS interaction (`open`, `search`, `schedule`, etc.)

---

## 🎤 Voice Recognition (WIP)

Voice input is currently under development and only partially integrated for **Windows** using native `SpeechRecognizer` APIs.  
**UI bindings and toggle controls are planned.**

To track this feature visually, use the [Canvas tab](canvas://) for real-time iterations.

---

## 🛠️ Build Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Visual Studio 2022+](https://visualstudio.microsoft.com/vs/) with:
  - MAUI workload
  - Windows App SDK
- Windows 10 (build 19041+) or later

---

## 💬 Contributions

This project is still in its early stages. Contributions, UI/UX suggestions, and experimental LLM integrations are welcome!

---

## 📄 License

MIT License © 2025 Utkaarsh Saha
