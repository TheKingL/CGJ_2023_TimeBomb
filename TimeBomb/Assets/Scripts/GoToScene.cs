using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GoToScene : MonoBehaviour
{
    public int buildIndex; 

    public void LoadScene()
    {
        SceneManager.LoadScene(buildIndex);
    }

}
