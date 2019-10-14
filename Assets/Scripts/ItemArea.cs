using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemArea : MonoBehaviour
{
    #region Variables
    [SerializeField] private RadialMenu radialMenu;
    private bool isPlayerNear = false;
    private BoxCollider2D collider;
    #endregion

    #region Methods
    private void CheckForPlayer()
    {
        if (isPlayerNear)
        {
            radialMenu.FadeInMenu();            
        }
        else
        {
            radialMenu.FadeOutMenu();
        }
        //Only make the buttons interactable when the player is near
        radialMenu.ToggleButtons(isPlayerNear);
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        collider = this.gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForPlayer();
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
