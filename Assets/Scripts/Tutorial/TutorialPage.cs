using UnityEngine;

public class TutorialPage : MonoBehaviour
{
    public GameObject pageOne;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
    }


    public void finishTutorial()
    {
        pageOne.SetActive(false);
        Time.timeScale = 1f;
        GameObject.FindWithTag("Managers").GetComponent<GameManager>().StartLevel();
    }

}
