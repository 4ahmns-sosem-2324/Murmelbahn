using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpeedScript : MonoBehaviour
{
    public float speedChange = 1f;
    Rigidbody marbleRigidbody;
    Vector3 currentVelocity;

    private void Start()
    {
        marbleRigidbody = GameObject.FindGameObjectWithTag("marble").GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        currentVelocity = marbleRigidbody.velocity;
    }

    private void OnTriggerStay(Collider other)
    {
        marbleRigidbody.AddForce(currentVelocity*speedChange, ForceMode.VelocityChange);
    }
}
