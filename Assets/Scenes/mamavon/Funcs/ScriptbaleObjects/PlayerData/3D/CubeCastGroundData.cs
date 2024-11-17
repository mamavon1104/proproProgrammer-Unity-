using UnityEngine;

namespace Mamavon.Data
{
    [CreateAssetMenu(fileName = "BoxCastGroundData", menuName = "Mamavon Packs/ScriptableObject/Object Scripts/BoxCastGroundData")]
    public class CubeCastGroundData : Player3DGroundData
    {
        [SerializeField] private Vector3 halfScale;
        public Vector3 Scale
        {
            set
            {
                halfScale = new Vector3(value.x / 2 - size_margine,
                                        value.y / 2 - size_margine,
                                        value.z / 2 - size_margine);
            }
        }

        /// <summary>
        /// �v���C���[�̃I�u�W�F�N�g���n�ʂɐڂ��Ă��邩�ǂ������`�F�b�N���܂��B
        /// Box Cast���g�p���Ă��܂�
        /// </summary>
        /// <param name="obj">�`�F�b�N����Transform�I�u�W�F�N�g</param>
        /// <param name="scripObj">BoxCastGroundData�I�u�W�F�N�g</param>
        /// <param name="hit">�n�ʃ`�F�b�N�̌��ʂƂ��Ă�RaycastHit�I�u�W�F�N�g</param>
        /// <returns>�I�u�W�F�N�g���n�ʂɐڂ��Ă���ꍇ��true�A����ȊO�̏ꍇ��false</returns>
        public override bool CheckGround(Transform obj, out RaycastHit hit)
        {
            // BoxCast���g�p���Ēn�ʃ`�F�b�N�����s���܂��B
            bool isGrounded = Physics.BoxCast(
                obj.position,                                           // Obj�̈ʒu
                halfScale,                                            // �{�b�N�X�̔����̃T�C�Y
                Vector3.down,                                           // BoxCast�̕����i�������j
                out hit,                                                // �Փˏ����󂯎�邽�߂�RaycastHit
                obj.rotation,                                           // �{�b�N�X�̉�]
                base.length,                                        // BoxCast�̍ő勗��
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

            Vector3 endPosition = obj.position + Vector3.down * base.length;

            // �I���ʒu�̃L���[�u��`��
            Gizmos.DrawCube(endPosition, halfScale * 2);
        }
    }
}