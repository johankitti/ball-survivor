using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class PlayerController : NetworkBehaviour {

	public float speed;
		
	private Vector2 direction;

	private Rigidbody rb;
	private Renderer rend;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		rend = GetComponent<Renderer>();
		AssignRandColor ();
	}

	void FixedUpdate()
	{
		CheckForReset ();

		if (!isLocalPlayer)
			return;
		
		bool isFalling = rb.velocity.y < 0;
		if (isFalling)
			return;

		// KEYBOARD MOVEMENT
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		// TOUCH MOVEMENT
		if (Input.touches.Length > 0) 
		{
			if (Input.touches [0].phase == TouchPhase.Moved) {  // Check if Touch has moved.
				direction = Input.touches [0].deltaPosition.normalized;  // Unit Vector of change in position
				speed = Input.touches [0].deltaPosition.magnitude; // distance traveled divided by time elapsed
				Debug.Log (speed);
			} else {
				direction = new Vector2 (0f, 0f);
			}
		}

		print ("____------____");
		print (moveHorizontal);
		moveHorizontal += direction.x;
		print (moveHorizontal);
		moveVertical += direction.y;
	
		Vector3 horVec = Camera.main.transform.up * moveVertical;
		Vector3 verVec = Camera.main.transform.right * moveHorizontal;
		Vector3 force = new Vector3 (
			horVec.x + verVec.x,
			0.0f,
			horVec.z + verVec.z);

		if (isServer)
			RpcAddClientForce (force);
		else 
			CmdAddForce (force);
	}

	void CheckForReset()
	{
		if (transform.position.y < -20f) {
			rb.velocity = new Vector3(0f,0f,0f);
			rb.angularVelocity = new Vector3(0f,0f,0f);
			transform.position = new Vector3 	(0f, 3f, 0f);
		}
	}

	[Command]
	public void CmdAddForce(Vector3 force) 
	{
		rb.AddForce (force * speed);
	}

	[ClientRpc]
	public void RpcAddClientForce(Vector3 force) 
	{
		rb.AddForce (force * speed);
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("Pick Up"))
		{
			other.gameObject.SetActive(false);
			transform.localScale += new Vector3(0.5F, 0.5F, 0.5F);
			rb.mass = rb.mass * 1.05f;
			speed = speed * 1.05f;
		}
	}

	void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.name == "Player(Clone)") 
		{
			rb.AddForce (col.rigidbody.velocity * col.rigidbody.mass * 50);
			col.rigidbody.AddForce (rb.velocity * rb.mass * 50);
		}
	}
	
	void AssignRandColor () 
	{
		
		rend.material.color = new Color(Random.Range (0.0f,1.0f), Random.Range (0.0f,1.0f), Random.Range (0.0f,1.0f));
	}
}
