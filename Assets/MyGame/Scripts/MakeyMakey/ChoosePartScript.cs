using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoosePartScript : MonoBehaviour
{
    public GameObject buildingPart1;
    public GameObject buildingPart2;
    public GameObject buildingPart3;
    public GameObject buildingPart4;
    public ChangeSpeedScript specialAbilitySpeed;

    int currentPart = 0;
    int currentSpecialAbility = 0;
    string currentPartTag = "buildingPart0";

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && currentPart != 1)
        {
            Instantiate(buildingPart1);
            currentPart = 1;
            DestroyArray();
            currentPartTag = "buildingPart1";
            currentSpecialAbility = 0;
            Debug.Log("changed object to " + currentPartTag);
        }

        if (Input.GetKey(KeyCode.RightArrow) && currentPart != 2)
        {
            Instantiate(buildingPart2);
            currentPart = 2;
            DestroyArray(); ;
            currentPartTag = "buildingPart2";
            currentSpecialAbility = 0;
            Debug.Log("changed object to " + currentPartTag);
        }

        if (Input.GetKey(KeyCode.UpArrow) && currentPart != 3)
        {
            Instantiate(buildingPart3);
            currentPart = 3;
            DestroyArray();
            currentPartTag = "buildingPart3";
            currentSpecialAbility = 0;
            Debug.Log("changed object to " + currentPartTag);
        }

        if (Input.GetKey(KeyCode.DownArrow) && currentPart != 4)
        {
            Instantiate(buildingPart4);
            currentPart = 4;
            DestroyArray();
            currentPartTag = "buildingPart4";
            currentSpecialAbility = 0;
            Debug.Log("changed object to " + currentPartTag);
        }

        if (currentPartTag != "buildingPart0" && Input.GetMouseButtonDown(0) && currentSpecialAbility > -1)
        {
            currentSpecialAbility = currentSpecialAbility - 1; 
            Debug.Log("made ability slower");
            ToggleSpecialAbility(0.5f);
            
        }

        if (currentPartTag != "buildingPart0" && Input.GetKeyDown(KeyCode.Space) && currentSpecialAbility < 1)
        {
            currentSpecialAbility = currentSpecialAbility + 1;
            Debug.Log("made ability faster");
            ToggleSpecialAbility(2f);
            
        }
    }

    void ToggleSpecialAbility(float current)
    {
        GameObject currentGameObject;
        currentGameObject = GameObject.FindGameObjectWithTag(currentPartTag);
        specialAbilitySpeed = currentGameObject.GetComponent<ChangeSpeedScript>();

        if ((0.5 <= currentGameObject.GetComponent<ChangeSpeedScript>().speedChange) && (2 >= currentGameObject.GetComponent<ChangeSpeedScript>().speedChange))
        {
            currentGameObject.GetComponent<ChangeSpeedScript>().speedChange = currentGameObject.GetComponent<ChangeSpeedScript>().speedChange * current;
        }
        Debug.Log("new speed multiplier equals " + currentGameObject.GetComponent<ChangeSpeedScript>().speedChange);
    }

    void DestroyArray()
    {
        if (currentPartTag != "buildingPart0")
        {            
            Destroy(GameObject.FindGameObjectWithTag(currentPartTag));
            currentPartTag = "buildingPart0";
        }
    }
}
