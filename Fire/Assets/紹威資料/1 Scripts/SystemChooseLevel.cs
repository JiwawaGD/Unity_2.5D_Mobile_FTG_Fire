using UnityEngine;
using UnityEngine.SceneManagement;

public class SystemChooseLevel : MonoBehaviour
{    
    [SerializeField][Header("¿ï¾Ü³õ´º")] int sceneBuildIndex;

    public void ChooseLevel()
    {
        scr_GameManager.ChooseLevel(sceneBuildIndex);
    }
    public void LevelOne()
    {
        scr_GameManager.ChooseLevel(6);
    }
    public void LevelTwo()
    {
        scr_GameManager.ChooseLevel(8);
    }
    public void LevelThree()
    {
        scr_GameManager.ChooseLevel(10);
    }
}


