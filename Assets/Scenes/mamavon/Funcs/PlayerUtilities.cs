using UnityEngine;

namespace Mamavon.Funcs
{
    //Player Utilities�@�v���C���[���ȒP�ɓ�����悤�Ɋ�b�I�ȓ����Z�ߏグ�܂���[

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
    public static class TransformExtention
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