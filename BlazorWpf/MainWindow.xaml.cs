using BlazorWpf.Invoke;
using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BlazorWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BlazorInterop _interop;
        public MainWindow()
        {
            Resources.Add("services", Startup.Services);
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private void WebView_WebMessageReceived(object? sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            // 处理来自 Blazor 组件的消息
            var msg = e.TryGetWebMessageAsString();
            if (msg == "dialog")
            {
                _interop.OpenFile();
            }

            this.Dispatcher.Invoke(() =>
            {
                // Dark
                if (msg == "Theme-True")
                {
                    Wpf.Ui.Appearance.Theme.Apply(Wpf.Ui.Appearance.ThemeType.Dark);
                }
                else if (msg == "Theme-False")
                {
                    Wpf.Ui.Appearance.Theme.Apply(Wpf.Ui.Appearance.ThemeType.Light);
                }
            });
         
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await webView.WebView.EnsureCoreWebView2Async();
            webView.WebView.WebMessageReceived += WebView_WebMessageReceived;
            _interop = new BlazorInterop(webView.WebView.CoreWebView2);

        }

        private void TrayMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem)
            {
                var item = sender as MenuItem;

                if (item == null) return;

                if (item.Tag.Equals("桌面"))
                {
                    this.WindowState = WindowState.Normal;
                }
            }
        }
    }
}
