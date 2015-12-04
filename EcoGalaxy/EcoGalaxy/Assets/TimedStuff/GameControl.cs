﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using LgOctEngine.CoreClasses;
public class GameControl : MonoBehaviour {

    public static List<GameObject> objects;
    public List<GameObject> testOfObjects;
    public static GameObject test; 
    public static string name;
    public static GameControl control;
    public static bool isLoaded = false;

	// Use this for initialization
	void Start () {
        objects = new List<GameObject>();
        control = this;
       
	
	}
	
	// Update is called once per frame
	void Update () {
        testOfObjects = objects;
        
       
	}

    public string ConvertObjects()
    {
        arrayBuild jsonMessage = new arrayBuild();
        foreach (GameObject thisObject in objects)
        {
            string longName = thisObject.transform.tag;
            float rotation = thisObject.transform.rotation.z;
            jsonMessage.AddObject(longName, thisObject.transform.position.x, thisObject.transform.position.y, rotation, 3);
            
        }
        string message = jsonMessage.serializeArray();
        return message;
    }

    

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/PlayerInfo.dat");

       // PlayerData data = new PlayerData();

        //data.test = test;

        bf.Serialize(file, ConvertObjects());
        file.Close();
        Debug.Log("Saved");
       
    }
    public void DeleteSave()
    {
        BinaryFormatter bf = new BinaryFormatter();
        File.Delete(Application.persistentDataPath + "/PlayerInfo.dat");

    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/PlayerInfo.dat"))
        {

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/PlayerInfo.dat", FileMode.Open);
            string data = (string)bf.Deserialize(file);
            file.Close();
            isLoaded = true;
            Level blah = LgJsonNode.CreateFromJsonString<Level>(data);
            blah.HandleNewObject();
            
            Debug.Log(data);
           // test = data.test;
           // Debug.Log(data.test.name);
           // Instantiate(test);
        }
    }

  
}

//[Serializable]
//class PlayerData
//{
//    List<GameObject> blah = new List<GameObject>();
//    List<string> help = new List<string>();

    

//}