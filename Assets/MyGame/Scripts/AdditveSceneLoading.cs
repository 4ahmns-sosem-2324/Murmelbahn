using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class AdditveSceneLoading : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("ConstanzeScene", LoadSceneMode.Additive);
        SceneManager.LoadScene("NiclasScene", LoadSceneMode.Additive);
        SceneManager.LoadScene("TamasScene", LoadSceneMode.Additive);
        SceneManager.LoadScene("snappen", LoadSceneMode.Additive);
        SceneManager.LoadScene("TobiasScene", LoadSceneMode.Additive);
        SceneManager.LoadScene("InaScene", LoadSceneMode.Additive);
    }
}
