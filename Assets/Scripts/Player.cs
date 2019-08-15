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
    private void GoToPointer()
    {
        if (hasSnack)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                travelPoints[1] = mousePos;
                isMoving = true;
                Debug.Log("Logged!");
            }
        }
    }

    private void MovePlayer()
    {
        if (isMoving &&
            hasSnack)
        {
            interpolation += moveSpeed * Time.deltaTime;
            if (interpolation > 1)
            {
                interpolation = 1;
            }
        }
        else if (isMoving &&
                 !hasSnack)
        {
            interpolation -= moveSpeed * Time.deltaTime;
            if (interpolation < 0)
            {
                interpolation = 0;
                isMoving = false;
            }
        }
        transform.position = Vector2.Lerp(travelPoints[0], travelPoints[1], interpolation);
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
    }
}
