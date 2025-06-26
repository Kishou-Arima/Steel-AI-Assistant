#if WINDOWS
using Windows.Media.SpeechRecognition;
using Windows.Foundation;
using System.Threading;
#endif
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Steel.Services
{
    public class VoiceInputService
    {
#if WINDOWS
            private SpeechRecognizer _recognizer;  
            private bool _isRecognizing;  
            private CancellationTokenSource? _cts;  

            public event EventHandler<string>? VoiceRecognized;  
            public event EventHandler<bool>? VoiceStateChanged;  

            public VoiceInputService()  
            {  
                _recognizer = new SpeechRecognizer();  
                _recognizer.Timeouts.InitialSilenceTimeout = TimeSpan.FromSeconds(2);  
                _recognizer.Timeouts.BabbleTimeout = TimeSpan.FromSeconds(4);  
                _recognizer.Constraints.Add(  
                    new SpeechRecognitionTopicConstraint(  
                        SpeechRecognitionScenario.Dictation, "dictation")  
                );  
                _ = _recognizer.CompileConstraintsAsync();  
            }  

            public async Task StartRecognitionAsync()  
            {  
                if (_isRecognizing)  
                    return;  

                try  
                {  
                    _isRecognizing = true;  
                    VoiceStateChanged?.Invoke(this, true);  

                    _cts = new CancellationTokenSource();  
                    var resultTask = _recognizer.RecognizeAsync().AsTask(_cts.Token);  
                    var result = await resultTask;  

                    if (result.Status == SpeechRecognitionResultStatus.Success)  
                    {  
                        VoiceRecognized?.Invoke(this, result.Text);  
                    }  
                }  
                catch (OperationCanceledException)  
                {  
                    // Canceled by user or logic  
                }  
                catch (Exception ex)  
                {  
                    Debug.WriteLine($"Voice recognition error: {ex.Message}");  
                }  
                finally  
                {  
                    _isRecognizing = false;  
                    VoiceStateChanged?.Invoke(this, false);  
                }  
            }  

            public void CancelRecognition()  
            {  
                if (_isRecognizing)  
                {  
                    _cts?.Cancel();  
                }  
            }  
#else
        public event EventHandler<string>? VoiceRecognized;
        public event EventHandler<bool>? VoiceStateChanged;

        public Task StartRecognitionAsync()
        {
            Debug.WriteLine("Voice recognition is not supported on this platform.");
            return Task.CompletedTask;
        }

        public void CancelRecognition() { }
#endif
    }
}
