using UnityEngine;
using UnityEngine.AI;

public class AnimationEvents : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [SerializeField] JuiceBottle juiceBottle;
    Animator animator;

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

    public void ReturnFromDamage()
    {
        if (animator.GetLayerWeight(AnimationConsts.DAMAGE_LAYER) > 0)
        {
            animator.SetLayerWeight(AnimationConsts.DAMAGE_LAYER, 0);
            print("CHEGA BELINGHAM");
        }
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Start()
    {
        animator = GetComponent<Animator>();
    }

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
