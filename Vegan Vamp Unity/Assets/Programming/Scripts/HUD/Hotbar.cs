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

    [SerializeField] Vector2 largePosit;
    [SerializeField] float largeScale;

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


    public void SeekAndDestroyBottles()
    {
        foreach (BeltSlot slot in slots)
        {
            slot.DeleteBottle();
        }
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
            foreach (BeltSlot slot in slots)
            {
                selectedSlot = (selectedSlot + 1) % slots.Length;

                //stop going to next if the slot has a bottle in it
                if (slots[selectedSlot].juiceIcon != null)
                {
                    SelectSlot(selectedSlot);
                    break;
                }
            }
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
                rect.localScale = new Vector3(largeScale, largeScale, largeScale);
                rect.anchoredPosition = largePosit;
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
