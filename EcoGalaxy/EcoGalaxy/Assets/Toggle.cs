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


    // Use this for initialization
    void Start()
    {
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
            image.enabled = !image.enabled;
        }
    }
}
