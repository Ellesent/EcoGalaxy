#pragma warning disable 0219 // unused assignment
#pragma warning disable 0168 // assigned not used
#pragma warning disable 0414 // unused variables

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

//JSON Using
using LgOctEngine.CoreClasses;

public class NetManager : NetworkManager {
	
	

    public List<string> nameList;
    public List<int> idList;
    public List<string> ipList;

    public void Start()
    {

        //Save default level
       // sendLevel();

        //Save new levelList
        //saveLevelList();
    }


    
	
	
	
	

	//Call to send the level to the clients
	public void sendLevel()
	{
       

		//Gets all objects with the tag "Level"
		Debug.Log ("Getting all tagged objects");
		GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Level");

        //Create class for JSON message
        arrayBuild jsonMessage = new arrayBuild();
		
		//Convert to proper list
		Debug.Log ("Converting to proper listing");
		foreach (GameObject thisObject in allObjects)
		{
			//Gets Objects proper name without all the duplicate issues
			string longName = thisObject.transform.name;
			string shortName;
			
			//If the object has "duplicate" text added to object name
			if( (longName.IndexOf(' ') + 1) != 0 )
			{
				shortName = longName.Substring(0, longName.IndexOf(' '));
			}
			else{
				shortName = longName;
			}

            //Get the objects status. THIS WILL BE ADDED ONTO LATER.
            Debug.Log("STATUSES HAVE NOT BEEN IMPLEMENTED TO GRAB STATUS FROM OBJECT. STATUSES DOES WORK ON OBJECTS THOUGH.");
            int status = 0;

            //Awful way to get status
            /*
            if(shortName.CompareTo("GravitySwitch") == 0)
            {
                gravityWheelControl getStatus = thisObject.gameObject.GetComponent(gravityWheelControl);
                thisObject.GetComponent(SpriteRenderer).name;
            }
            */

            //Get the objects rotation.
            float rotation = thisObject.transform.rotation.z;

            //Compile it all to a single string. OBSELETE BUT WILL BE USED FOR DEBUGGING FOR NOW.
            //string result = "SL:" + shortName + ":" + thisObject.transform.position.x + ":" + thisObject.transform.position.y;
            //Debug.Log(result);

            //Add object to list. DOES NOT USE PROPER GRIDS YET.
            jsonMessage.AddObject(shortName, (int)thisObject.transform.position.x, (int)thisObject.transform.position.y, rotation, 1);
		}


		/*
         * SEND END LEVEL OBJECT
        */

        GameObject endObject = GameObject.FindGameObjectWithTag("Exit");

        //Gets Objects proper name without all the duplicate issues
        string endlongName = endObject.transform.name;
        string endshortName;

        //If the object has "duplicate" text added to object name
        if ((endlongName.IndexOf(' ') + 1) != 0)
        {
            endshortName = endlongName.Substring(0, endlongName.IndexOf(' '));
        }
        else
        {
            endshortName = endlongName;
        }

        //Get the objects status. THIS WILL BE ADDED ONTO LATER.
        int endstatus = 0;

        //Get the objects rotation. THIS WILL BE ADDED ONTO LATER.
        float endrotation = endObject.transform.rotation.z;

        //Compile it all to a single string
        //string endresult = "SL:" + endshortName + ":" + endObject.transform.position.x + ":" + endObject.transform.position.y;
        //Debug.Log(endresult);

        //Add object to list
        jsonMessage.AddObject(endshortName, (int)endObject.transform.position.x, (int)endObject.transform.position.y, endrotation, 3);
        

        /*
         * SEND ARRAY
        */

        /*
        //Serialize array
        string message = jsonMessage.serializeArray();
        Debug.Log("Serialized output: " + message);

        //Sends jsonMessage to client
        
        Debug.Log("Sending Array of Messages");
        input.text = message;
        SendReadyToBeginMessage(0);
        input.text = "";
        */

        //Save level to file
        string message = jsonMessage.serializeArray();
        saveLevel(message);

		
	}

