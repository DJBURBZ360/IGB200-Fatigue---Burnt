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

    public void OnCursorClick(GameObject item)
    {
        currentItem = item;
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
