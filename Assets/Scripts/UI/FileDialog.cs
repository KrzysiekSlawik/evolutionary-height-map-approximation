using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Windows.Forms;
using System.IO;

static class FileDialog
{
    public static string xmlFilter = "XML Files|*.xml";
    public static string textureFilter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";
    [DllImport("user32.dll")]
    private static extern void OpenFileDialog();
    public static bool TryGetOpenFilePath(out string path, string filters = "")
    {
        OpenFileDialog dialog = new OpenFileDialog();
        dialog.Filter = filters;
        if(dialog.ShowDialog() == DialogResult.OK)
        {
            path = dialog.FileName;
            return true;
        }
        path = null;
        return false;
    }
    public static bool TryGetSaveFilePath(out string path, string filters = "")
    {
        SaveFileDialog dialog = new SaveFileDialog();
        dialog.Filter = filters;
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            path = dialog.FileName;
            return true;
        }
        path = null;
        return false;
    }
}
