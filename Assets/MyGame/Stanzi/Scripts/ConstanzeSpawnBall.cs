using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConstanzeSpawnBall : MonoBehaviour
{
    public GameObject obj;
    int randNum;
    public Transform spawnDest1; //spawnDest2, spawnDest3, spawnDest4;
   
    public Vector3 Pos1; //Pos2, Pos3, Pos4;

    private bool canSpawn = false;
    private bool hasSpawned = false;
    public float initialDelay = 30.0f;
    public TMP_Text text;
    private string count;
  

    private int counter = 0;
    private void Start()
    {
        StartCoroutine(EnableBallSpawnAfterDelay()); 
    }

    private void Update()
    {
        /*if (counter == 10)
        {
            Debug.Log("es geht");
            Spawnen();
            if (Input.GetKeyDown(KeyCode.Space) == true)
            {
                counter = 0;
            }
        }*/

        if (canSpawn && !hasSpawned)
        {
            SpawnNewBall();
            hasSpawned = true;
           
        }

        //Input.GetKeyDown(KeyCode.Space) && 

        count = initialDelay.ToString();
        text.text = count;
        
}


    /*IEnumerator Timer()
    {
        RealTimeCount();
        yield return new WaitForSeconds(1);  
    }*/

    private IEnumerator EnableBallSpawnAfterDelay()
    {
        // Warte initialDelay Sekunden, bevor das Spawnen erlaubt wird
        yield return new WaitForSeconds(initialDelay);
        
        canSpawn = true;
    }

    void Spawnen()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnNewBall();

        }
    }

  

    void SpawnNewBall()
    {
        
        /*randNum = Random.Range(0, 4);
        if (randNum == 0)
        {
            
            Instantiate(obj, spawnDest1.position, spawnDest1.rotation);
        }
        if (randNum == 1)
        {
            Instantiate(obj, spawnDest2.position, spawnDest2.rotation);
        }
        if (randNum == 2)
        {
            Instantiate(obj, spawnDest3.position, spawnDest3.rotation);
        }
        if (randNum == 3)
        {
            Instantiate(obj, spawnDest4.position, spawnDest4.rotation);
        }*/
        Instantiate(obj, spawnDest1.position, spawnDest1.rotation);
    }
}

