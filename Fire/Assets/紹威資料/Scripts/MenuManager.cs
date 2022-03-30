using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject Setting;
    /// <summary>
    /// 遊戲開始
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene("紹威選單");
    }
    /// <summary>
    /// 遊戲結束
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
    public void setting()
    {
        Setting.SetActive(true);
    }
}
