using UnityEngine;

public class BottleIcon : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    //components
    Rigidbody2D rb;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [SerializeField] float smallSize;
    [SerializeField] float largeSize;

    public enum Place
    {
        None,
        Inside,
        Outside,
    }

    public Place placeInBag = Place.Outside;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region



    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Start()
    {
        //get components
        rb = GetComponent<Rigidbody2D> ();
    }

    void Update()
    {
        if (placeInBag == Place.Inside)
        {
            //size
            if (transform.localScale.x != smallSize)
            {
                Vector2 targetSize = new Vector2(smallSize, smallSize);

                transform.localScale = Vector2.MoveTowards(transform.localScale, targetSize, 0.02f);
            }

            //physics
            rb.constraints = RigidbodyConstraints2D.None;
        }

        if (placeInBag == Place.Outside)
        {
            //size
            if (transform.localScale.x != largeSize)
            {
                Vector2 targetSize = new Vector2(largeSize, largeSize);

                transform.localScale = Vector2.MoveTowards(transform.localScale, targetSize, 0.02f);
            }

            //rotation
            if (transform.rotation != Quaternion.identity)
            {
                transform.rotation = Quaternion.Euler(Vector2.MoveTowards(transform.rotation.eulerAngles, Vector2.zero, 0.1f));
            }

            //physics
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

    }

    #endregion
    //========================


}
