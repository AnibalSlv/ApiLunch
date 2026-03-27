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

namespace ApiLunch;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
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
    
        await Task.Delay(4000); 
    
        await WebView2.EnsureCoreWebView2Async(null);
            WebView2.Source = new Uri("http://localhost:5173/");
            
        WebView2.WebMessageReceived += OnWebMessageReceived; // Avisa a C# para que cuando Vite envie un msg él lo pueda recibir
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

    private void OpenFileDialog()
    {
        // Configure open file dialog box
        var dialog = new Microsoft.Win32.OpenFileDialog();
        dialog.FileName = "*"; // Default file name
        dialog.DefaultExt = ".exe"; // Default file extension
        dialog.Filter = "Python Archives|*.exe"; // Filter files by extension

        // Show open file dialog box
        bool? result = dialog.ShowDialog();

        // Process open file dialog box results
        if (result == true)
        {
            // Open document
            string filename = dialog.FileName; // ruta
            Console.WriteLine(filename);
            
            // 1. Prepara la información del proceso (para lanzar procesos
            // Windows pregunta: ¿con qué programa?, ¿qué carpetas?, ¿con qué permisos?)
            ProcessStartInfo startInfo = new ProcessStartInfo();

            // 2. El "FileName" DEBE ser el ejecutable de Python
            // Si tienes python en el PATH, solo pon "python". Si no, pon la ruta completa al python.exe
            startInfo.FileName = $"{filename}"; 

            // 3. Ejecutamos el módulo uvicorn directamente
            startInfo.Arguments = "-m uvicorn main:app --host 127.0.0.1 --port 8000";
            startInfo.WorkingDirectory = @"D:\Proyectos\Velo\Server"; // Sin esto no sabe donde está el main a ejecutar 
            
            // 4. Inicia el proceso
            Process.Start(startInfo);
        }
    }
    
    private void OnWebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
    {
        // El mensaje llega como un JSON string
        string jsonString = e.WebMessageAsJson;
        
        // MessageBox.Show($"¡Mensaje recibido desde Vite!: {jsonString}");

        if (jsonString.Contains("OpenDialogFile")) 
        {
            OpenFileDialog();
        }   
    }
}