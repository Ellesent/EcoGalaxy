using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Button : MonoBehaviour {

    public Canvas can;
    public static string playerName;
    public UnityEngine.UI.Toggle toggle;
    public UnityEngine.UI.Toggle toggle1;
    public UnityEngine.UI.Toggle toggle2;
    public Text conqueredText;
    public dreamloLeaderBoard pc;
    public Camera main;
    public Camera second;
    public Image pick;
    public Image tach;
    public Image eng;
    public Image arch;
    public Text tArt;
    public Text Title;

    public Text message;

    bool isTwoPlayer;
   

	// Use this for initialization
	void Start () {
        isTwoPlayer = true;
        //second.enabled = false;
        if (Application.loadedLevel == 2)
        {
            conqueredText.text += MoneyManager.conquered;
            pc.AddScore(playerName, MoneyManager.conquered);

        }
	
	}
	
	// Update is called once per frame
	void Update () {
       //ebug.Log(playerName);
        
	
	}

    public void OnClickLoad(int level)
    {
        Application.LoadLevel(level);
    }



    public void OnObjectClick(GameObject objSpawn)
    {
        if (objSpawn.tag == "Food")
        {
           if (MoneyManager.Oxygen == false)
           {
               Debug.Log("no");
               message.text = "This object needs oxygen first";
           }
           else
           {
               if ((objSpawn.GetComponent<AllObjects>() != null && MoneyManager.money >= objSpawn.GetComponent<AllObjects>().howMuch) || (objSpawn.GetComponent<AllObjectsWater>() != null && MoneyManager.money >= objSpawn.GetComponent<AllObjectsWater>().howMuch))
               {
                   Vector3 mousePos = Input.mousePosition;
                   mousePos.z = 2.0f;
                   Vector3 objectPos = Camera.main.ScreenToWorldPoint(mousePos);
                   GameObject blah = Instantiate(objSpawn, objectPos, Quaternion.identity) as GameObject;
                   //blah.transform.localScale = new Vector3(0.06f, 0.06f, 1); 



                   toggle.isOn = true;
                   toggle1.isOn = true;
                   toggle2.isOn = true;
               }

               else
               {
                   message.text = "Not enough money";
               }
           }
        }

        else if (objSpawn.tag == "WaterCollect")
        {
            if (MoneyManager.power < Mathf.Abs(objSpawn.GetComponent<AllObjectsWater>().pow))
            {
                Debug.Log("no power");
                message.text = "Not enough power";
            }
            else if ((objSpawn.GetComponent<AllObjects>() != null && MoneyManager.money >= objSpawn.GetComponent<AllObjects>().howMuch) || (objSpawn.GetComponent<AllObjectsWater>() != null && MoneyManager.money >= objSpawn.GetComponent<AllObjectsWater>().howMuch))
            {
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = 2.0f;
                Vector3 objectPos = Camera.main.ScreenToWorldPoint(mousePos);
                GameObject blah = Instantiate(objSpawn, objectPos, Quaternion.identity) as GameObject;
                //blah.transform.localScale = new Vector3(0.06f, 0.06f, 1); 



                toggle.isOn = true;
                toggle1.isOn = true;
                toggle2.isOn = true;
            }
            else
            {
                message.text = "Not enough money";
            }
        }
        
        else if ((objSpawn.GetComponent<AllObjects>() != null && MoneyManager.money >= objSpawn.GetComponent<AllObjects>().howMuch) || (objSpawn.GetComponent<AllObjectsWater>() != null && MoneyManager.money >= objSpawn.GetComponent<AllObjectsWater>().howMuch))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 2.0f;
            Vector3 objectPos = Camera.main.ScreenToWorldPoint(mousePos);
            GameObject blah = Instantiate(objSpawn, objectPos, Quaternion.identity) as GameObject;
            //blah.transform.localScale = new Vector3(0.06f, 0.06f, 1); 
            


            toggle.isOn = true;
            toggle1.isOn = true;
            toggle2.isOn = true;
        }
        else
        {
            message.text = "Not enough money";
        }
        
    }

    public void ValChanged(InputField field)
    {
        if (field.text.Length > 0)
        {
        playerName = field.text;
        Application.LoadLevel(6);
        }
        else{
            field.placeholder.GetComponent<Text>().text = "Please enter a name";
            field.ActivateInputField();
            field.Select();
            
        }
    }

    public void ResourceButtonGO()
    {
        if (isTwoPlayer == true)
        {
            second.enabled = true;
            //camera.rect = new Rect(0.5f, 0, 0.5f, 0);
            //Rect r = camera.GetComponent<Rect>();
            //r.x = 0.5f;
            //r.width = 0.5f;
            main.rect = new Rect(0, 0, 0.5f, 1);
            pick.enabled = true;
            tach.enabled = true;
            eng.enabled = true;
            arch.enabled = true;
            tArt.enabled = true;
            Title.enabled = true;
        }
    }

    public void TwoPChars(GameObject obj)
    {
        GameObject play = Instantiate(obj, new Vector2(-1.3f,12), Quaternion.identity) as GameObject;
        second.transform.SetParent(play.transform);
        pick.enabled = false;
        tach.enabled = false;
        eng.enabled = false;
        arch.enabled = false;
        tArt.enabled = false;
        Title.enabled = false;
        isTwoPlayer = false;

    }

    public void GoBack()
    {
        isTwoPlayer = true;
        second.enabled = false;
        main.rect = new Rect(0, 0, 1, 1);
    }

   

    
}
