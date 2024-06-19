using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSnapper : MonoBehaviour
{
    private const float snapAngle = 45f;

    public Transform center;
    public Transform[] localExits;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //SnapObjectToNearestAngle();
            SnapToExit();
        }
    }

    private void SnapObjectToNearestAngle()
    {
        Vector3 currentRotation = transform.rotation.eulerAngles;
        float nearestAngle = Mathf.Round(currentRotation.y / snapAngle) * snapAngle;
        transform.rotation = Quaternion.Euler(currentRotation.x, nearestAngle, currentRotation.z);
    }

    public void SnapToExit()
    {
        //find the nearest exit to the center
        GameObject[] exitsTemp = GameObject.FindGameObjectsWithTag("Exit");
        List<GameObject> allExits = new List<GameObject>();
        for (int i = 0; i < exitsTemp.Length; i++)
        {
            if (CheckExits(exitsTemp[i]))
            {
                allExits.Add(exitsTemp[i]);
            }
        }

        Transform nearestExit = null;
        float nearestExitDist = -1;
        foreach (GameObject exit in allExits)
        {
            if (nearestExitDist > Vector3.Distance(center.position, exit.transform.position))
            {
                nearestExit = exit.transform;
                nearestExitDist = Vector3.Distance(center.position, exit.transform.position);
            }
        }

        //find which exit is closest to the nearest exit
        Transform nearestLocalExit = null;
        nearestExitDist = -1;
        foreach (Transform localExit in localExits)
        {
            if (nearestLocalExit != null)
            {
                if (nearestExitDist > Vector3.Distance(localExit.position, nearestExit.position))
                {
                    nearestLocalExit = localExit;
                }
            }
            else
            {
                nearestLocalExit = localExit;
            }
        }

        //move the object accordingly
        transform.position = transform.position + (nearestExit.position - nearestLocalExit.position);
    }

    private bool CheckExits(GameObject exitToCheck)
    {
        for (int i = 0; i < localExits.Length; i++)
        {
            if (exitToCheck == localExits[i])
            {
                return false;
            }
        }
        return true;
    }
}
