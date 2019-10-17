using UnityEngine;

public class ItemGrabber : MonoBehaviour
{
    private GameObject currentItem;

    private void GetNewItem()
    {
        if (Item.numInstance < 1)
        {
            PlayerStats.NumItemsGrabbed++;

            Vector3 offset = Player.instance.transform.position + Player.instance.ItemOffset;            
            Player.instance.CurrentItem = Instantiate(currentItem, offset, currentItem.transform.rotation, Player.instance.transform);
            if (Player.instance.transform.localScale.x < 0) Player.instance.ForceFlipItem();
            Player.instance.HasItem = true;
        }
    }

    /// <summary>
    /// Returns true if the currently held item and the new item doesn't match their types or when there's not currently held item.
    /// </summary>
    private bool CheckForItem()
    {
        if (Player.instance.CurrentItem != null)
        {
            Item newInstance = currentItem.GetComponent<Item>();
            Item currentInstance = Player.instance.CurrentItem.GetComponent<Item>();

            if (newInstance.name + "(Clone)" != currentInstance.name)
            {
                return true;
            }
            else
            {
                return false;
            }            
        }
        else
        {
            return true;
        }
    }

    public void OnCursorClick(GameObject item)
    {
        currentItem = item;
        if (CheckForItem())
        {
            print("grabbed");
            if (!Player.instance.HasItem)
            {
                GetNewItem();
            }
            else
            {
                Player.instance.SimulateDropItem();
                GetNewItem();
            }
        }
    }
}
