using Cysharp.Threading.Tasks;
using UnityEngine;

public class BulletCS : MonoBehaviour
{
    async void Start()
    {
        await UniTask.WaitForSeconds(2f);
        Destroy(gameObject);
    }
    private void OnCollisionEnter()
    {
        Destroy(gameObject);
    }
}
