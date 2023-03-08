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

public class makeImages : MonoBehaviour
{
    // Start is called before the first frame update
    public String prompt;
    void Start()
    {
        //Runtime.PythonDLL = Application.dataPath + "/python-3.10.10-embed-amd64/python310.dll";
        Runtime.PythonDLL = Application.streamingAssetsPath + "/python-3.10.10-embed-amd64/python310.dll";
        PythonEngine.Initialize();
        using (Py.GIL())
        {
            dynamic imageCreator = Py.Import("create");
            string location= Application.streamingAssetsPath +"/";
            UnityEngine.Debug.LogError(location);
            string l1 = location + "image1.jpg";
            //try {
                dynamic image1 = imageCreator.makeImage(prompt, l1); //+ " abstract style painting"
                //dynamic image2 = imageCreator.makeImage(prompt, location +  "image2.jpg"); //+ " cubism style print"
                //dynamic image3 = imageCreator.makeImage(prompt, location + "image3.jpg"); // + " realistic image"
                
                //StartCoroutine(LoadImage());
            //} catch{
                //UnityEngine.Debug.LogError("Problem with code");
                //We need to replace this with something better for the user
           // } 
        } 
    }

    IEnumerator LoadImage()
    {
        var imgPath = Directory.GetCurrentDirectory();
        //imgPath += Application.streamingAssetsPath + "/" + imageName; //Python currently creates 1 image of randomized pixels called text.jpg.
        //imgPath += "\\" + imageName;
        string location= Application.streamingAssetsPath +"/";
        imgPath = location + "image1.jpg";
        //UnityEngine.Debug.Log(imgPath);
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
    
   public void OnApplicationQuit()
    {
        if (PythonEngine.IsInitialized)
        {
            print("ending python");
            PythonEngine.Shutdown();
        }
    }
}
