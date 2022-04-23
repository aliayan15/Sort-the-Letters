using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadingNextScene : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
