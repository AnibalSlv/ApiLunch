using System.Diagnostics;
using System.Windows;
using Microsoft.Win32;

namespace ApiLunch.Utils;

public class OpenFileDialog
{
    public string OpenFile()
    {
        // Configure open file dialog box
        var dialog = new Microsoft.Win32.OpenFileDialog();
        dialog.FileName = ""; // Default file name
        dialog.DefaultExt = ".exe"; // Default file extension
        dialog.Filter = "Exe|*.exe";; // Filter files by extension

        // Show open file dialog box
        bool? result = dialog.ShowDialog();

        // Process open file dialog box results
        if (result == true)
        {
            // Open document
            return dialog.FileName; // ruta

        }
        return string.Empty;
    }
}