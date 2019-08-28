using UnityEngine;

public class Snack : MonoBehaviour
{
    #region Variables
    public enum SnackLevels { level1, level2, level3 };

    [SerializeField] private SnackLevels snackLevel;
    private bool isOnPlayer = false;
    private bool isDragged = false;
    public static int numInstance = 0;
    #endregion

    #region Accessors
    public SnackLevels SnackLevel
    {
        get { return snackLevel; }
    }

    public bool IsDragged
    {
        get { return isDragged; }
    }
    #endregion

    #region Methods
    private void GrabSnack()
    {
        if (Input.GetMouseButton(0))
        {
            isDragged = true;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            transform.position = mousePos;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (isOnPlayer)
            {
                isDragged = false;
                this.transform.parent = Player.instance.transform;
                transform.position = Player.instance.transform.position + Player.instance.SnackOffset;
                Player.instance.HasSnack = true;
            }
            else
            {
                //Destroy game object when not dragged on character
                numInstance--;
                Destroy(this.gameObject);
            }
        }
    }

    private void DropSnack()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Player.instance.HasSnack = false;
            numInstance--;
            Destroy(this.gameObject);
        }
    }
    #endregion

    private void Start()
    {
        numInstance++;
    }

    private void Update()
    {
        if (!Player.instance.HasSnack)
        {
            GrabSnack();
        }
        else
        {
            DropSnack();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isOnPlayer = true;
        }
        else
        {
            isOnPlayer = false;
        }
    }
}
