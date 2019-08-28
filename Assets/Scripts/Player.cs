using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Variables
    [SerializeField] private Vector3 snackOffset;
    [SerializeField] private float moveSpeed = 50f;
    [SerializeField] private Vector2[] boundaries =  new Vector2[2]; //0 - min, 1 - max
    private float interpolation = 0;
    public static Player instance;
    private bool hasSnack = false;
    private bool enableMovement = true;

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

    public bool EnableMovement
    {
        get { return enableMovement; }
        set { enableMovement = value; }
    }
    #endregion

    #region Methods
    /*
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
    */

    private void Move()
    {
        if (Input.GetAxis("Horizontal") < 0) //left
        {
            transform.position -= gameObject.transform.right * moveSpeed * Time.deltaTime;
        }
        else if (Input.GetAxis("Horizontal") > 0) //right
        {
            transform.position += gameObject.transform.right * moveSpeed * Time.deltaTime;
        }

        if (Input.GetAxis("Vertical") < 0) //down
        {
            transform.position -= gameObject.transform.up * moveSpeed * Time.deltaTime;
        }
        else if (Input.GetAxis("Vertical") > 0) //up
        {
            transform.position += gameObject.transform.up * moveSpeed * Time.deltaTime;
        }
    }
    
    private void LimitMovement()
    {
        //horizontal boundary
        if (transform.position.x < boundaries[0].x)
        {
            transform.position = new Vector2(boundaries[0].x,
                                             transform.position.y);
        }
        else if (transform.position.x > boundaries[1].x)
        {
            transform.position = new Vector2(boundaries[1].x,
                                             transform.position.y);
        }

        //vertical boundary
        if (transform.position.y < boundaries[0].y)
        {
            transform.position = new Vector2(transform.position.x,
                                             boundaries[0].y);
        }
        else if (transform.position.y > boundaries[1].y)
        {
            transform.position = new Vector2(transform.position.x,
                                             boundaries[1].y);
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
        //GoToPointer();
        //MovePlayer();
        //CheckForEmployee();

        if (enableMovement) Move();
        LimitMovement();
    }
}
