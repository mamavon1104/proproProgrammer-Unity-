using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
public class MoleCreater : MonoBehaviour
{
    [SerializeField] GameObject m_molePrefab;
    [SerializeField] Transform[] m_tiles;

    /// <summary>
    /// スポーンとムーブまでの時間を同じにしちゃって一つの変数で速度が速くなるように演出
    /// </summary>
    public static float moleSpawnAndMoveTime = 1.2f;
    public static readonly float moleYPos = -7;

    private async void Start()
    {
        WhackAMoleValueManager.time.Subscribe(x =>
        {
            moleSpawnAndMoveTime = x switch //UniRXのTimeを取得して、3秒以下だったら～等を羅列し、スポーン時間を設定した、
            {
                <= 0 => 1000000, //0秒以下になったら100万秒待たす(絶対生成)しない
                < 3 => 0.2f,
                < 5 => 0.3f,
                < 10 => 0.4f,
                < 15 => 0.6f,
                < 20 => 0.8f,
                _ => moleSpawnAndMoveTime //別の場合は不変ですよ
            };
        });

        while (true)
        {
            await UniTask.WaitForSeconds(moleSpawnAndMoveTime);
            var tile = m_tiles[Random.Range(0, m_tiles.Length)];
            Vector3 moleSpawnPosition = new Vector3(tile.position.x, moleYPos, tile.position.z);
            var mole = Instantiate(m_molePrefab, moleSpawnPosition, Quaternion.identity);
        }
    }

    [ContextMenu("タイルを取得")]
    private void GetTiles() //子オブジェクトのタイルたちを取得 4*4も自動で収納されるのでこれでokかも
    {
        Transform myT = transform;
        m_tiles = new Transform[myT.childCount];
        for (int i = 0; i < myT.childCount; i++)
        {
            m_tiles[i] = myT.GetChild(i);
        }
    }

    #region 孫にモグラは設計的に悪い感じがしたので没、量産する形で。
    //[SerializeField] Transform[] m_moles;
    //[ContextMenu("タイルの子、モグラを取得")]
    //private void GetMoles()
    //{
    //    Transform myT = transform;
    //    for (int i = 0; i < m_tiles.Count(); i++)
    //    {
    //        foreach (Transform childT in m_tiles[i])
    //        {
    //            if (childT.TryGetComponent<MoleController>(out _))
    //            {
    //                m_moles[i] = childT;
    //                break;
    //            }
    //        }
    //    }
    //}
    #endregion
}
