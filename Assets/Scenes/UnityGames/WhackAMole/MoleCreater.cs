using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
public class MoleCreater : MonoBehaviour
{
    [SerializeField] GameObject m_molePrefab;
    [SerializeField] Transform[] m_tiles;

    /// <summary>
    /// �X�|�[���ƃ��[�u�܂ł̎��Ԃ𓯂��ɂ�������Ĉ�̕ϐ��ő��x�������Ȃ�悤�ɉ��o
    /// </summary>
    public static float moleSpawnAndMoveTime = 1.2f;
    public static readonly float moleYPos = -7;

    private async void Start()
    {
        WhackAMoleValueManager.time.Subscribe(x =>
        {
            moleSpawnAndMoveTime = x switch //UniRX��Time���擾���āA3�b�ȉ���������`���𗅗񂵁A�X�|�[�����Ԃ�ݒ肵���A
            {
                <= 0 => 1000000, //0�b�ȉ��ɂȂ�����100���b�҂���(��ΐ���)���Ȃ�
                < 3 => 0.2f,
                < 5 => 0.3f,
                < 10 => 0.4f,
                < 15 => 0.6f,
                < 20 => 0.8f,
                _ => moleSpawnAndMoveTime //�ʂ̏ꍇ�͕s�ςł���
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

    [ContextMenu("�^�C�����擾")]
    private void GetTiles() //�q�I�u�W�F�N�g�̃^�C���������擾 4*4�������Ŏ��[�����̂ł����ok����
    {
        Transform myT = transform;
        m_tiles = new Transform[myT.childCount];
        for (int i = 0; i < myT.childCount; i++)
        {
            m_tiles[i] = myT.GetChild(i);
        }
    }

    #region ���Ƀ��O���͐݌v�I�Ɉ��������������̂Ŗv�A�ʎY����`�ŁB
    //[SerializeField] Transform[] m_moles;
    //[ContextMenu("�^�C���̎q�A���O�����擾")]
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
