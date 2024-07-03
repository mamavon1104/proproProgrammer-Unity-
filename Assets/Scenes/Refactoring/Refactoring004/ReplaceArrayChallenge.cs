using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ReplaceArrayChallenge : MonoBehaviour
{
    [SerializeField] EnemyPrefabs m_enemyPrefabs; //���΂ɔ������R�[�h
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

[Serializable] //�����t���邱�Ƃɂ���ăN���X��Inspector����\��������B�����o�ϐ��Ɂ`Field�Ƃ��Ă��̂̓t�B�[���h��ɏ������炩...�H
public class EnemyPrefabs : IEnumerable<GameObject> // interface�̈� GetEnumerator�Ƃ��Ȃ��Ă͂Ȃ�Ȃ��B
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

//Serialize���͕ʂ̈ʒu�ɂ���f�[�^�𒼗�(�I�̑���)�݂����ɕ��ׂ�A��������ƃf�[�^�����������o������A
//Inspector���炾������ł���(scene�̏��Ƀf�[�^�������o����)���������Ƃ��o����B
