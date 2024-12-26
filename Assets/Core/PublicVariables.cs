using Decker;
using UnityEngine;

public static class PublicVariables
{
    //TIMERS
    public static float GlobalTimeMultiplier = 1f;
    public static float TimeAfterCardPlay = 0.6f / GlobalTimeMultiplier;
    public static float TimeAfterPawnTurn = 0.5f / GlobalTimeMultiplier;
    public static float TimeToMoveCard = 1.0f / GlobalTimeMultiplier;
    public static float TimeAfterEffect = 0.2f / GlobalTimeMultiplier;
    public static float TimeSpawnCardMove = 0.4f / GlobalTimeMultiplier;

    //Card params
    public static float CardThickness = 0.13f;
    public static float CardWidth = 1.74f;

    public static void SetTransform(Transform set, StTransform target)
    {
        set.SetPositionAndRotation(target.pos, target.rot);
        set.localScale = target.sca;
    }


    public static StTransform MakeTransform(Transform transform)
    {
        StTransform st;
        st.pos = transform.position;
        st.rot = transform.rotation;
        st.sca = transform.localScale;
        return st;
    }
}
