using System.Text.Json;
using System.Windows;
using Microsoft.Web.WebView2.Core;
using ApiLunch.Utils;

namespace ApiLunch.Service;

public class SendSelectFolder
{
    private static readonly OpenFolderDialog DialogService = new OpenFolderDialog();
    
    public static void Send(Microsoft.Web.WebView2.Wpf.WebView2 webView)
    {
        string result = DialogService.OpenFolder();
        
        if (result == string.Empty)
        {
            MessageBox.Show("Error: Seleccione una carpeta valida");
        }
        else
        {
            var postOpenDialogResult = JsonSerializer.Serialize(result);
            webView.CoreWebView2.PostWebMessageAsJson(postOpenDialogResult);
            MessageBox.Show(result ,"Carpeta Seleccionada exitosamente");
        }
    }
}