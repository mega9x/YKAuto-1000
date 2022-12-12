using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.Metrics;


using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;
using YK1000.Views;

namespace YK1000;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        var services = new ServiceCollection();
        services.AddWindowsFormsBlazorWebView();
        webView.HostPage = "wwwroot\\index.html";
        webView.Services = services.BuildServiceProvider();
        webView.RootComponents.Add<App>("#app");
    }
}