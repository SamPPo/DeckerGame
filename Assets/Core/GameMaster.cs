using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField]
    private List<Transform> spawnPoints = new();
    [SerializeField]
    private GameObject controllerpfab;
    private PawnController pawnController;

    public List<PawnController> pawnControllers = new();
    int turnIndex = 0;

    void Start()
    {
        int i = 0;
        foreach (Transform t in spawnPoints)
        {
            PawnController item = Instantiate(controllerpfab).GetComponent<PawnController>();
            if(i < 3)
            { 
                item.faction = Decker.Faction.player;
            }
            else
            {
                item.faction = Decker.Faction.enemy;
            }
            item.gameMaster = this;
            item.transform.SetPositionAndRotation(t.position, t.rotation);
            pawnControllers.Add(item);
            i++;
        }
        
    }

    [ContextMenu("PlayRound")]
    void PlayRound()
    {
        var currentController = pawnControllers[turnIndex].GetComponent<PawnController>();
        PawnController.onTurnEnd += StartWaitAfterTurnEnd;
        currentController.PlayTurn();
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
