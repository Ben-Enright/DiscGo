using UnityEngine;
using System.Collections;

public class PURotation : MonoBehaviour {

	private float rotationSpeed;
	private Rigidbody rb;

	void Start () {
		Destroy (gameObject, 50f);
		rb = GetComponent<Rigidbody>();
		rotationSpeed = 5.0f;
		Rotator();
	}

	void Rotator ()
	{
		rb.angularVelocity = new Vector3(0, rotationSpeed, 0);
	}
}
