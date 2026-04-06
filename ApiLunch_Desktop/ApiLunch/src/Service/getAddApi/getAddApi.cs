using System.Text.Json;
using Microsoft.Web.WebView2.Core;
using System.Windows;
using ApiLunch.Dto;
using ApiLunch.DataBase;
using Microsoft.Web.WebView2.WinForms;

namespace ApiLunch.Service;

public class GetAddApi
{
    private OpenDialog _dialogService = new OpenDialog();
    private ListDataBase _listDb = new ListDataBase();
    public static event Action<PostWebViewMessage>? OnSendMessage; // Estoy casi seguro de que no necesito esto ya
    // Porque eso era para hacer un puente con el Main para acceder a webview 
    
    
    public void OnWebMessageReceived(object? sender, CoreWebView2WebMessageReceivedEventArgs e)
    {
        
        // El mensaje llega como un JSON string
        string jsonString = e.WebMessageAsJson ?? string.Empty;
        
        var webView = sender as Microsoft.Web.WebView2.Wpf.WebView2;

        try
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            AddApiDto messageGetApi = JsonSerializer.Deserialize<AddApiDto>(jsonString, options);

            if (jsonString.Contains("OpenDialogFile"))
            {
                _dialogService.OpenFolder();
            }
            
            if (jsonString.Contains("SaveAddApi") && messageGetApi != null)
            {
                _listDb.AddDataDb(messageGetApi);
    
                var messagePostApi = JsonSerializer.Serialize(_listDb.GetDataDb);
                    
                webView.CoreWebView2.PostWebMessageAsJson(messagePostApi);
                MessageBox.Show("Api guardada con exito");
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