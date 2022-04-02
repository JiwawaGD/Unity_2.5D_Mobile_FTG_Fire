using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;       
using UnityEngine.Audio;    //聲音宣告

public class MenuManager : MonoBehaviour
{
    [Header("分辨率物件")]
    public Toggle[] resolutioinToggles;
    [Header("尺寸宣告")]
    public int[] screenWidths;


    [Header("索引質跟蹤")]
    private int activeScreenResIndex;

    [Header("音效調節器")]
    public AudioMixer audioMixer;

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
            //活動屏幕的分辨率=i
            activeScreenResIndex = i;
            //螢幕寬度 縱橫比
            float aspectration = 16 / 9f;
            //螢幕((數值)寬度為屏幕度，(數值)X/Y，(布林直)是否全屏=false)
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
            //判斷全屏為否
            resolutioinToggles[i].interactable = !isFullScreen;
        }
        //如果現在是全屏
        if (isFullScreen)
        {  
            //獲取手機支持的所有分辨綠的數組
            Resolution[] allResolutions = Screen.resolutions;
            //最大分辨率為=所有引所點[索引點長度-1]
            Resolution maxResolution = allResolutions[allResolutions.Length - 1];
            //分辨率最大點(寬、高、布林)
            Screen.SetResolution(maxResolution.width, maxResolution.height, true);
        }
        else 
        {
            //可用活動屏幕分辨率調用設置屏幕分辨率
            SetScreenResolution(activeScreenResIndex);
        }
    }
    public void SetVolune(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
    }
}
