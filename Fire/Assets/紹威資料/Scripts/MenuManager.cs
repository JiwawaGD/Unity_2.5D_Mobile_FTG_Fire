using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject Setting;
    /// <summary>
    /// �C���}�l
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene("�Ы¿��");
    }
    /// <summary>
    /// �C������
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
