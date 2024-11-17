using UnityEngine;

namespace Mamavon.Data
{
    [CreateAssetMenu(fileName = "CircleCollider2DScriptsObjs", menuName = "Mamavon Packs/ScriptableObject/Object Scripts/Circle Collider 2D ScriptObjs")]
    public class CircleCastGroundData : Player2DGroundData
    {
        [Header("CircleCollider2D�̔��a")]
        [SerializeField] private float r = 0.5f;
        public float Radius
        {
            set { r = value - size_margine; }
        }

        public override bool CheckGround2D(Transform obj, out RaycastHit2D hit)
        {
            // CircleCast2D���g�p���Ēn�ʃ`�F�b�N�����s���܂��B
            hit = Physics2D.CircleCast(
                obj.position,                                           // Obj�̈ʒu
                r,                                  // CircleCast�̔��a - �ق�̂�����Ƃ���
                Vector2.down,                                           // CircleCast�̕����i�������j
                base.length,                                            // CircleCast�̍ő勗��
                base.groundLayer                                        // �Փ˂����o���郌�C���[�}�X�N
            );

            return hit.collider != null;
        }

        public override bool CheckSideWall2D(Transform obj, out RaycastHit2D hit, Vector2 direction)
        {
            throw new System.NotImplementedException();
        }

        public override void DrawGroundCheckGizmo2D(Transform obj, bool isGrounded)
        {
            if (!base.isDraw)
                return;

            Color gizmoColor = isGrounded ? Color.green : Color.red;
            gizmoColor.a = base.gizmoAlpha; // �����x��ݒ�

            Gizmos.color = gizmoColor;

            Vector2 endPosition = (Vector2)obj.position + Vector2.down * base.length;

            // 2D�~��`�悷�邽�߂ɁAXY���ʏ�ɉ~��`��
            Vector3 center = new Vector3(endPosition.x, endPosition.y, 0);
            Vector3 size = new Vector3(r * 2, r * 2, 0);
            Gizmos.DrawWireSphere(center, r);
        }

        public override void DrawSideWallCheckGizmo2D(Transform obj, bool isWallDetected, Vector2 direction)
        {
            throw new System.NotImplementedException();
        }
    }
}