using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField]
    private List<Transform> spawnPoints = new();
    [SerializeField]
    private GameObject pawnController;

    void Start()
    {
        foreach (Transform t in spawnPoints)
        {
            GameObject item = Instantiate(pawnController);
            item.transform.position = t.position;
            item.transform.rotation = t.rotation;
        }
        
    }

    void Update()
    {
        
    }
}
