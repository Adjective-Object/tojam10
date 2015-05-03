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
                GetComponent<Renderer>().material.SetTexture("_MainTex", FullPanelBackgrounds[rand.Next(0, FullPanelBackgrounds.Length)]);
                GetComponent<Renderer>().material.SetTexture("_PanelTex", FullPanels[rand.Next(0, FullPanels.Length)]);
                break;
            case 1:
                GetComponent<Renderer>().material.SetTexture("_MainTex", LeftPanelBackgrounds[rand.Next(0, LeftPanelBackgrounds.Length)]);
                GetComponent<Renderer>().material.SetTexture("_PanelTex", LeftPanels[rand.Next(0, LeftPanels.Length)]);
                break;
            case 2:
                GetComponent<Renderer>().material.SetTexture("_MainTex", RightPanelBackgrounds[rand.Next(0, RightPanelBackgrounds.Length)]);
                GetComponent<Renderer>().material.SetTexture("_PanelTex", RightPanels[rand.Next(0, RightPanels.Length)]);
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
