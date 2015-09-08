using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class PlayerController : NetworkBehaviour {

	public float speed;
		
	private Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
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
}
