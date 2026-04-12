namespace ApiLunch.Utils;

public class OpenFolderDialog
{
    public string OpenFolder()
    {
        var dialog = new Microsoft.Win32.OpenFolderDialog();
        dialog.Title = "Select a folder";
        dialog.Multiselect = false; // Set to true to allow multiple folders

        bool? result = dialog.ShowDialog();

        if (result == true)
        {
            return dialog.FolderName;
        }
        return string.Empty;
    }
}