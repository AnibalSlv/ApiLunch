using System.Text.Json;
using System.Windows;
using Microsoft.Web.WebView2.Core;
using ApiLunch.Utils;

namespace ApiLunch.Service;

public class SendSelectFile
{
    private static readonly OpenFileDialog DialogService = new OpenFileDialog();
    
    public static void Send(Microsoft.Web.WebView2.Wpf.WebView2 webView)
    {
        string result = DialogService.OpenFile();
        
        if (result == string.Empty)
        {
            MessageBox.Show("Error: Seleccione un archivo valido");
        }
        else
        {
            var postOpenDialogResult = JsonSerializer.Serialize(result);
            webView.CoreWebView2.PostWebMessageAsJson(postOpenDialogResult);
            MessageBox.Show(result ,"Archivo Seleccionado exitosamente");
        }
    }
}
