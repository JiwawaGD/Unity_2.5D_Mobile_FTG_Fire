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
    /// <summary>
    /// 遊戲畫面設定
    /// </summary>
    /// <param name="i"></param>
    public void SetScreenResolution(int i)
    {
        if (resolutioinToggles[i].isOn) 
        {
            activeScreenResIndex = i;
            //寬高比設定
            float aspectration = 16 / 9f;
            //螢幕(寬度，高度，是否全屏=false)
            Screen.SetResolution(screenWidths[i], (int)(screenWidths[i] / aspectration), false);
        }
    }
    /// <summary>
    /// 遊戲全螢幕設定
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
    /// 遊戲主音效設定
    /// </summary>
    /// <param name="value"></param>
    public void SetMasterVolume(float value)
    { 
        
    }
    /// <summary>
    /// 遊戲音樂設定
    /// </summary>
    /// <param name="value"></param>
    public void SetMusicVolume(float value)
    {

    }
    /// <summary>
    /// 遊戲音效設定
    /// </summary>
    /// <param name="value"></param>
    public void SetSfxVolume(float value)
    {

    }
}
