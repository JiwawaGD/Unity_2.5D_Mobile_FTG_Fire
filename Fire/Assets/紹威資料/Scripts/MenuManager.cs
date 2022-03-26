using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Slider[] volumSliders;
    public Toggle[] resolutioinToggles;
    public int[] screenWidths;
    public int activeScreenResIndex;

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
    /// <summary>
    /// �C���e���]�w
    /// </summary>
    /// <param name="i"></param>
    public void SetScreenResolution(int i)
    {
        if (resolutioinToggles[i].isOn) 
        {
            activeScreenResIndex = i;
            //�e����]�w
            float aspectration = 16 / 9f;
            //�ù�(�e�סA���סA�O�_����=false)
            Screen.SetResolution(screenWidths[i], (int)(screenWidths[i] / aspectration), false);
        }
    }
    /// <summary>
    /// �C�����ù��]�w
    /// </summary>
    /// <param name="isFullScreen"></param>
    public void SetFullScreen(bool isFullScreen)
    {
        for (int i = 0; i < resolutioinToggles.Length; i++)
        {
            resolutioinToggles[i].interactable = !isFullScreen;
        }
        if (isFullScreen)
        {
            Resolution[] allResolutions = Screen.resolutions;
            Resolution maxResolution = allResolutions[allResolutions.Length - 1];
            Screen.SetResolution(maxResolution.width, maxResolution.height, true);
        }
        else 
        {
            SetScreenResolution(activeScreenResIndex);
        }
    }
    /// <summary>
    /// �C���D���ĳ]�w
    /// </summary>
    /// <param name="value"></param>
    public void SetMasterVolume(float value)
    { 
        
    }
    /// <summary>
    /// �C�����ֳ]�w
    /// </summary>
    /// <param name="value"></param>
    public void SetMusicVolume(float value)
    {

    }
    /// <summary>
    /// �C�����ĳ]�w
    /// </summary>
    /// <param name="value"></param>
    public void SetSfxVolume(float value)
    {

    }
}
