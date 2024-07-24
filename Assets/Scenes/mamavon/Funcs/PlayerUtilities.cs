using UnityEngine;
namespace Mamavon.Funcs
{
    //Player Utilities�@�v���C���[���ȒP�ɓ�����悤�Ɋ�b�I�ȓ����Z�ߏグ�܂���[

    public static class PlayerCheckGroundClass
    {
        /// <summary>
        /// �v���C���[�̃I�u�W�F�N�g���n�ʂɐڂ��Ă��邩�ǂ������`�F�b�N���܂��B
        /// Sphere Cast���g�p���Ă��܂�
        /// </summary>
        /// <param name="obj">�`�F�b�N����Transform�I�u�W�F�N�g</param>
        /// <param name="groundCheckOffsetY">�n�ʃ`�F�b�N��Y�����I�t�Z�b�g</param>
        /// <param name="groundCheckRadius">�n�ʃ`�F�b�N�̋��̔��a</param>
        /// <param name="groundCheckDistance">�n�ʃ`�F�b�N�̍ő勗��</param>
        /// <param name="groundLayers">�n�ʂƂ��Ĉ������C���[�̃}�X�N</param>
        /// <param name="hit">�n�ʃ`�F�b�N�̌��ʂƂ��Ă�RaycastHit�I�u�W�F�N�g</param>
        /// <param name="isDraw">�f�o�b�O�\�����s�����ǂ���</param>
        /// <returns>�I�u�W�F�N�g���n�ʂɐڂ��Ă���ꍇ��true�A����ȊO�̏ꍇ��false</returns>
        public static bool CheckGroundSphere(this Transform obj, SphereColliderClass scripObj, out RaycastHit hit, bool isDraw = false)
        {
            // SphereCast���g�p���Ēn�ʃ`�F�b�N�����s���܂��B
            bool isGrounded = Physics.SphereCast(
                obj.position,                                           // Obj�̕���
                scripObj.radius - SphereColliderClass.RADIUS_TOLERANCE, // SphereCast�̔��a - �ق�̂�����Ƃ���
                Vector3.down,                                           // SphereCast�̕����i�������j
                out hit,                                                // �Փˏ����󂯎�邽�߂�RaycastHit
                scripObj.length,                                        // SphereCast�̍ő勗��
                scripObj.groundLayer,                                   // �Փ˂����o���郌�C���[�}�X�N
                QueryTriggerInteraction.Ignore                          // �g���K�[�𖳎�
            );

            if (isDraw)
                Debug.DrawRay(obj.position, Vector3.down * scripObj.length, isGrounded ? Color.green : Color.red);

            return isGrounded;
        }
    }
    internal static class CameraClass
    {
        /// <summary>
        /// �J������Transform���牽���ɉ�]���Ă��邩���āA�����input���|���鎖�ňړ�������vector���Q�b�g����
        /// </summary>
        /// <param name="cameraTransform">Camera��Transform</param>
        /// <param name="inputVector"> player�̓���{vector2}</param>
        /// <returns> Vector3�ŕԂ� �����̒l��speed���|���Ă����Έړ��ł���͂����B </returns>
        public static Vector3 CalculateMovementDirection(this Transform cameraTransform, Vector2 inputVector)
        {
            return (Quaternion.AngleAxis(cameraTransform.eulerAngles.y, Vector3.up) //�J�����̉�]
                    * new Vector3(inputVector.x, 0, inputVector.y))                //player��vector
                    .normalized;                                                    //���K������
        }
    }
    internal static class TransformExtention
    {
        public static void Reset(this Transform transform)
        {
            transform.position = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }
        public static void SetPosX(this Transform transform, float x)
        {
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
        public static void SetPosY(this Transform transform, float y)
        {
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }
        public static void SetPosZ(this Transform transform, float z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, z);
        }
    }
}