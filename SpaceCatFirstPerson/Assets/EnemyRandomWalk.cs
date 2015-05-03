using UnityEngine;
using System.Collections;

public class EnemyRandomWalk : MonoBehaviour {

	static GameObject player;
	public float aggroRange = 10;
	public float randomWalkAngleDrift = 15;
	public float randomWalkMinDistance = 5;
	public float randomWalkMaxDistance = 10;
	public float speed = 0.5f;
	
	private Animator animator;
	private LivingEntity livingEntity;
	private CharacterController controller;
	private Vector3 destination;
	private float closeEnough = 1;
	private float giveUpThreshold = 0.01f;
	private Vector3 lastPos = Vector3.zero;

	public enum STATE {IDLE, ATTACK, MOVE};
	private STATE state;

	// Use this for initialization
	void Start () {
		if (player == null) {
			player = GameObject.Find("Player");
		}
		animator = this.GetComponent<Animator>();
		livingEntity = this.GetComponent<LivingEntity>();
		controller = this.GetComponent<CharacterController>();
	} 
	
	// Update is called once per frame
	void Update () {
		
		if(!livingEntity.alive) {
			Destroy(controller);
			Destroy(this);
			return;
		}
		
		if (this.state == STATE.IDLE) {
			// randomly move with some probability
			if (Random.Range(0f, 1f) < Time.deltaTime ) {
				this.state = STATE.MOVE;
				this.animator.SetBool("move", true);
				this.destination = 
					this.transform.position +
						Quaternion.AngleAxis(
							this.transform.rotation.eulerAngles.y + 
								Random.Range(-randomWalkAngleDrift, randomWalkAngleDrift),
							Vector3.up) *
						Vector3.forward *
						Random.Range(randomWalkMinDistance, randomWalkMaxDistance);
				//Debug.Log("walk to "+destination);
			}
			
		}
		
		else if(this.state == STATE.MOVE) {
			 if ((this.destination - this.transform.position).magnitude < closeEnough) {
				// if close to destination, switch to IDLE
				this.animator.SetBool("move", false);
				this.state = STATE.IDLE;
			 }
			 else {
				 // lerp towards destination if not close enough
				lastPos = this.transform.position;		
				this.controller.Move( 
					 	(this.destination - this.transform.position) * this.speed * Time.deltaTime);
				// give up if stuck on geometry
				if ((this.lastPos - this.transform.position).magnitude < this.giveUpThreshold) {
					this.state = STATE.IDLE;
				}
			 }
		}

	}
}
