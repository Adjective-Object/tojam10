using UnityEngine;
using System.Collections;

public class PanelScript : MonoBehaviour
{
    public Texture[] FullPanelBackgrounds;
    public Texture[] FullPanels;

    public Texture[] LeftPanelBackgrounds;
    public Texture[] LeftPanels;

    public Texture[] RightPanelBackgrounds;
    public Texture[] RightPanels;


    System.Random rand;
    // Use this for initialization
    void Start()
    {
        rand = new System.Random();
        Debug.Log("Gonna make some panels");
        switch (rand.Next(0, 3))
        {
            case 0: //Full panel
                GetComponent<Renderer>().material.SetTexture("_MainTex", FullPanelBackgrounds[0]);
                GetComponent<Renderer>().material.SetTexture("_PanelTex", FullPanels[0]);
                break;
            case 1:
                GetComponent<Renderer>().material.SetTexture("_MainTex", LeftPanelBackgrounds[0]);
                GetComponent<Renderer>().material.SetTexture("_PanelTex", LeftPanels[0]);
                break;
            case 2:
                GetComponent<Renderer>().material.SetTexture("_MainTex", RightPanelBackgrounds[0]);
                GetComponent<Renderer>().material.SetTexture("_PanelTex", RightPanels[0]);
                break;
            default:
                break;
        }
    }

    private float getColorOffset()
    {
        return (float)((rand.NextDouble() - 0.5) * 0.15);
    }

    // Update is called once per frame
    void Update()
    {

        GetComponent<Renderer>().material.SetColor("_PanelColor", new Color(0.9f + getColorOffset(), 0.70f + getColorOffset(), 0.2f + getColorOffset()));
    }
}
