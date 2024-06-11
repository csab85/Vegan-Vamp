using UnityEngine;

public class Hotbar : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    //components
    RectTransform rect;

    //scripts
    [SerializeField] BeltSlot[] slots;
    [SerializeField] Inventory inventory;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [HideInInspector] public int selectedSlot = -1;
    Vector2 basePosit;
    Vector3 baseScale;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    public void DeselectAll()
    {
        foreach(BeltSlot slot in slots)
        {
            slot.DeselectBottle();
        }
    }

    void SelectSlot(int slotIndex)
    {
        slots[slotIndex].SelectBottle();
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region
    private void Start()
    {
        rect = GetComponent<RectTransform>();
        basePosit = rect.anchoredPosition;
        baseScale = rect.localScale;
    }

    private void Update()
    {
        //inputs
        if (Input.mouseScrollDelta.y != 0)
        {
            while (true)
            {
                selectedSlot = (selectedSlot + 1) % slots.Length;

                //stop going to next if the slot has a bottle in it
                if (slots[selectedSlot].juiceIcon != null)
                {
                    break;
                }
            }
            SelectSlot(selectedSlot);
        }

        //hotbar
        if (Input.GetKeyDown(KeyCode.Alpha1) | Input.GetKeyDown(KeyCode.Keypad1))
        {
            selectedSlot = 0;
            SelectSlot(selectedSlot);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) | Input.GetKeyDown(KeyCode.Keypad2))
        {
            selectedSlot = 1;
            SelectSlot(selectedSlot);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) | Input.GetKeyDown(KeyCode.Keypad3))
        {
            selectedSlot = 2;
            SelectSlot(selectedSlot);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) | Input.GetKeyDown(KeyCode.Keypad4))
        {
            selectedSlot = 3;
            SelectSlot(selectedSlot);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5) | Input.GetKeyDown(KeyCode.Keypad5))
        {
            selectedSlot = 4;
            SelectSlot(selectedSlot);
        }

        if (Input.GetKeyDown(KeyCode.Alpha6) | Input.GetKeyDown(KeyCode.Keypad6))
        {
            selectedSlot = 5;
            SelectSlot(selectedSlot);
        }

        //control size and position
        if (inventory.openMode)
        {
            if (rect.localScale != Vector3.one || rect.anchoredPosition != Vector2.zero)
            {
                rect.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                rect.anchoredPosition = new Vector2(0, -150);
            }
        }
        else
        {
            if (rect.localScale != baseScale || rect.anchoredPosition != basePosit)
            {
                rect.localScale = baseScale;
                rect.anchoredPosition = basePosit;
            }
        }
    }

    #endregion
    //========================


}
