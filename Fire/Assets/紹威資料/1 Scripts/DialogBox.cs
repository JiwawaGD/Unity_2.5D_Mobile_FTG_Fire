using UnityEngine;
using UnityEngine.UI;

namespace WEI
{
    public class DialogBox : MonoBehaviour
    {
        public Transform Box;
        public CanvasGroup Background;
        /// <summary>
        /// 玩家點擊QuitGame
        /// </summary>
        private void OnEnable()
        {
            //時間重置為0
            Background.alpha = 0;
            //透明背景(透明度,時間)
            Background.LeanAlpha(1, 0.5f);
            //背景初始位置
            Box.localPosition = new Vector2(0, -Screen.height);
            //背景動畫
            Box.LeanMoveLocalY(0, .5f).setEaseInOutExpo().delay = .1f;
        }
        /// <summary>
        /// 玩家關閉QuitGame
        /// </summary>
        private void CloseDialog()
        {
            //透明背景關閉(透明度,時間)
            Background.LeanAlpha(0, .5f);
            Box.LeanMoveLocalY(-Screen.height, .5f).setEaseInExpo();
        }
    }
}
