using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Variables
    [SerializeField] private Vector3 snackOffset;
    [SerializeField] private float moveSpeed = 50f;
    private float interpolation = 0;
    public static Player instance;
    private bool hasSnack = false,
                 isMoving = false;

    private Vector2[] travelPoints = new Vector2 [2];
    private Employee currentTarget;
    #endregion

    #region Accessors
    public Vector3 SnackOffset
    {
        get { return snackOffset; }
    }

    public bool HasSnack
    {
        get { return hasSnack; }
        set { hasSnack = value; }
    }

    public bool IsMoving
    {
        get { return isMoving; }
    }
    #endregion

    #region Methods
    /// <summary>
    /// Moves the player only when they have a snack grabbed.
    /// </summary>
    private void GoToPointer()
    {
        if (hasSnack)
        {
            //Move only when the clicked object is an employee and their car is parked.
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D rayHit = Physics2D.Raycast(mousePos, mousePos);

            if (rayHit.transform != null)
            {
                if (rayHit.transform.tag == "Employee" &&
                    rayHit.transform.GetComponent<Employee>().Car.IsParked &&
                    Input.GetMouseButtonDown(0))
                {
                    currentTarget = rayHit.transform.GetComponent<Employee>();
                    travelPoints[1] = rayHit.transform.position;
                    isMoving = true;
                }
            }
        }
    }

    private void MovePlayer()
    {        
        if (isMoving)
        {
            if (hasSnack)
            {
                interpolation += moveSpeed * Time.deltaTime;
            }
            else
            {
                interpolation -= moveSpeed * Time.deltaTime;
            }
        }

        if (interpolation > 1)
        {
            interpolation = 1;
        }
        else if (interpolation < 0)
        {
            interpolation = 0;
        }

        if (interpolation == 0)
        {
            isMoving = false;
            currentTarget = null;
        }

        transform.position = Vector2.Lerp(travelPoints[0], travelPoints[1], interpolation);
    }

    /// <summary>
    /// Move back to position when delivering the snack is unsuccessful.
    /// </summary>
    private void CheckForEmployee()
    {
        if (currentTarget != null &&
            !currentTarget.Car.IsParked)
        {
            isMoving = false;
            interpolation -= moveSpeed * Time.deltaTime;
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        instance = this.gameObject.GetComponent<Player>();
        travelPoints[0] = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        GoToPointer();
        MovePlayer();
        CheckForEmployee();
    }
}
