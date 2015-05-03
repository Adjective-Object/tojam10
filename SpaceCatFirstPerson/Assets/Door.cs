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

    static float doorSpeed = 5f;
    static float doorTime = 1/doorSpeed;

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
            if (timer >doorTime)
            {
                isOpen = true;
                isOpening = false;
                timer = 0;
                doorCollider.enabled = false;
                doorModel.transform.position = new Vector3(doorModel.transform.position.x, 1, doorModel.transform.position.z);
            }
            else
            {
                doorModel.transform.Translate(0, Time.deltaTime * doorSpeed, 0);
            }
        }
        else if (isOpen)
        {
            timer += Time.deltaTime;
            if (timer > doorTime)
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
            if (timer > doorTime)
            {
                isClosing = false;
                doorModel.transform.position = new Vector3(doorModel.transform.position.x, 0, doorModel.transform.position.z);
            }
            else
            {
                doorModel.transform.Translate(0, -Time.deltaTime * doorSpeed, 0);
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
                isOpening = true;
                timer = 0;
            }
        }
    }
}
