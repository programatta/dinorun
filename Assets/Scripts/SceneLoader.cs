using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader: MonoBehaviour
{
    [SerializeField] GameObject crossFade;
    [SerializeField] Animator transition;
    public float transitionTime = 1f;
    public static SceneLoader Instance = null;

    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
            crossFade.SetActive(true);
        }
        else if( Instance != this)
            Destroy(gameObject);
    }

    public void LoadNextLevel(string levelName)
    {
        StartCoroutine(LoadLevel(levelName));
    }


    /*=======================================================================*/
    /*                              Private Section                          */
    /*=======================================================================*/
    IEnumerator LoadLevel(string levelName)
    {
        //Play animation.
        transition.SetTrigger("Start");

        //Wait (usamos realTime ya que paramos el juego con Time.scaleTime=0 y 
        //asi evitamos que se nos quede parada la corrutina.)
        yield return new WaitForSecondsRealtime(transitionTime);

        // Load
        SceneManager.LoadScene(levelName);
    }
}
