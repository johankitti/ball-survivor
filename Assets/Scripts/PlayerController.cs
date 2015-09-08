using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class PlayerController : NetworkBehaviour {

	public float speed;
		
	private Vector3 lastPos;
	private Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		lastPos = new Vector3(0f,0.5f,0f);
	}

	void FixedUpdate()
	{
		if (!isLocalPlayer)
			return;
		Debug.Log (Mathf.Abs(transform.position.y - lastPos.y));
		if (Mathf.Abs(transform.position.y - lastPos.y) > 0.1f)
			return;

		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal,0.0f,moveVertical);

		rb.AddForce (movement * speed);
		lastPos = transform.position;
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("Pick Up"))
		{
			other.gameObject.SetActive(false);
		}
	}
}
