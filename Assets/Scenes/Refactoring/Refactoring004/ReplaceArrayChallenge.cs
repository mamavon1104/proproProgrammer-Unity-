using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ReplaceArrayChallenge : MonoBehaviour
{
    [SerializeField] EnemyPrefabs m_enemyPrefabs; //流石に美しいコード
    private void Start()
    {
        var normalEnemy = Instantiate(m_enemyPrefabs.normalEnemyPrefab);
        var highSpeedEnemy = Instantiate(m_enemyPrefabs.highSpeedEnemyPrefab);
        var bossEnemy = Instantiate(m_enemyPrefabs.bossEnemyPrefab);
        
        foreach (var enemyPrefab in m_enemyPrefabs)
        {
            Debug.Log(enemyPrefab.name);
        }
    }
}

[Serializable] //これを付けることによってクラスをInspectorから表示させる。メンバ変数に～Fieldとつけてたのはフィールド上に書くからか...？
public class EnemyPrefabs : IEnumerable<GameObject> // interfaceの為 GetEnumeratorとしなくてはならない。
{
    public GameObject normalEnemyPrefab;
    public GameObject highSpeedEnemyPrefab;
    public GameObject bossEnemyPrefab;

    public IEnumerator<GameObject> GetEnumerator()
    {
        yield return normalEnemyPrefab;
        yield return highSpeedEnemyPrefab;
        yield return bossEnemyPrefab;
    }
    IEnumerator IEnumerable.GetEnumerator()
    { 
        return GetEnumerator(); 
    }
}

//Serialize化は別の位置にあるデータを直列(蜂の巣箱)みたいに並べる、そうするとデータを書きだし出来たり、
//Inspectorからだしたりできて(sceneの所にデータを書き出して)動かすことが出来る。
