using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Web.WebView2.Core;

using ApiLunch.Service;
using ApiLunch.Service.RunApi;
using Microsoft.Web.WebView2.Wpf;
namespace ApiLunch;

public partial class MainWindow : Window
{
    private GetAddApi _getAddApi; // <-- Si necesita tener valores y no sucede pon
    // _getAddApi = new GetAddApi(); dentro de mainWindow antes de IntializeWebView()

    private RunApi _runApi = new RunApi();
    
    public MainWindow()
    {
        InitializeComponent();
        _getAddApi = new GetAddApi();
        InitializeWebView();
    }
    
    async void InitializeWebView()
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = "cmd.exe",
            Arguments = "/c npm run dev", 
            WorkingDirectory = @"D:\Proyectos\ApiLunch\apiLunch_web",
            CreateNoWindow = true, // Abre la terminal
            UseShellExecute = true
        });
    
        await Task.Delay(6500); 
    
        await WebView.EnsureCoreWebView2Async(null);
        WebView.Source = new Uri("http://localhost:5173/");
        
            
        // TODO Avisa a C# para que cuando Vite envie un msg él lo pueda recibir
        WebView.WebMessageReceived  += _getAddApi.OnWebMessageReceived;
        WebView.WebMessageReceived += _runApi.OnWebMessageReceived2;
    }
    
    protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
    {
        // Esto busca cualquier proceso de Node que esté corriendo y lo cierra. (incluye: cualquier proceso)
        Process.Start(new ProcessStartInfo {
            FileName = "taskkill",
            Arguments = "/F /IM node.exe /T",
            CreateNoWindow = true,
            UseShellExecute = false
        });
        base.OnClosing(e);
    }
    
    
}