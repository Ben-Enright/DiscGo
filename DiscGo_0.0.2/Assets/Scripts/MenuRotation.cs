using UnityEngine;
using System.Collections;

public class MenuRotation : MonoBehaviour {

	private float rotationSpeed;
	private Rigidbody rb;

	void Start () {
		rb = GetComponent<Rigidbody>();
		rotationSpeed = 3;
		Rotator();
	}

	void Rotator ()
	{
			rb.angularVelocity = new Vector3(0, 0, rotationSpeed);
	}
}

