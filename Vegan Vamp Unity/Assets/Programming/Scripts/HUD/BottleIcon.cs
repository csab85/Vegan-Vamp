using TMPro;
using UnityEngine;

public class BottleIcon : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    //game object
    [SerializeField] GameObject description; 

    //components
    Rigidbody2D rb;

    //scripts
    StatsManager selfStats;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [SerializeField] float smallSize;
    [SerializeField] float largeSize;
    [SerializeField] bool updateDescription;

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

    public void ShowDescription()
    {
        description.SetActive(true);
    }

    public void HideDescription()
    {
        description.SetActive(false);
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Start()
    {
        //get components
        rb = GetComponent<Rigidbody2D> ();

        //scripts
        selfStats = GetComponent<StatsManager>();

        //set description label
        if (updateDescription)
        {
            string descriptionText = "";
            int height = 0;

            foreach (string ingredientName in selfStats.descriptionDict.Keys)
            {
                if (selfStats.descriptionDict[ingredientName] > 0)
                {
                    descriptionText += $"{ingredientName} ({selfStats.descriptionDict[ingredientName]})\n";
                    height += 50;
                }
            }

            //translate
            descriptionText = descriptionText.Replace("Health Ingredient", "Ingrediente de Saúde");
            descriptionText = descriptionText.Replace("Fire Ingredient", "Ingrediente de Fogo");
            descriptionText = descriptionText.Replace("Ice Ingredient", "Ingrediente de Gelo");
            descriptionText = descriptionText.Replace("Tornado Ingredient", "Ingrediente de Tornado");
            descriptionText = descriptionText.Replace("Speed Ingredient", "Ingrediente de Velocidade");
            descriptionText = descriptionText.Replace("Gravity Ingredient", "Ingrediente de Gravidade");
            descriptionText = descriptionText.Replace("Teleport Ingredient", "Ingrediente de Teleporte");

            TextMeshProUGUI textMesh = description.transform.Find("Text").GetComponent<TextMeshProUGUI>();
            RectTransform textRect = description.transform.Find("Text").GetComponent<RectTransform>();
            RectTransform bgRect = description.transform.Find("Background").GetComponent<RectTransform>();


            textMesh.text = descriptionText;
            textRect.sizeDelta = new Vector2(480, height);
            bgRect.sizeDelta = textRect.sizeDelta;
        }
    }

    void Update()
    {
        if (placeInBag == Place.Inside)
        {
            //size
            if (transform.localScale.x != smallSize)
            {
                Vector2 targetSize = new Vector2(smallSize, smallSize);

                transform.localScale = targetSize;
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

                transform.localScale = targetSize;
            }

            //rotation
            if (transform.rotation != Quaternion.identity)
            {
                transform.rotation = Quaternion.Euler(Vector2.MoveTowards(transform.rotation.eulerAngles, Vector2.zero, 0.1f));
            }

            //physics
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        //kp description rotation right
        description.transform.rotation = Quaternion.identity;
    }

    #endregion
    //========================


}
