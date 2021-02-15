using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MapLoader : MonoBehaviour
{
    private MeshRenderer render;
    private Vector2Int size;
    public Texture2D def;
    private TempEvo evo;
    private HMapGen target;
    public static HMapGen currentMap;
    public Slider mapSlider;
    private Texture2D targetTex;
    private Texture2D currentTex;
    public UnityEvent OnFinishComputing;
    
    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<MeshRenderer>();
        size = new Vector2Int(500, 500);
        mapSlider.onValueChanged.AddListener(OnSliderChanged);
        currentMap = new ConstHMap();
        target = FileReceiver.loadedHMap;
        float[] genes = { 0, 0, 0 };
        currentMap.SetGenes(genes);
        LoadMap(target, out targetTex);
        LoadMap(currentMap, out currentTex);
        OnSliderChanged(0.5f);
    }

    public void LoadMap(HMapGen map, out Texture2D texture)
    {
        texture = new Texture2D(size.x, size.y);
        for(int x = 0; x < size.x; x++)
        {
            for(int y = 0; y < size.y; y++)
            {
                Color c = new Color();
                c.r = c.g = c.b = map.GetValue((float)x / (float)size.x, (float)y / (float)size.y);
                texture.SetPixel(x, y, c);
            }
        }
        texture.Apply();
        render.material.SetTexture("heightMap", texture);
    }
    public void ReLoadMap()
    {
        target = FileReceiver.loadedHMap;
        LoadMap(target, out targetTex);
    }
    public void SetMap(float proc)
    {
        Texture2D texture = new Texture2D(size.x, size.y);
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Color c = new Color();
                if(x < size.x * (1f - proc))
                {
                    c = currentTex.GetPixel(x, y);
                }
                else
                {
                    c = targetTex.GetPixel(x, y);
                }
                texture.SetPixel(x, y, c);
            }
        }
        texture.Apply();
        render.material.SetTexture("heightMap", texture);
    }

    public void RestartEvo()
    {
        target = FileReceiver.loadedHMap;
        evo = new TempEvo();
        currentMap = evo.Evolution().root;
        LoadMap(target, out targetTex);
        LoadMap(currentMap, out currentTex);
        OnSliderChanged(mapSlider.value);
        OnFinishComputing.Invoke();
    }
    
    public void Restart() {
        Invoke("RestartEvo", 0.05f);
    }

    public void Improve() {
        Invoke("ImproveEvo", 0.05f);
    }
    
    public void ImproveEvo()
    {
        currentMap = evo.ImprovePopulation(ConstParameters.evoLoopDuration).root;
        LoadMap(currentMap, out currentTex);
        OnSliderChanged(mapSlider.value);
        Debug.Log(currentMap); 
        OnFinishComputing.Invoke();
    }
    public void OnSliderChanged(float value)
    {
        SetMap(value);
    }
}
