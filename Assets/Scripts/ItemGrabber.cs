using UnityEngine;

public class ItemGrabber : MonoBehaviour
{
    public void OnCursorClick(GameObject item)
    {
        if (!Player.instance.HasItem)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            if (Item.numInstance < 1)
            {
                Instantiate(item, mousePos, item.transform.rotation);
            }
        }
    }
}
