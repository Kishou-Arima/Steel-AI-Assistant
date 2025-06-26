#if WINDOWS
using Windows.System;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Windows.UI.Core;
#endif

using Steel.Services;
//using Steel.Services.Steel.Services;
using Steel.ViewModels;
using System.Threading.Tasks;

namespace Steel;

public partial class MainPage : ContentPage
{
    private readonly VoiceInputService _voiceInputService;

    public MainPage()
    {
        InitializeComponent();
        BindingContext = new ChatViewModel();

        _voiceInputService = new VoiceInputService();
        _voiceInputService.VoiceRecognized += OnVoiceRecognized;
        _voiceInputService.VoiceStateChanged += OnVoiceStateChanged;

#if WINDOWS
        // Enable Enter/Shift+Enter behavior on Windows  
        InputEditor.HandlerChanged += (s, e) =>  
        {  
            var nativeEditor = InputEditor.Handler?.PlatformView as TextBox;  

            if (nativeEditor != null)  
            {  
                nativeEditor.AcceptsReturn = true;  

                nativeEditor.KeyUp += (sender, args) =>  
                {  
                    if (args.Key == VirtualKey.Enter) // Replace 'Keys.Enter' with 'VirtualKey.Enter'  
                    {  
                        var shift = CoreWindow.GetForCurrentThread()  
                                              ?.GetKeyState(VirtualKey.Shift)  
                                              .HasFlag(CoreVirtualKeyStates.Down) ?? false;  

                        if (!shift)  
                        {  
                            (BindingContext as ChatViewModel)?.SendCommand.Execute(null);  
                            args.Handled = true;  
                        }  
                    }  
                };  
            }  
        };  
#endif
    }

    private async void OnMicClicked(object sender, EventArgs e)
    {
        await _voiceInputService.StartRecognitionAsync();
    }

    private void OnVoiceRecognized(object? sender, string recognizedText)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            InputEditor.Text = recognizedText;
        });
    }

    private void OnVoiceStateChanged(object? sender, bool isRecording)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            // Swap visibility using opacity  
            MicIdleIcon.Opacity = isRecording ? 0 : 1;
            MicRecordingIcon.Opacity = isRecording ? 1 : 0;
        });
    }
}
