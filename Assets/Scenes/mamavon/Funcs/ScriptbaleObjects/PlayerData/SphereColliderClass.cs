using UnityEngine;

namespace Mamavon.Funcs
{

    [CreateAssetMenu(fileName = "SphereColliderScriptsObjs", menuName = "Mamavon Packs/Object Scripts/Sphere Collider ScriptObjs")]
    public class SphereColliderClass : ScriptableObject
    {
        [Header("SphereCollider�̔��a")] public float radius = 0.5f;
        [Header("���ɐL�΂����߂̋���")] public float length = 0.1f;
        [Header("�n�ʂ̃��C���[��ݒ�")] public LayerMask groundLayer = 1 << 0;
        [Header("�`�悷�鎞�Ԃ�������")] public byte drawSeconds = 2;
        [Header("�`�悷�鎞�͂����true��")] public bool isDraw = false;

        /// <summary>
        /// Radius���҂�����(transform.size / 2)���Ɖ��̂����ɍs���Ȃ��̂ł���ň����Ă����Ȃ��Ƃ����Ȃ��B
        /// </summary>
        public const float RADIUS_TOLERANCE = 0.0001f;
    }
}
