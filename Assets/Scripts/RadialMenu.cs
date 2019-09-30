using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialMenu : MonoBehaviour
{
    #region Variables
    [SerializeField] private float fadeRate = 3f;
    [SerializeField] private GameObject[] radialMenuChunks = new GameObject[3];

    private Button[] items = new Button[6];           //indices 0 - 1 = SLPS, 2 - 3 = HDCH, 4 - 5 = DZNS
    private Image[] radialMenuImages = new Image[12]; //indices 0 - 3 = SLPS, 4 - 7 = HDCH, 8 - 11 = DZNS
    private GameManager manager;
    
    private Fader fader = new Fader();

    private const int NUM_FATIGUE_TYPES = 3;
    #endregion

    #region Public Methods
    public void FadeInMenu()
    {       
        for (int i = 0; i < radialMenuImages.Length; i++)
        {
            Color color = radialMenuImages[i].color;

            if (CheckForFatigueTypes(i, radialMenuImages.Length))
            {
                fader.DoFadeIn(ref color.a, fadeRate);
            }
            else
            {
                fader.DoFadeIn(ref color.a, fadeRate, 0.5f);
            }
            radialMenuImages[i].color = color;
        }        
    }

    public void FadeOutMenu()
    {        
        for (int i = 0; i < radialMenuImages.Length; i++)
        {
            Color color = radialMenuImages[i].color;
            fader.DoFadeOut(ref color.a, fadeRate);
            radialMenuImages[i].color = color;
        }
    }
    #endregion

    #region Private Methods
    /// <summary>
    /// Checks whether the item for a fatigue type is an available fatigue type in the level.
    /// </summary>
    /// <param name="index">The current index the item is being checked.</param>
    /// <param name="arrayLength">The array length of the targeted list to be checked.</param>
    /// <returns></returns>
    private bool CheckForFatigueTypes(int index, int arrayLength)
    {
        bool judgement = false;
        int arraySkip = arrayLength / NUM_FATIGUE_TYPES;
        int[] arrayIndices = new int[NUM_FATIGUE_TYPES * 2];

        //sets mins and max values indicators per fatigue type
        int j = 0;
        for (int i = 0; i < arrayIndices.Length; i += 2)
        {
            arrayIndices[i] = j;
            j += arraySkip - 1;

            arrayIndices[i + 1] = j;
            j++;
        }
        
        foreach (Employee.FatigueTypes fatigueType in manager.AvailableFatigueTypes)
        {
            
            if (fatigueType == Employee.FatigueTypes.Sleepiness)
            {
                if (index >= arrayIndices[0] && index <= arrayIndices[1]) judgement = true;
            }
            else if (fatigueType == Employee.FatigueTypes.Headache)
            {
                if (index >= arrayIndices[2] && index <= arrayIndices[3]) judgement = true;
            }
            else if (fatigueType == Employee.FatigueTypes.Dizziness)
            {
                if (index >= arrayIndices[4] && index <= arrayIndices[5]) judgement = true;
            }
        }

        return judgement;
    }

    /// <summary>
    /// If an item's fatigue type is available in the level, then make it interactable. Else, don't
    /// </summary>
    private void ChangeButtonInteractivity()
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].interactable = CheckForFatigueTypes(i, items.Length);
        }
    }
    #endregion

    void Start()
    {
        int imageIndex = 0;
        int buttonIndex = 0;
        for (int i = 0; i < radialMenuChunks.Length; i++)
        {
            GameObject outerRing = radialMenuChunks[i].transform.GetChild(0).gameObject;
            GameObject innerRing = radialMenuChunks[i].transform.GetChild(1).gameObject;

            //get button references
            items[buttonIndex] = outerRing.GetComponent<Button>();
            items[buttonIndex + 1] = innerRing.GetComponent<Button>();
            buttonIndex += 2;

            //get images references  
            //radial menu chunk image ref
            radialMenuImages[imageIndex] = outerRing.GetComponent<Image>();
            radialMenuImages[imageIndex + 1] = innerRing.GetComponent<Image>();

            //item in chunk image ref
            radialMenuImages[imageIndex + 2] = outerRing.transform.GetChild(0).GetComponent<Image>();
            radialMenuImages[imageIndex + 3] = innerRing.transform.GetChild(0).GetComponent<Image>();
            imageIndex += 4;
        }

        //disable buttons all by default
        foreach (Button button in items)
        {
            button.interactable = false;
        }

        //hide all images by default
        foreach (Image image in radialMenuImages)
        {
            image.color = new Color(image.color.r, 
                                    image.color.g, 
                                    image.color.b, 
                                    0f);
        }

        manager = GameObject.FindWithTag("Managers").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeButtonInteractivity();
    }
}
