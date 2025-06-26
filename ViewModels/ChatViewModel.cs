using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Steel.Models;
using Steel.Services;


namespace Steel.ViewModels
{
    public class ChatViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Message> Messages { get; set; } = new();

        private string _userInput = string.Empty;
        public string UserInput
        {
            get => _userInput;
            set
            {
                if (_userInput != value)
                {
                    _userInput = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand SendCommand => new Command(async () => await SendMessageAsync());

        private readonly OpenAIService _apiService = new();
        private readonly string _apiKey = SecretService.GetOpenAIApiKey();
        public event PropertyChangedEventHandler? PropertyChanged;

        private async Task SendMessageAsync()
        {
            if (string.IsNullOrWhiteSpace(UserInput)) return;

            Messages.Add(new Message { Content = UserInput, IsUser = true });

            var reply = await _apiService.GetResponseAsync(UserInput, _apiKey);
            Messages.Add(new Message { Content = reply, IsUser = false });

            UserInput = string.Empty;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
