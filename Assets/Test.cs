using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test : MonoBehaviour
{

    public InputActionAsset inputActions;

    private void Awake()
    {
        inputActions.FindActionMap("Debug").FindAction("Obj2").performed += Testing;
        inputActions.FindActionMap("Debug").actionTriggered += Testing;
    }

    void Testing(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("AAAAAAAAAA");
    }
}
