using UnityEngine;
using UnityEngine.UI;

namespace WEI
{
    public class DialogBox : MonoBehaviour
    {
        public Transform Box;
        public CanvasGroup Background;
        /// <summary>
        /// ���a�I��QuitGame
        /// </summary>
        private void OnEnable()
        {
            //�ɶ����m��0
            Background.alpha = 0;
            //�z���I��(�z����,�ɶ�)
            Background.LeanAlpha(1, 0.5f);
            //�I����l��m
            Box.localPosition = new Vector2(0, -Screen.height);
            //�I���ʵe
            Box.LeanMoveLocalY(0, .5f).setEaseInOutExpo().delay = .1f;
        }
        /// <summary>
        /// ���a����QuitGame
        /// </summary>
        private void CloseDialog()
        {
            //�z���I������(�z����,�ɶ�)
            Background.LeanAlpha(0, .5f);
            Box.LeanMoveLocalY(-Screen.height, .5f).setEaseInExpo();
        }
    }
}
