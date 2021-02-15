using System.Collections;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Events;

public class FileReceiver : MonoBehaviour {	
	float[] features;
	SliderManager[] sliders;
	public Texture2D defaultMap;
	public static HMapTexture loadedHMap;
    public UnityEvent loadSucces;
    public UnityEvent loadFailed;
    public UnityEvent saveSucces;
    public UnityEvent saveFailed;
    private void Start()
    {
        loadedHMap = new HMapTexture(defaultMap);
    }
    public void LoadTargetMap()
    {
        string path;
        if (FileDialog.TryGetOpenFilePath(out path, FileDialog.textureFilter))
        {
            if(Path.GetExtension(path) == ".xml")
            {
                XmlSerializer formatter = new XmlSerializer(typeof(HMapGen));
                FileStream stream = new FileStream(path, FileMode.Open);
                HMapGen map = formatter.Deserialize(stream) as HMapGen;
                loadedHMap = new HMapTexture(map); 
                stream.Close();
            }
            else
            {
                Texture2D tex = new Texture2D(1, 1);
                tex.LoadImage(System.IO.File.ReadAllBytes(path));
                loadedHMap = new HMapTexture(tex);
            }
            loadSucces.Invoke();
        }
        else
        {
            loadFailed.Invoke();
        }
    }
    public void SaveBestFit()
    {
        string path;
        if(FileDialog.TryGetSaveFilePath(out path, FileDialog.xmlFilter))
        {
            XmlSerializer formatter = new XmlSerializer(typeof(HMapGen));
            FileStream stream = new FileStream(path, FileMode.Create);
            formatter.Serialize(stream, MapLoader.currentMap);
            stream.Close();
        }
        else
        {
            saveFailed.Invoke();
        }
    }
}
