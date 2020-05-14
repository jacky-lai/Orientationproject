using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartMenu : MonoBehaviour
{
    public void PlayGame()
    {
        //int index = Random.Range(1, 4);
        int index = 1;
        SceneManager.LoadScene(index);
    }
}
