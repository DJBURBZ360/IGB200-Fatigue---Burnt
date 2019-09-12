using UnityEngine.UI;
using UnityEngine;

public class ItemArea : MonoBehaviour
{
    #region Variables
    private Image uiRenderer;
    private SpriteRenderer renderer;
    private bool isPlayerAway = false;
    private Fader spriteFader = new Fader();
    private Fader uiFader = new Fader();
    [SerializeField] private float fadeRate = 3;
    [SerializeField] private float maxOpacity = 0.5f;
    [SerializeField] private GameObject snackUI;
    [SerializeField] private GameObject snackListUI;
    #endregion

    #region Methods
    /// <summary>
    /// Fades out the snack UI, and fades in the snack area box when player is away.
    /// </summary>
    private void FadeObjects()
    {
        if (isPlayerAway)
        {
            float opacity = renderer.color.a;
            float opacity2 = uiRenderer.color.a;

            spriteFader.DoFadeIn(ref opacity, fadeRate, maxOpacity);
            uiFader.DoFadeOut(ref opacity2, fadeRate);

            renderer.color = new Color(renderer.color.r,
                                       renderer.color.g,
                                       renderer.color.b,
                                       opacity);

            uiRenderer.color = new Color(uiRenderer.color.r,
                                         uiRenderer.color.g,
                                         uiRenderer.color.b,
                                         opacity2);
        }
        else
        {
            float opacity = renderer.color.a;
            float opacity2 = uiRenderer.color.a;

            spriteFader.DoFadeOut(ref opacity, fadeRate);
            uiFader.DoFadeIn(ref opacity2, fadeRate);

            renderer.color = new Color(renderer.color.r,
                                       renderer.color.g,
                                       renderer.color.b,
                                       opacity);

            uiRenderer.color = new Color(uiRenderer.color.r,
                                         uiRenderer.color.g,
                                         uiRenderer.color.b,
                                         opacity2);
        }
    }

    /// <summary>
    /// Locks the player movement when accessing the snacks
    /// </summary>
    private void LockPlayerMovement()
    {
        if (snackListUI.activeSelf)
        {
            Player.instance.EnableMovement = false;
        }
        else
        {
            Player.instance.EnableMovement = true;
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        renderer = this.gameObject.GetComponent<SpriteRenderer>();
        uiRenderer = snackUI.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        FadeObjects();
        LockPlayerMovement();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isPlayerAway = false;
            snackUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isPlayerAway = true;
            snackUI.SetActive(false);
        }
    }
}
