using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class Loading : MonoBehaviour
{

    public GameObject LoadingScreen;

    public Animator anim;
    public float TranstionTime = 1.5f;
    public Slider LoadingBar;
    public TextMeshProUGUI percentagetxt;

    public void Load(int SceneNum)
    {
        if (LoadingScreen.activeSelf == false)
        {
            LoadingScreen.SetActive(true);
        }
        StartCoroutine(LoadScene(SceneNum));
    
    }

   

    IEnumerator LoadScene(int SceneNum)
    {
        anim.SetTrigger("Start");
        yield return new WaitForSecondsRealtime(TranstionTime);
        AsyncOperation AsyncLoading = SceneManager.LoadSceneAsync(SceneNum);
      
        // Fill loading bar while  loading
      //  while (!AsyncLoading.isDone)
      //  {
      //      float progress = Mathf.Clamp01(AsyncLoading.progress / .9f);
      //      LoadingBar.value = progress;
      //      percentagetxt.text = progress * 100f + "%";
       //    yield return null;
       // }
    }

    //Loads Main Menu
    public void LoadMainMenu()
    {
        StartCoroutine(LoadScene(0));
    }
    //Loads a scene by taking in its index number
    public void LoadALevel(int num)
    {

        StartCoroutine(LoadScene(num));

    }

}
