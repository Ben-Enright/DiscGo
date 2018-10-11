using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {

    private int yesNo;
    private float rotationSpeed;
    private Rigidbody rb;

	void Start () {
        Destroy (gameObject, 10f);
        rb = GetComponent<Rigidbody>();
        yesNo = Random.Range(1, 2);
        rotationSpeed = Random.Range(-3, 3);
        Rotator();
    }
	
    void Rotator ()
    {
        if (yesNo == 1)
        {
            rb.angularVelocity = new Vector3(0, 0, rotationSpeed);
        }
    }
}
