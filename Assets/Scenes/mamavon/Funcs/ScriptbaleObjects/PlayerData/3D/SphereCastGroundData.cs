using UnityEngine;

namespace Mamavon.Data
{

    [CreateAssetMenu(fileName = "SphereColliderScriptsObjs", menuName = "Mamavon Packs/ScriptableObject/Object Scripts/Sphere Collider ScriptObjs")]
    public class SphereCastGroundData : Player3DGroundData
    {
        [Header("SphereCollider�̔��a")] private float radius = 0.5f;

        public override bool CheckGround(Transform obj, out RaycastHit hit)
        {
            // SphereCast���g�p���Ēn�ʃ`�F�b�N�����s���܂��B
            bool isGrounded = Physics.SphereCast(
                obj.position,                                           // Obj�̕���
                radius - size_margine, // SphereCast�̔��a - �ق�̂�����Ƃ���
                Vector3.down,                                           // SphereCast�̕����i�������j
                out hit,                                                // �Փˏ����󂯎�邽�߂�RaycastHit
                base.length,                                        // SphereCast�̍ő勗��
                base.groundLayer,                                   // �Փ˂����o���郌�C���[�}�X�N
                QueryTriggerInteraction.Ignore                          // �g���K�[�𖳎�
            );

            return isGrounded;
        }
        public override void DrawGroundCheckGizmo(Transform obj, bool isGrounded)
        {
            if (!base.isDraw)
                return;

            Color gizmoColor = isGrounded ? Color.green : Color.red;
            gizmoColor.a = base.gizmoAlpha; // �����x��ݒ�

            Gizmos.color = gizmoColor;

            float r = radius - size_margine;

            Vector3 endPosition = obj.position + Vector3.down * base.length;
            Gizmos.DrawSphere(endPosition, r);
        }
    }
}
