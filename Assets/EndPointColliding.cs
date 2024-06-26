using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPointColliding : MonoBehaviour
{
    public Canvas CongratulationsScreen;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            CongratulationsScreen.enabled = true;
        }
    }
}
