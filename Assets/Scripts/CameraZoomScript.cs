using UnityEngine;
using System.Collections;

public class CameraZoomScript : MonoBehaviour {

	float zoomInMax = 20f;
	float zoomTick = 1f;

	void FixedUpdate()
	{
		if (Input.GetKeyDown ("i")) 
		{
			Zoom (-1f);
			Resize (-1);
		}

		if (Input.GetKeyDown ("o")) 
		{
			Zoom (1f);
			Resize (1f);
		}
	}

	void Zoom(float direction)
	{
		Vector3 newPosition = new Vector3 ();
		newPosition.x = transform.position.x;
		newPosition.z = transform.position.z;
		newPosition.y = transform.position.y + direction * zoomTick;

		if (newPosition.y > zoomInMax)
			transform.position = newPosition;
	}

	void Resize(float direction)
	{
		GameObject lawl = GameObject.Find ("Ground");

		Vector3 newSize = new Vector3 ();
		newSize.x = lawl.transform.localScale.x + zoomTick * direction;
		newSize.y = lawl.transform.localScale.y;
		newSize.z = lawl.transform.localScale.z + zoomTick * direction;

		lawl.transform.localScale = newSize;
	}
}
