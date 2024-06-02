using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [SerializeField] JuiceBottle juiceBottle;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region



    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    void Throw()
    {
        juiceBottle.ThrowBottle();
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region



    #endregion
    //========================


}

public static class AnimationConsts
{
    public const int MOVEMENT_LAYER = 0;
    public const int GUN_LAYER = 1;
    public const int BOTTLE_LAYER = 2;
    public const int DAMAGE_LAYER = 3;
}
