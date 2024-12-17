using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField]
    private List<Transform> spawnPoints = new();
    [SerializeField]
    private GameObject pawnController;

    private List<GameObject> pawnControllers = new();
    int turnIndex = 0;

    void Start()
    {
        foreach (Transform t in spawnPoints)
        {
            GameObject item = Instantiate(pawnController);
            item.transform.position = t.position;
            item.transform.rotation = t.rotation;
            pawnControllers.Add(item);
        }
        
    }

    [ContextMenu("PlayRound")]
    void PlayRound()
    {
        var currentController = pawnControllers[turnIndex].GetComponent<PawnController>();
        PawnController.onTurnEnd += StartWaitAfterTurnEnd;
        currentController.PlayTurn();
        print("" + Time.frameCount + currentController);
    }



    void StartWaitAfterTurnEnd()
    {
        PawnController.onTurnEnd -= StartWaitAfterTurnEnd;
        StartCoroutine(WaitAfterTurnEnd());
    }

    IEnumerator WaitAfterTurnEnd()
    {
        yield return new WaitForSeconds(1);
        TurnEnd();
    }

    void TurnEnd()
    {

        turnIndex++;
        if (turnIndex >= pawnControllers.Count)
        {
            turnIndex = 0;
        }
        PlayRound();

    }

}