    private void saveLevel(string level)
    {
        //Setup Save File Writer
        BinaryFormatter bf = new BinaryFormatter();
        //FileStream file = File.Create(Application.persistentDataPath + "/serverLogin.dat");
        FileStream file = File.Create("ServerData/Levels" +"/testLevel.dat");

        //Serialize data and save, then closes file
        bf.Serialize(file, level);
        file.Close();
    }

    public void saveLevelList()
    {
        //levelList list = new levelList();
        int count = 0;

        //Search for all files within the Levels folder, except levelList.dat
        DirectoryInfo dir = new DirectoryInfo("ServerData/Levels");
        FileInfo[] info = dir.GetFiles("*.dat");
        foreach (FileInfo f in info) 
        {
            //Remove .dat from name
            string name = f.Name.Substring(0, f.Name.IndexOf('.'));
            Debug.Log(name);

            //Check to make sure it isn't levelist.dat
            if( name.CompareTo("levelList") != 0 )
            {
                //Add to the list
                Debug.Log("Adding " + name + " to Level List");
               // list.levelName.Add(name);
               // list.levelBestTime.Add("Not Implemented");
                //list.levelRating.Add(5);

                //Up the count
                count++;
            }
        }

        //Save to file
        BinaryFormatter bf = new BinaryFormatter();
        //FileStream file = File.Create(Application.persistentDataPath + "/serverLogin.dat");
        FileStream file = File.Create("ServerData/Levels" + "/levelList.dat");

        //Serialize data and save, then closes file
        ///bf.Serialize(file, list);
        file.Close();
    }
}

public class arrayBuild : LgBaseClass
{
    Level messageArrayClass = LgJsonNode.Create<Level>();

    private static LevelObject CreateLevelObject()
    {
        LevelObject levelObject = LgJsonNode.Create<LevelObject>();


        levelObject.id = "PrefabName";
        levelObject.row = 8;
        levelObject.column = 10;
        levelObject.rotation = 90;
        //levelObject.status = 0;

        return levelObject;
    }

    public static void SimpleArrayTest()
    {
        //Create the array of objects
        Level simpleArrayClass = LgJsonNode.Create<Level>();

        //Add objects to the array
        for (int i = 0; i < 5; i++)
        {
            // Method #1 - Directly add the type to array
            LevelObject levelObject = CreateLevelObject();
            simpleArrayClass.LevelObjectArray.Add(levelObject);
        }
        for (int i = 0; i < 5; i++)
        {
            // Method #2 - Use the array to add an entry and THEN fill it out
            LevelObject levelObject = simpleArrayClass.LevelObjectArray.AddNew();
            levelObject.id = "PrefabName";
            levelObject.row = i;
            levelObject.column = i * 2;
            //levelObject.status = 0;
            // No need to 'save' it, we are writing directly to it
        }

        // Serialize the array (SERVER USE FOR NOW)
        string serialized = simpleArrayClass.Serialize();

        // Deserialize the array (CLIENT USE FOR NOW)
        Level simpleArrayClassDeserialized = LgJsonNode.CreateFromJsonString<Level>(serialized);

        //Output the array to console to verify
        Debug.Log("Serialized output: " + serialized);
        Debug.Log("Deserialized output: " + simpleArrayClassDeserialized.Serialize());
    }

    //My code to compile level into the array
    public void AddObject(string prefabName, float row, float column, float rotation, int scene)
    {
        //Debug.Log("Adding Object to Array");
        LevelObject levelObject = messageArrayClass.LevelObjectArray.AddNew();
        levelObject.id = prefabName;
        levelObject.row = row;
        levelObject.column = column;
        levelObject.rotation = rotation;
        levelObject.sceneNumber = scene;
    }

    public string serializeArray()
    {
        // Serialize the array (SERVER USE FOR NOW)
        string serialized = messageArrayClass.Serialize();

        //Output the array to console to verify
        //Debug.Log("Serialized output: " + serialized);

        return serialized;
    }
}