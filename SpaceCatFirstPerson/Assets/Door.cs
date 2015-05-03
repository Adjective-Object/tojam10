using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {


    public bool isOpening;
    public bool isOpen;
    public bool isClosing;

    public float timerStart;
    public float timer;

    public GameObject doorModel;
    public BoxCollider doorCollider;

	// Use this for initialization
	void Start () {
        isOpening = false;
        isOpen = false;
	}
	
	// Update is called once per frame
	void Update () {
        
        if (isOpening)
        {
            timer += Time.deltaTime;
            if (timer > 0.5f)
            {
                isOpen = true;
                isOpening = false;
                timer = 0;
                doorCollider.enabled = false;
            }
            else
            {
                doorModel.transform.Translate(0, Time.deltaTime * 2, 0);
            }
        }
        else if (isOpen)
        {
            timer += Time.deltaTime;
            if (timer > 0.5f)
            {
                isOpen = false;
                isClosing = true;
                timer = 0;
                doorCollider.enabled = true;
            }
        }
        else if (isClosing)
        {
            timer += Time.deltaTime;
            if (timer > 0.5f)
            {
                isClosing = false;
            }
            else
            {
                doorModel.transform.Translate(0, -Time.deltaTime * 2, 0);
            }
        }
	}

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (isOpen)
            {
                timer = 0;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isOpening && !isOpen && !isClosing)
        {
            if (other.tag == "Player")
            {
                Debug.Log("Entered muh door");
                isOpening = true;
                timer = 0;
            }
        }
    }
}
