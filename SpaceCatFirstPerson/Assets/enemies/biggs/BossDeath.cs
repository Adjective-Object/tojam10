using UnityEngine;
using System.Collections;

public class BossDeath : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void enableTeleporter()
    {
        Debug.Log("Boss dead, enabling teleport.");
        FindObjectsOfType<TeleporterScript>()[0].setActive(true);
    }
}
