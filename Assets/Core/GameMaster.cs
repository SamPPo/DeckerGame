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
    private int turnIndex = 0;

    void Start()
    {
        InitializePawns();        
    }

    // Initialize pawns and set their factions
    private void InitializePawns()
    {
        int i = 0;
        foreach (Transform t in spawnPoints)
        {
            PawnController item = Instantiate(controllerpfab).GetComponent<PawnController>();
            item.faction = i < 3 ? Decker.Faction.player : Decker.Faction.enemy;
            item.gameMaster = this;
            item.transform.SetPositionAndRotation(t.position, t.rotation);
            pawnControllers.Add(item);
            i++;
        }
    }

    //Get controller from list based on "turnIndex" and activate it. Add delegate to event "StartWaitAfterTurnEnd"
    [ContextMenu("PlayRound")]
    void PlayRound()
    {
        //make sure there are controllers
        if (pawnControllers.Count == 0) return;

        var currentController = pawnControllers[turnIndex].GetComponent<PawnController>();
        PawnController.onTurnEnd += StartWaitAfterTurnEnd;
        currentController.PlayTurn();
    }

    //Start couroutine after turnd ended for pawn
    void StartWaitAfterTurnEnd()
    {
        PawnController.onTurnEnd -= StartWaitAfterTurnEnd;
        StartCoroutine(WaitAfterTurnEnd());
    }

    //Wait after turn ended for pawn and activate Turn End
    IEnumerator WaitAfterTurnEnd()
    {
        yield return new WaitForSeconds(PublicVariables.TimeAfterPawnTurn);
        TurnEnd();
    }

    //Add to turn index and activate next round
    void TurnEnd()
    {
        turnIndex = (turnIndex + 1) % pawnControllers.Count;
        PlayRound();
    }


}
