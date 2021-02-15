using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using TMPro;
using System.Collections;

public class SaveSystem : MonoBehaviour
{
    public static void SaveTreeXML(HMapGen hMap) {        
        XmlSerializer formatter = new XmlSerializer(typeof(HMapGen));
        //string path = input.text;
        string path = @"c:\Users\Aleksander\Documents\tree.xml";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, hMap);
        stream.Close();
    }  

    public HMapGen LoadTreeBinary() {
        //string path = input.text;
        string path = @"c:\Users\Aleksander\Documents\tree.binary";
        if(File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            HMapGen data = formatter.Deserialize(stream) as HMapGen;
            stream.Close();
            return data;
        } else {
            return null;
        }
    }

    public static HMapGen LoadTreeXml() {
        //string path = input.text;
        string path = @"c:\Users\Aleksander\Documents\tree.xml";
        if(File.Exists(path)) {
            XmlSerializer formatter = new XmlSerializer(typeof(HMapGen));
            FileStream stream = new FileStream(path, FileMode.Open);

            HMapGen data = formatter.Deserialize(stream) as HMapGen;
            stream.Close();
            return data;
        } else {
          //  StartCoroutine(ShowWarn(path, 3));
            return null;
        }
    }

    public static void SaveParamsXML(ParamsData data, string path) {
        XmlSerializer formatter = new XmlSerializer(typeof(ParamsData));
        //string path = @"c:\Users\Aleksander\Documents\params.xml";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static ParamsData LoadParamsXML(string path) {
        //string path = input.text;
        if(File.Exists(path)) {
            XmlSerializer formatter = new XmlSerializer(typeof(ParamsData));
            FileStream stream = new FileStream(path, FileMode.Open);

            ParamsData data = formatter.Deserialize(stream) as ParamsData;
            stream.Close();
            return data;
        } else { 
            return null;
        }
    }
}
