using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//LOAD THIS FIRST IN THE LEVEL
public class DevPreload : MonoBehaviour
{
    void Awake()
    {
        GameObject check = GameObject.Find("__app");
        if (check == null)
        { UnityEngine.SceneManagement.SceneManager.LoadScene("preload"); }
    }
}
