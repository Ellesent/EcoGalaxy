using UnityEngine;
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
    public static bool isLoaded;
    public Level blah;

	// Use this for initialization
	void Start () {
        objects = new List<GameObject>();
        control = this;

        if (Application.loadedLevel == 3)
        {
            AchievementManager.LockAchievement("first");
        }
        
       // isLoaded = false;
       
	
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
       // Achievements.first.Value = true;
        BinaryFormatter bf = new BinaryFormatter();

        //create needed files
        FileStream file = File.Create(Application.persistentDataPath + "/PlayerInfo.dat");
        FileStream res = File.Create(Application.persistentDataPath + "/ResourceInfo.dat");

       // Get current resource numbers
        ResourceControl data = new ResourceControl();
        data.money = MoneyManager.money;
        data.water = MoneyManager.water;
        data.food = MoneyManager.food;
        data.power = MoneyManager.power;
        data.buildMat = MoneyManager.buildMat;
        data.rating = MoneyManager.rating;
        data.pop = MoneyManager.pop;
        data.conquered = MoneyManager.conquered;
        data.oxygen = MoneyManager.Oxygen;
        
        //serialize the information to the specified file
        bf.Serialize(file, ConvertObjects());
        bf.Serialize(res, data);

        //close the files and confirm save
        file.Close();
        res.Close();
        Debug.Log("Saved");
       
    }
    public void DeleteSave()
    {
        BinaryFormatter bf = new BinaryFormatter();
        File.Delete(Application.persistentDataPath + "/PlayerInfo.dat");
        File.Delete(Application.persistentDataPath + "/ResourceInfo.dat");
        objects.Clear();

    }

    public void Load()
    {
        //check if file exists
        if (File.Exists(Application.persistentDataPath + "/ResourceInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();

            //open file and deserialize data into an object we can work with
            FileStream res = File.Open(Application.persistentDataPath + "/ResourceInfo.dat", FileMode.Open);
            ResourceControl dat = (ResourceControl)bf.Deserialize(res);

            //Set all resources to loaded resources
            MoneyManager.money = dat.money;
            MoneyManager.water = dat.water;
            MoneyManager.food = dat.food;
            MoneyManager.power = dat.power;
            MoneyManager.pop = dat.pop;
            MoneyManager.rating = dat.rating;
            MoneyManager.Oxygen = dat.oxygen;
            MoneyManager.buildMat = dat.buildMat;
            MoneyManager.conquered = dat.conquered;

            Debug.Log(dat.oxygen);
            res.Close();
            
        }
        
        if (File.Exists(Application.persistentDataPath + "/PlayerInfo.dat"))
        {

                isLoaded = true;
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/PlayerInfo.dat", FileMode.Open);
                string data = (string)bf.Deserialize(file);
                
                file.Close();
                blah = LgJsonNode.CreateFromJsonString<Level>(data);
                if (Application.loadedLevel != 3)
                {
                    Application.LoadLevel(3);
                }
                else
                {
                   List<GameObject> solarPanel = new List<GameObject>();
                   List<GameObject> oxygenList = new List<GameObject>();
                   List<GameObject> foodList = new List<GameObject>();
                   solarPanel.AddRange(GameObject.FindGameObjectsWithTag("SolarPanel"));
                   oxygenList.AddRange(GameObject.FindGameObjectsWithTag("Oxygen"));
                   foodList.AddRange(GameObject.FindGameObjectsWithTag("Food"));
                    foreach (GameObject sol in solarPanel)
                    {
                        GameObject temp = sol;
                        Destroy(temp);
                        objects.Remove(sol);
                    }
                    foreach(GameObject ox in oxygenList)
                    {
                        GameObject temp = ox;
                        Destroy(temp);
                        objects.Remove(ox);
                    }
                    foreach (GameObject fo in foodList)
                    {
                        GameObject temp = fo;
                        Destroy(temp);
                        objects.Remove(fo);
                    }

                }
                
               blah.HandleNewObject();
                //Debug.Log(data);
       
            
            
            
           // test = data.test;
           // Debug.Log(data.test.name);
           // Instantiate(test);
        }
    }

    //void OnLevelWasLoaded(int level)
    //{
    //    if ( level == 3 && isLoaded == true)
    //    {
    //        Debug.Log("Handle");
    //        blah.HandleNewObject();
           
    //    }
    //}

    private void DontDestroyOnLoad()
    {
        throw new NotImplementedException();
    }

  
}
[Serializable]
public class ResourceControl
{
    public int money;
    public int water;
    public int food;
    public int power;
    public int buildMat;
    public int rating;
    public int pop;
    public int conquered;
    public bool oxygen; 
}

//[Serializable]
//class PlayerData
//{
//    List<GameObject> blah = new List<GameObject>();
//    List<string> help = new List<string>();

    

//}
