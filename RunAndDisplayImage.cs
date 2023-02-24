using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.Diagnostics;
using System.ComponentModel;
using System.Data;
using System.IO;
using UnityEditor;


public class RunAndDisplayImage : MonoBehaviour
{

    public String imageName;
    // Start is called before the first frame update
    void Start()
    {
        var path = Directory.GetCurrentDirectory();
        path += "/python_exe/python_exe.exe";
        var draw = Process.Start(path);
        draw.WaitForExit();

        StartCoroutine(LoadImage());
    }

    IEnumerator LoadImage()
    {
        var imgPath = Directory.GetCurrentDirectory();
        imgPath += "\\" + imageName + ".jpg"; //Python currently creates 1 image of randomized pixels called text.jpg.
        //imgPath += "\\" + imageName + ".png";
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
