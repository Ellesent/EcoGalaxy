using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Toggle : MonoBehaviour
{
    public Image oxShield;
    public Text oxAmount;
    public Text oxTitle;
    public Image solar;
    public Text solTitle;
    public Text solAmount;
    public Image im;
    public Image res;
    public Image wat;
    public Text watText;
    public Image food;
    public Text foodText;
    GameObject woo;

    public GameObject obj;



    public Image tasks;
    //public ScrollRect achieveScroll;
    public GameObject achievements;

    public Text message;


    // Use this for initialization
    void Start()
    {
        obj.transform.GetChild(0).gameObject.SetActive(false);
        obj.transform.GetChild(1).gameObject.SetActive(false);
        obj.transform.GetChild(2).gameObject.SetActive(false);
        obj.transform.GetChild(3).gameObject.SetActive(false);
   
        oxShield.enabled = false;
        oxAmount.enabled = false;
        im.enabled = false;
        oxTitle.enabled = false;
        solar.enabled = false;
        solTitle.enabled = false;
        solAmount.enabled = false;
        res.enabled = false;
        wat.enabled = false;
        watText.enabled = false;
        food.enabled = false;
        foodText.enabled = false;

        //achieveScroll.enabled = false;
        tasks.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onChange(Image image)
    {
        
        //if (image.name == "NecessaryObjects")
       // {
            oxAmount.enabled = !image.enabled;
            oxShield.enabled = !image.enabled;
            oxTitle.enabled = !image.enabled;
            solar.enabled = !image.enabled;
            solTitle.enabled = !image.enabled;
            solAmount.enabled = !image.enabled;
            image.enabled = !image.enabled;

            message.text = "";
        //}
       
        
        //CanvasRenderer[] blah = image.GetComponentsInChildren<CanvasRenderer>();
        //foreach (CanvasRenderer b in blah)
        //{
        //    if (b != image.rectTransform)
        //    {
                
        //    }
        //}
    }

    public void Test(Image image)
    {
        if (image.name == "ResourceObjects")
        {
           
            //res.enabled = true;
            wat.enabled = !image.enabled;
            watText.enabled = !image.enabled;
            food.enabled = !image.enabled;
            foodText.enabled = !image.enabled;
            image.enabled = !image.enabled;

            message.text = "";
           
        }
    }

    public void Achievements(UnityEngine.UI.Toggle toggle)
    {
        
        //achieveScroll.enabled = !achieveScroll.enabled;
        tasks.enabled = !tasks.enabled;
        //one.enabled = !one.enabled;
        //two.enabled = !two.enabled;
       // three.enabled = !three.enabled;
        //four.enabled = !four.enabled;

        if (!toggle.isOn)
        {
            obj.transform.GetChild(0).gameObject.SetActive(true);
            obj.transform.GetChild(1).gameObject.SetActive(true);
            obj.transform.GetChild(2).gameObject.SetActive(true);
            obj.transform.GetChild(3).gameObject.SetActive(true);
            woo = Instantiate(achievements);
            woo.transform.position += new Vector3(-7, 2);
        }
        else
        {
            obj.transform.GetChild(0).gameObject.SetActive(false);
            obj.transform.GetChild(1).gameObject.SetActive(false);
            obj.transform.GetChild(2).gameObject.SetActive(false);
            obj.transform.GetChild(3).gameObject.SetActive(false);
            Destroy(woo);
        }
        
       // woo.transform.SetParent(ViewPort.transform);


    }
}
