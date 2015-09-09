using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class PlayerController : NetworkBehaviour {

	public float speed;

	private float startTime;
	private Vector3 startPos;
		
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
		if (!isLocalPlayer)
			return;
		
		bool isFalling = rb.velocity.y < 0;
		if (isFalling)
			return;

		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		if (Input.touchCount == 1) {
			startTime = Time.time;
			startPos = Input.mousePosition;
			startPos.z = transform.position.z - Camera.main.transform.position.z;
			startPos = Camera.main.ScreenToWorldPoint(startPos);
		}
		
		Vector3 force = new Vector3 (moveHorizontal,0.0f,moveVertical);

		if (isServer)
			RpcAddClientForce (force);
		else 
			CmdAddForce (force);
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
		}
	}

	void AssignRandColor () 
	{

		rend.material.color = new Color(Random.Range (0.0f,1.0f), Random.Range (0.0f,1.0f), Random.Range (0.0f,1.0f));
	}

	void OnMouseDown() {
		startTime = Time.time;
		startPos = Input.mousePosition;
		startPos.z = transform.position.z - Camera.main.transform.position.z;
		startPos = Camera.main.ScreenToWorldPoint(startPos);
		Debug.Log ("hej");
	}
	
	void OnMouseUp() {
		var endPos = Input.mousePosition;
		endPos.z = transform.position.z - Camera.main.transform.position.z;
		endPos = Camera.main.ScreenToWorldPoint(endPos);
		
		var force = endPos - startPos;
		force.z = force.magnitude;
		force /= (Time.time - startTime);
		
		//rigidbody.AddForce(force * factor);
	}
}
