using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanging : MonoBehaviour
{

    public void sceneChange(string cena)
    {
        SceneManager.LoadScene(cena);
    }
}