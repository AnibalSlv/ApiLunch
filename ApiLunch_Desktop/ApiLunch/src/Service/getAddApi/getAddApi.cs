using System.Text.Json;
using Microsoft.Web.WebView2.Core;
using System.Windows;
using ApiLunch.Dto;
using ApiLunch.Utils;

namespace ApiLunch.Service;

public class GetAddApi
{
    public void OnWebMessageReceived(object? sender, CoreWebView2WebMessageReceivedEventArgs e)
    {
        // El mensaje llega como un JSON string
        string jsonString = e.WebMessageAsJson ?? string.Empty;
        
        var webView = sender as Microsoft.Web.WebView2.Wpf.WebView2;

        try
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            AddApiDto messageGetApi = JsonSerializer.Deserialize<AddApiDto>(jsonString, options);
            
            switch (jsonString)
            {
                // Solo se sjecuta si el string "s" contiene "OpenDialogFile"
                case not null when jsonString.Contains("OpenFileDialog"): 
                    SendSelectFile.Send(webView);
                    break;
                case not null when jsonString.Contains("OpenFolderDialog"):
                    SendSelectFolder.Send(webView);
                    break;
                case not null when jsonString.Contains("SaveAddApi"):
                    SaveApi.Save(webView, messageGetApi);
                    break;
                default:
                    Console.WriteLine("Este mensaje llego sin una accion valida");
                    break;
            }
        }
        catch (JsonException ex)
        {
            MessageBox.Show($"Error Json: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error Global: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}