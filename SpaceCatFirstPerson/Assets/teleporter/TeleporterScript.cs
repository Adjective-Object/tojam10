using UnityEngine;
using System.Collections;

public class TeleporterScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Entered Teleporter");

            MapMaker mapmaker = (MapMaker)FindObjectsOfType<MapMaker>()[0];
            mapmaker.destroyMap();
        }
    }
}
