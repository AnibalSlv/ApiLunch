using System.Diagnostics;
using System.Text.Json;
using Microsoft.Web.WebView2.Core;
using System.Windows;
using ApiLunch.Dto;

namespace ApiLunch.Service.RunApi;

public class RunApi
{
    public void OnWebMessageReceived2(object? sender, CoreWebView2WebMessageReceivedEventArgs e)
    {

        // El mensaje llega como un JSON string
        string jsonString = e.WebMessageAsJson ?? string.Empty;

        try
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            AddApiDto messageGetApi = JsonSerializer.Deserialize<AddApiDto>(jsonString, options);

            if (jsonString.Contains("RunApi"))
            {
                MessageBox.Show($"Intentando ejecutar: '{messageGetApi.Execute}' en la carpeta '{messageGetApi.Folder}'");
                // Open document
                string filename = messageGetApi.Execute; // ruta
                Console.WriteLine(filename);
                
                // 1. Prepara la información del proceso (para lanzar procesos
                // Windows pregunta: ¿con qué programa?, ¿qué carpetas?, ¿con qué permisos?)
                ProcessStartInfo startInfo = new ProcessStartInfo();

                // 2. El "FileName" DEBE ser el ejecutable de Python
                // Si tienes python en el PATH, solo pon "python". Si no, pon la ruta completa al python.exe
                startInfo.FileName = $"{filename}"; 

                // 3. Ejecutamos el módulo uvicorn directamente
                startInfo.Arguments = $"-m uvicorn main:app --host 127.0.0.1 --port {messageGetApi.Port}";
                startInfo.WorkingDirectory = $"{messageGetApi.Folder}"; // Sin esto no sabe donde está el main a ejecutar 
            
                // 4. Inicia el proceso
                Process.Start(startInfo);
                MessageBox.Show("Api ejecutada con exito");
            }

            if (messageGetApi != null)
            {

            }

        }
        catch (JsonException ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}