using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController1 : MonoBehaviour {

	public float speed;
	public Text countText;
	
	private Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		countText.text = "på dig";

	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal2");
		float moveVertical = Input.GetAxis ("Vertical2");

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