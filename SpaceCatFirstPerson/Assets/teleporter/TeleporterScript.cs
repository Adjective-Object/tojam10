using UnityEngine;
using System.Collections;

public class TeleporterScript : MonoBehaviour {

    public GameObject Arrow;

    private bool _isActive;

	// Use this for initialization
	void Start () {
        setActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setActive(bool active)
    {
        _isActive = active;
        if (_isActive)
        {
            //Show arrow
            Arrow.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            Arrow.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (_isActive)
        {
            if (other.tag == "Player")
            {
                Debug.Log("Entered Teleporter");

                MapMaker mapmaker = (MapMaker)FindObjectsOfType<MapMaker>()[0];
                mapmaker.destroyMap();
            }
        }
    }
}
