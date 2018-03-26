using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

    Rigidbody rb;

	public void ThrowBall()
    {
        rb.AddForce(Vector3.forward * 2, ForceMode.Impulse);
    }
}
