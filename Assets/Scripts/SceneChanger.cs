using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger: MonoBehaviour
{
    private float originalTimeScale;

    private void Awake()
    {
        originalTimeScale = Time.timeScale;
    }
    public void ChangeScene(string nextScene)
    {
        Time.timeScale = originalTimeScale;
        SceneManager.LoadScene(nextScene);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
