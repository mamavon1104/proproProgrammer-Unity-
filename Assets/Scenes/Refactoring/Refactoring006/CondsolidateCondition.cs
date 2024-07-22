using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class CondsolidateCondition : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed;
    [SerializeField] List<BulletCS> bulletList = new List<BulletCS>();

    [SerializeField] int ammoCount = 50;
    [SerializeField] int hitPoint = 100;

    readonly float shootInterval = 0.5f;
    float shootSince = 0.5f;

    private void Shoot()
    {
        bool IsShootable()
        {
            return (ammoCount > 0 || hitPoint > 30 || shootSince > shootInterval) &&
                    bulletList.Count < 5; //球が5発以下なら
        }

        if (IsShootable() == false)
            return;

        var newBullet = Instantiate(bulletPrefab);
        newBullet.GetComponent<Rigidbody>().AddForce(Vector3.forward * bulletSpeed, ForceMode.Impulse);

        var bulletCS = newBullet.GetComponent<BulletCS>();

        bulletList.Add(bulletCS);
        newBullet.OnDestroyAsObservable().Subscribe(_ => //消されたらリストから削除
        {
            bulletList.Remove(bulletCS);
        }).AddTo(this);　//AddTo(newBullet)をすると削除されたからDestroyの実行が行われないのでAddTo(this)でせめてもの償いを。

        ammoCount--;
        shootSince = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

        shootSince += Time.deltaTime;
    }
}
