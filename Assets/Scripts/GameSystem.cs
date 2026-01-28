using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void Retry()
    {
        SceneManager.LoadScene("MainScene");
    }
}
