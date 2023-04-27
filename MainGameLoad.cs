using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainGameLoad : MonoBehaviour
{
    private SaveLoad thesaveload;
    public static MainGameLoad instance;
    public void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
            Destroy(this.gameObject);
    }
    public void gameloadbutton()
    {
        //LoadingSceneManager.LoadScene("gamescene");
        StartCoroutine(LoadCo());
        
    }
     IEnumerator LoadCo()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("gamescene");
        while(!operation.isDone)
        {
            yield return null;
        }
        thesaveload = FindObjectOfType<SaveLoad>();
        thesaveload.LoadData();
        Destroy(gameObject);
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
