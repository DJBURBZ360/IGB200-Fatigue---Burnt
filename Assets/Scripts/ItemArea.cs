using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemArea : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject clipboard;
    [SerializeField] private GameObject itemsUI;
    [SerializeField] private Vector2 popUpPosition;
    [SerializeField] private float interpolationSpeed;
    
    private float currentInterpolation = 0;
    private bool isPlayerNear = false;
    private Vector2 originalPosition;
    private BoxCollider2D collider;
    public Button[] items = new Button[6];
    #endregion

    #region Methods
    private void PopUpClipboard()
    {
        if (isPlayerNear)
        {
            if (currentInterpolation < 1)
            {
                currentInterpolation += interpolationSpeed * Time.deltaTime;
            }
            else if (currentInterpolation > 1)
            {
                currentInterpolation = 1;
            }
        }
        else
        {
            if (currentInterpolation > 0)
            {
                currentInterpolation -= interpolationSpeed * Time.deltaTime;
            }
            else if (currentInterpolation < 0)
            {
                currentInterpolation = 0;
            }
        }
    }

    /// <summary>
    /// Moves the clipboard and converts the new position into screen space for the canvas to move.
    /// </summary>
    private void MoveClipBoard()
    {
        Vector2 position = Vector2.Lerp(originalPosition, popUpPosition, currentInterpolation);
        clipboard.transform.position = Camera.main.WorldToScreenPoint(position);
    }

    /// <summary>
    /// Only allows item grabbing when the player is within the area, and the clipboard is fully retracted.
    /// </summary>
    private void ToggleItems()
    {
        if (isPlayerNear &&
            currentInterpolation >= 1)
        {
            foreach (Button item in items)
            {
                item.interactable = true;
            }
        }
        else
        {
            foreach (Button item in items)
            {
                item.interactable = false;
            }
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = Camera.main.ScreenToWorldPoint(clipboard.transform.position);
        collider = this.gameObject.GetComponent<BoxCollider2D>();
        items = itemsUI.transform.GetComponentsInChildren<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveClipBoard();
        PopUpClipboard();
        ToggleItems();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isPlayerNear = false;
        }
    }
}
