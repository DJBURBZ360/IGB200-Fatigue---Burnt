using UnityEngine;

public class Item : MonoBehaviour
{
    #region Variables
    public enum ItemLevels { level1, level2, level3 };

    [SerializeField] private ItemLevels itemLevel;
    [SerializeField] private Employee.FatigueTypes forFatigueType;
    [SerializeField] private GameObject itemSFX;
    private bool isOnPlayer = false;
    private bool isDragged = false;
    public static int numInstance = 0;
    #endregion

    #region Accessors
    public ItemLevels ItemLevel
    {
        get { return itemLevel; }
    }

    public bool IsDragged
    {
        get { return isDragged; }
    }

    public Employee.FatigueTypes ForFatigueType
    {
        get { return forFatigueType; }
    }
    #endregion

    #region Methods
    private void GrabItem()
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
                transform.position = Player.instance.transform.position + Player.instance.ItemOffset;
                Player.instance.HasItem = true;
            }
            else
            {
                //Destroy game object when not dragged on character
                numInstance--;
                Destroy(this.gameObject);
            }
        }
    }

    private void DropItem()
    {
        if (Input.GetMouseButtonDown(1) ||
            Input.GetButton("Drop Item"))
        {
            Player.instance.HasItem = false;
            numInstance--;
            Destroy(this.gameObject);
        }
    }

    public void PlaySound()
    {
        Instantiate(itemSFX, transform.position, Quaternion.identity);
    }
    #endregion

    private void Start()
    {
        numInstance++;
    }

    private void Update()
    {
        if (!Player.instance.HasItem)
        {
            GrabItem();
        }
        else
        {
            DropItem();
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
