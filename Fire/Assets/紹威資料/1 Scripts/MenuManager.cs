using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;    //聲音宣告
namespace WEI
{
    public class MenuManager : MonoBehaviour
    {
        [Header("開始鍵")]
        public Button startButton;
        //public GameObject Gwawa;
        //public GameObject Gwawaimage;
        //public GameObject MatchstickMen;
        //public GameObject Goose;
        [Header("無法離開畫面")]
        public GameObject juck;
        [Header("分辨率物件")]
        public Toggle[] resolutioinToggles;
        [Header("尺寸宣告")]
        public int[] screenWidths;

        //private MenuGwawaA menugwawaa;


        [Header("索引質跟蹤")]
        private int activeScreenResIndex;
        [Header("關卡抓取")]
        private int lvIndex;

        [Header("音效調節器")]
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
            startButton = GetComponent<Button>();
            //mybutton.onClick.AddListener(ButtonOnclick);
        }
        /// <summary>
        /// 遊戲開始
        /// </summary>
        public void StartGame()
        {
            SceneManager.LoadScene("遊戲操作介紹");
        }
        /// <summary>
        /// 遊戲操作介紹場景
        /// </summary>
        public void Game_operation_introduction()
        {
            SceneManager.LoadScene("遊戲操作介紹");
        }
        /// <summary>
        /// 前往 關卡
        /// </summary>
        public void Level_open()
        {
            SceneManager.LoadScene("關卡");
        }
        /// <summary>
        /// 前往角色選取
        /// </summary>
        public void RoleSelection()
        {
            SceneManager.LoadScene("角色選擇");
        }
        /// <summary>
        /// 關卡一
        /// </summary>
        public void Level_one()
        {
            SceneManager.LoadScene("場景 1");
            
        }
        ///// <summary>
        ///// 吉娃娃選角
        ///// </summary>
        //public void GwawaSetting()
        //{
        //    if (Gwawa != null)
        //    {
        //        //menugwawaa.StartGwawaM();
        //        Animator animator = Gwawa.GetComponent<Animator>();
        //        if (animator != null)
        //        {
        //            bool isOpen = animator.GetBool("Touch");
        //            animator.SetBool("Touch", !isOpen);
        //        }
        //    }
        //}
        ///// <summary>
        ///// 醜鵝選角
        ///// </summary>
        //public void GooseSetting()
        //{
        //    if (Goose != null)
        //    {
        //        Animator animator = Goose.GetComponent<Animator>();
        //        if (animator != null)
        //        {
        //            bool isOpen = animator.GetBool("Touch");
        //            animator.SetBool("Touch", !isOpen);
        //        }
        //    }
        //}
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
        /// <summary>
        /// 音效
        /// </summary>
        /// <param name="volume"></param>
        public void SetVolune(float volume)
        {
            audioMixer.SetFloat("MasterVolume", volume);
        }
    }
}