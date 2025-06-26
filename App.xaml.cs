#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
using WinRT.Interop;
#endif

namespace Steel
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = new Window(new AppShell());

#if WINDOWS
                    // Apply Fluent title bar styling  
                    window.Created += (s, e) =>  
                    {  
                        var handler = window.Handler;  
                        if (handler?.PlatformView is Microsoft.UI.Xaml.Window mauiWindow)  
                        {  
                            var hwnd = WindowNative.GetWindowHandle(mauiWindow);  
                            var windowId = Win32Interop.GetWindowIdFromWindow(hwnd);  
                            var appWindow = AppWindow.GetFromWindowId(windowId);  

                            // Extend content into the title bar  
                            appWindow.TitleBar.ExtendsContentIntoTitleBar = true;  

                            // Make title bar buttons blend with the app  
                            appWindow.TitleBar.ButtonBackgroundColor = Microsoft.UI.Colors.Transparent;  
                            appWindow.TitleBar.ButtonInactiveBackgroundColor = Microsoft.UI.Colors.Transparent;  
                            appWindow.TitleBar.ButtonHoverBackgroundColor =ColorHelper.FromArgb(255, 45, 45, 48); // Optional hover color  
                            appWindow.TitleBar.ButtonPressedBackgroundColor = ColorHelper.FromArgb(255, 28, 28, 30);  

                            // Optional: Make the full title bar transparent too  
                            appWindow.TitleBar.BackgroundColor = Microsoft.UI.Colors.Transparent;  
                            appWindow.TitleBar.InactiveBackgroundColor =Microsoft.UI.Colors.Transparent;  
                        }  
                    };  
#endif

            return window;
        }
    }
}
