using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSelfDestroy : MonoBehaviour
{
    private Vector3 lastPosition;
    private bool isChecking = false;

    void Start()
    {
        lastPosition = transform.position;
        StartCoroutine(MovementChecker());
    }

    void Update()
    {
        // Update last position only if ball is moving
        if (transform.position != lastPosition)
        {
            lastPosition = transform.position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

    IEnumerator MovementChecker()
    {
        while (true)
        {
            Vector3 positionToCheck = transform.position;
            yield return new WaitForSeconds(0.5f);
            if (transform.position == positionToCheck)
            {
                Destroy(gameObject);
                yield break; // Exit coroutine if object is destroyed
            }
        }
    }
}