using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Python.Runtime;
using System;
using System.IO;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Diagnostics;
using System.ComponentModel;
using System.Data;
using UnityEditor;

public class displayImage : MonoBehaviour
{
    public String imageName;
    public bool drawn = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!drawn){
            StartCoroutine(LoadImage());
            drawn = true;
        }
        
    }

    IEnumerator LoadImage()
    {
        //var imgPath = Directory.GetCurrentDirectory();
        //imgPath += Application.streamingAssetsPath + "/" + imageName; //Python currently creates 1 image of randomized pixels called text.jpg.
        //imgPath += "\\" + imageName;
        string location= Application.streamingAssetsPath +"/";
        var imgPath = location + imageName;
        UnityEngine.Debug.Log(imgPath);
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(imgPath))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                UnityEngine.Debug.Log(uwr.error);
            }
            else
            {
                var texture = DownloadHandlerTexture.GetContent(uwr);

                var material = new Material(Shader.Find("UI/Default"));
                material.mainTexture = texture;

                //GetComponent<Image>().material = material;
                GetComponent<Renderer>().material.mainTexture = texture;
            }
        }
    }

    /*public void OnApplicationQuit()
    {
        if (PythonEngine.IsInitialized)
        {
            print("ending python");
            PythonEngine.Shutdown();
        }
    } */
}
