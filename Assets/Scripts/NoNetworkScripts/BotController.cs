using UnityEngine;
using System.Collections;

public class BotController : MonoBehaviour {

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
		
		bool isFalling = rb.velocity.y < 0;
		if (isFalling)
			return;
	}
	
	void CheckForReset()
	{
		if (transform.position.y < -20f) {
			rb.velocity = new Vector3(0f,0f,0f);
			rb.angularVelocity = new Vector3(0f,0f,0f);
			transform.position = new Vector3 	(0f, 3f, 0f);
		}
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
			
		}
	}
	
	void AssignRandColor () 
	{
		
		rend.material.color = new Color(Random.Range (0.0f,1.0f), Random.Range (0.0f,1.0f), Random.Range (0.0f,1.0f));
	}
}

