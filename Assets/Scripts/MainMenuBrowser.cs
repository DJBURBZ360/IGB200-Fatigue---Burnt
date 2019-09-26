using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBrowser : MonoBehaviour
{
    #region Variables
    public GameObject howToPlayArea;
    public GameObject levelSelectArea;

    [SerializeField] private float moveSpeed = 2.0f;
    private float interpolation = 0;
    private bool moveUp = false;
    private bool moveRight = false;
    private bool isMoving = false;
    private Vector2 originalPos;
    private Vector2 destination;
    #endregion

    #region Public Methods
    public void PanUp()
    {
        if (!moveRight &&
            !moveUp)
        {
            destination = howToPlayArea.transform.position;
            moveUp = true;
            isMoving = true;
        }
    }

    public void PanDown()
    {
        if (!moveRight &&
            moveUp)
        {
            destination = howToPlayArea.transform.position;
            moveUp = false;
            isMoving = true;
        }
    }

    public void PanRight()
    {
        if (!moveUp &&
            !moveRight)
        {
            destination = levelSelectArea.transform.position;
            moveRight = true;
            isMoving = true;
        }
    }

    public void PanLeft()
    {
        if (!moveUp &&
            moveRight)
        {
            destination = levelSelectArea.transform.position;
            moveRight = false;
            isMoving = true;
        }
    }
    #endregion

    #region Private Methods
    private void MoveTo()
    {
        if (interpolation != 1 &&
            interpolation < 1)
        {
            interpolation += moveSpeed * Time.deltaTime;
            
            if (interpolation >= 1)
            {
                isMoving = false;
                interpolation = 1;
            }
        }
    }

    private void MoveBack()
    {
        if (interpolation != 0 &&
            interpolation > 0)
        {
            interpolation -= moveSpeed * Time.deltaTime;

            if (interpolation <= 0)
            {
                isMoving = false;
                interpolation = 0;
            }
        }
    }

    private void PanCamera()
    {
        if (isMoving)
        {
            if (moveRight || moveUp) MoveTo();
            else if (!moveRight || !moveUp) MoveBack();
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        originalPos = Camera.main.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        PanCamera();
        Camera.main.transform.position = Vector3.Lerp(originalPos, destination, interpolation);
    }
}
