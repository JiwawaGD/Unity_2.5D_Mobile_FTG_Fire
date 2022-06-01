using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;    //�n���ŧi

public class MenuManager : MonoBehaviour
{
    public Button mybutton;
    public GameObject Gwawa;
    public GameObject Gwawaimage;
    public GameObject MatchstickMen;
    public GameObject Goose;
    [Header("�L�k���}�e��")]
    public GameObject juck;
    [Header("����v����")]
    public Toggle[] resolutioinToggles;
    [Header("�ؤo�ŧi")]
    public int[] screenWidths;

    //private MenuGwawaA menugwawaa;


    [Header("���޽����")]
    private int activeScreenResIndex;
    [Header("���d���")]
    private int lvIndex;

    [Header("���Ľո`��")]
    public AudioMixer audioMixer;

    public void Open()
    {
        juck.SetActive(true);
    }
    public void Close()
    {
        juck.SetActive(false);
    }
    public void Start()
    {
        mybutton = GetComponent<Button>();
        //mybutton.onClick.AddListener(ButtonOnclick);
    }
    /// <summary>
    /// �C���}�l
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene("�a�Ͽ��");
    }
    /// <summary>
    /// �e���U�@��
    /// </summary>
    /*private void ButtonOnclick()
    {

        lvIndex = SceneManager.GetActiveScene().buildIndex;

        lvIndex++;

        SceneManager.LoadScene(lvIndex);

    }*/
    /// <summary>
    /// �N�����﨤
    /// </summary>
    public void GwawaSetting()
    {
        if (Gwawa != null)
        {
            //menugwawaa.StartGwawaM();
            Animator animator = Gwawa.GetComponent<Animator>();
            if (animator != null)
            {
                bool isOpen = animator.GetBool("Touch");
                animator.SetBool("Touch", !isOpen);
            }
        }
    }
    /// <summary>
    /// ����H�﨤
    /// </summary>
    public void MatchstickMenSetting()
    {
        if (MatchstickMen != null)
        {
            Animator animator = MatchstickMen.GetComponent<Animator>();
            if (animator != null)
            {
                bool isOpen = animator.GetBool("Touch");
                animator.SetBool("Touch", !isOpen);
            }
        }
    }
    /// <summary>
    /// ���Z�﨤
    /// </summary>
    public void GooseSetting()
    {
        if (Goose != null)
        {
            Animator animator = Goose.GetComponent<Animator>();
            if (animator != null)
            {
                bool isOpen = animator.GetBool("Touch");
                animator.SetBool("Touch", !isOpen);
            }
        }
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
