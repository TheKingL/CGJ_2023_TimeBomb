using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoint : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;

    public Transform GetSpawnPoint()
    {
        return spawnPoint;
    }

    

}
