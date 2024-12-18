using Mamavon.Useful;
using UnityEngine;

namespace Mamavon.Code
{
    public class MoveScene : MonoBehaviour
    {
        [SerializeField] private SceneObject m_targetScene;
        private bool movingScene = false;

        public async void Move()
        {
            if (movingScene)
                return;

            movingScene = true;
            //awaitこれで解決できる
            await LoadSceneMamavon.Instance.LoadScene(m_targetScene);
        }
    }
}