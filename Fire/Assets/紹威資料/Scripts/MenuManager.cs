using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;       
using UnityEngine.Audio;    //�n���ŧi

public class MenuManager : MonoBehaviour
{
    [Header("����v����")]
    public Toggle[] resolutioinToggles;
    [Header("�ؤo�ŧi")]
    public int[] screenWidths;


    [Header("���޽����")]
    private int activeScreenResIndex;

    [Header("���Ľո`��")]
    public AudioMixer audioMixer;

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
            //���ʫ̹�������v=i
            activeScreenResIndex = i;
            //�ù��e�� �a���
            float aspectration = 16 / 9f;
            //�ù�((�ƭ�)�e�׬��̹��סA(�ƭ�)X/Y�A(���L��)�O�_����=false)
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
            //�P�_���̬��_
            resolutioinToggles[i].interactable = !isFullScreen;
        }
        //�p�G�{�b�O����
        if (isFullScreen)
        {  
            //������������Ҧ�����񪺼Ʋ�
            Resolution[] allResolutions = Screen.resolutions;
            //�̤j����v��=�Ҧ��ީ��I[�����I����-1]
            Resolution maxResolution = allResolutions[allResolutions.Length - 1];
            //����v�̤j�I(�e�B���B���L)
            Screen.SetResolution(maxResolution.width, maxResolution.height, true);
        }
        else 
        {
            //�i�ά��ʫ̹�����v�եγ]�m�̹�����v
            SetScreenResolution(activeScreenResIndex);
        }
    }
    public void SetVolune(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
    }
}
