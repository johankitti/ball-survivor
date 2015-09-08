using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class PlayerController : NetworkBehaviour {

	public float speed;
	public Text countText;

	private Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		countText.text = "Hej";
	}

	void FixedUpdate()
	{
		if (!isLocalPlayer)
			return;
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal,0.0f,moveVertical);

		rb.AddForce (movement * speed);
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("Pick Up"))
		{
			other.gameObject.SetActive(false);
		}
	}
}
