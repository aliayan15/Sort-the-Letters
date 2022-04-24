using UnityEngine;

namespace Managers
{
    // Write special codes for level here
    public class LevelInfo : MonoBehaviour
    {
        private void Start()
        {
            GameManager.Instance.SetGame();
        }
    }
}
