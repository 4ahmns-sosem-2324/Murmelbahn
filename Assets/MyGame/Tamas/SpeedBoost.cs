using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    public GameManager gM;
    Vector3 normalizedVelocity;
    public int speedForce;

    private void Update()
    {
        normalizedVelocity = gM.sceneBall.GetComponent<Rigidbody>().velocity.normalized;
        //Debug.Log(normalizedVelocity);
    }

    private void OnTriggerStay(Collider other)
    {
        gM.sceneBall.GetComponent<Rigidbody>().AddForce(normalizedVelocity * Time.deltaTime * speedForce);
        Debug.Log("BOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOST");
    }
}
