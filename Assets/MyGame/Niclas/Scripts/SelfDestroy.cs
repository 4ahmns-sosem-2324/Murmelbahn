using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{ 
    public float velCheck;
    private Vector3 currentPosition;
    private Vector3 positionToCheck;

    void Update()
    {
        currentPosition = transform.position;
        StartCoroutine(MovementChecker());
        
        
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
        positionToCheck = currentPosition;
        yield return new WaitForSeconds(2f);
        if (currentPosition == positionToCheck)
        {
            Destroy(gameObject);
        }
    }
}
