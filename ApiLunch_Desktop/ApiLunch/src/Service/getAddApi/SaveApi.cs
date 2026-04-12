using System.Text.Json;
using System.Windows;
using ApiLunch.DataBase;
using ApiLunch.Dto;
using Microsoft.Web.WebView2.Core;

namespace ApiLunch.Service;

public class SaveApi
{
    private static IDataBase _dataBase = new ListDataBase();
    
    public static void Save(Microsoft.Web.WebView2.Wpf.WebView2 webView, AddApiDto messageGetApi)
    {
        _dataBase.AddDataDb(messageGetApi);
    
        var messagePostApi = JsonSerializer.Serialize(_dataBase.GetDataDb);
                    
        webView.CoreWebView2.PostWebMessageAsJson(messagePostApi);
        MessageBox.Show("Api guardada con exito");
    }
}