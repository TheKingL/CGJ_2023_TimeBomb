using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideShowObject : MonoBehaviour
{
    public GameObject objectToHide;

    public void hide()
    {
        objectToHide.SetActive(false);
    }
    
    public void show()
    {
        objectToHide.SetActive(true);
    }
}
