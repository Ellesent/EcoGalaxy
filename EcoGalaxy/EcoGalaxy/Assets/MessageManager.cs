using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using LgOctEngine.CoreClasses;
using UnityEngine.UI;


interface IJsonable
{
	void HandleNewObject();
}

// Unity-formatted Json message to send between client and server
// The use of templates means we can use this for all message types
public class JsonMessage<T> : MessageBase
{
	public string message;
}


public class LevelObject : LgJsonDictionary, IJsonable
{
	public string id { get { return GetValue<string>("id", ""); } set { SetValue<string>("id", value); } }       // The unique string identifier that corresponds to the Prefab to load
	public float row { get { return GetValue<float>("row", 0); } set { SetValue<float>("row", value); } }             // The row of the "grid" that the object occupies
	public float column { get { return GetValue<float>("column", 0); } set { SetValue<float>("column", value); } }      // The column of the "grid" that the object occupies
	public float rotation { get { return GetValue<float>("rotation", 0); } set { SetValue<float>("rotation", value); } }  // The rotation of the object, in degrees, in a clockwise manner.  A zero rotation would be "upright".
    public int sceneNumber { get { return GetValue<int>("sceneNumber", 0); } set { SetValue<int>("sceneNumber", value); } }

   
	
	public void HandleNewObject()
	{
		Debug.Log("Handling LevelObject");
		// TODO: put code that does something with this object
	}
}

// An entire level or collection of level objects
public class Level : LgJsonDictionary, IJsonable
{
	public string title { get { return GetValue<string>("title", ""); } set { SetValue<string>("title", value); } }

	public LgJsonArray<LevelObject> LevelObjectArray
	{
		get { return GetNode<LgJsonArray<LevelObject>>("Level"); }
		set { SetNode<LgJsonArray<LevelObject>>("Level", value); }
	}

	public void HandleNewObject()
	{
        GameObject stance; 
		Debug.Log("Handling Level");
        //Application.LoadLevel(3);
        //AsyncOperation async = new AsyncOperation();
        //if (Application.loadedLevel != 3)
        //{
        //  async = Application.LoadLevelAsync(3);
        //}
		// TODO: put code that does something with this object

        
        
            for (int i = 0; i < LevelObjectArray.Count; i++)
            {
                switch (LevelObjectArray[i].id)
                {
                    case "SolarPanel":
                       stance= GameObject.Instantiate(Resources.Load("SolarPanel"), new Vector3(LevelObjectArray[i].row, LevelObjectArray[i].column, 0), Quaternion.Euler(0, 0, LevelObjectArray[i].rotation)) as GameObject;
                       //stance.AddComponent<RealTimeCounter>();

                        //if (Physics2D.OverlapCircle(stance.transform.position, 1))
                        //{
                        //    Debug.Log("caught");
                        //    GameObject.Destroy(stance);
                        //}
                        break;
                    case "Oxygen":
                        GameObject.Instantiate(Resources.Load("OxygenShield"), new Vector3(LevelObjectArray[i].row, LevelObjectArray[i].column, 0), Quaternion.Euler(0, 0, LevelObjectArray[i].rotation));
                        break;
                    case "Food":
                        GameObject.Instantiate(Resources.Load("Food"), new Vector3(LevelObjectArray[i].row, LevelObjectArray[i].column, 0), Quaternion.Euler(0, 0, LevelObjectArray[i].rotation));
                        break;
                    case "DoorEnter":
                        GameObject.Instantiate(Resources.Load("DoorEnter"), new Vector3(LevelObjectArray[i].row, LevelObjectArray[i].column, 0), Quaternion.Euler(0, 0, LevelObjectArray[i].rotation));
                        break;
                    case "DoorExit":
                        GameObject.Instantiate(Resources.Load("DoorExit"), new Vector3(LevelObjectArray[i].row, LevelObjectArray[i].column, 0), Quaternion.Euler(0, 0, LevelObjectArray[i].rotation));
                        break;
                }
                //GameObject.Instantiate(LevelObjectArray[i].id, new Vector3(LevelObjectArray[i].row, LevelObjectArray[i].column), Quaternion.Euler(LevelObjectArray[i].rotation))
            }
            //GameControl.isLoaded = false;
        
	}
}

   

