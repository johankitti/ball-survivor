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
			transform.position = new Vector3 	(9f, 3f, 0f);
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
		if (col.gameObject.name == "Sphere Player") 
		{
			/*float xDist = rb.position.x - col.rigidbody.position.x;
			float zDist = rb.position.z - col.rigidbody.position.z;
			float collisionAngle = Mathf.Atan2(zDist, xDist);
			
			float angle1 = Mathf.Atan2(rb.velocity.x, rb.velocity.y);
			float angle2 = Mathf.Atan2(col.rigidbody.velocity.x, col.rigidbody.velocity.z);
			
			Vector3 velocity1 = new Vector3(
				rb.velocity.magnitude * Mathf.Cos (angle1 - collisionAngle),
				0.0f,
				rb.velocity.magnitude * Mathf.Sin (angle1 - collisionAngle)
				);
			Vector3 velocity2 = new Vector3(
				col.rigidbody.velocity.magnitude * Mathf.Cos (angle2 - collisionAngle),
				0.0f,
				col.rigidbody.velocity.magnitude * Mathf.Sin (angle2 - collisionAngle)
				);
			
			Vector3 finalVelocity1 = new Vector3(
				((rb.mass - col.rigidbody.mass) * velocity1.x + (2*col.rigidbody.mass) * velocity2.x) / (rb.mass + col.rigidbody.mass),
				0.0f,
				velocity1.z
				);
			
			Vector3 finalVelocity2 = new Vector3(
				((2 * rb.mass) * velocity1.x + (rb.mass - col.rigidbody.mass) * velocity2.x) / (rb.mass + col.rigidbody.mass),
				0.0f,
				velocity2.z
				);
			
			rb.velocity = new Vector3 (
				Mathf.Cos (collisionAngle) * finalVelocity1.x + Mathf.Cos (collisionAngle + Mathf.PI/2) * finalVelocity1.z,
				0.0f,
				Mathf.Sin (collisionAngle) * finalVelocity1.x + Mathf.Sin (collisionAngle + Mathf.PI/2) * finalVelocity1.z
				);
			col.rigidbody.velocity = new Vector3 (
				Mathf.Cos (collisionAngle) * finalVelocity2.x + Mathf.Cos (collisionAngle + Mathf.PI/2) * finalVelocity2.z,
				0.0f,
				Mathf.Sin (collisionAngle) * finalVelocity2.x + Mathf.Sin (collisionAngle + Mathf.PI/2) * finalVelocity2.z
			);*/
		}
	}
	
	void AssignRandColor () 
	{
		
		rend.material.color = new Color(Random.Range (0.0f,1.0f), Random.Range (0.0f,1.0f), Random.Range (0.0f,1.0f));
	}
}

