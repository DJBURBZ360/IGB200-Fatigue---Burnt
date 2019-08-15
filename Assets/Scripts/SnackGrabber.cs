using UnityEngine;

public class SnackGrabber : MonoBehaviour
{
    public void OnCursorClick(GameObject snack)
    {
        if (!Player.instance.HasSnack)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            if (Snack.numInstance < 1)
            {
                Instantiate(snack, mousePos, Quaternion.identity);
            }
        }
    }
}
