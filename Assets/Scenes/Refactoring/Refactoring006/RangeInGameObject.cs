using Mamavon.Funcs;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Vector3 Distanceで距離を求めて～、foreachで～、Linqなど～様々なアプローチがある、LinQの方が楽そうらしい
public class RangeInGameObject : MonoBehaviour
{
    [SerializeField] private List<GameObject> enterObject = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy"))
            return;

        var otherObj = other.gameObject;

        if (!enterObject.Contains(otherObj.gameObject))
            enterObject.Add(otherObj);
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Enemy"))
            return;

        var otherObj = other.gameObject;

        if (enterObject.Contains(otherObj.gameObject))
            enterObject.Remove(other.gameObject);
    }
    [ContextMenu("テスト取得")]
    GameObject FindClosestObjectTo()
    {
        Vector3 myPos = transform.position;

        //https://00m.in/iGWFg このような処理で可能、知らなかった。
        return enterObject.OrderBy(obj => Vector3.Distance(obj.transform.position, myPos)) //objのpositionと自分のposのDistanceを昇順で取って並べる
                          .FirstOrDefault()                                                //最初の奴だけとってくるって訳よ😎
                          .Debuglog(TextColor.Red);
    }
}
