using System.Diagnostics;
using Microsoft.Win32; // Importante para no escribir la ruta larga siempre

namespace ApiLunch.Service;

public class OpenDialog
{
    public void OpenFolder()
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
}