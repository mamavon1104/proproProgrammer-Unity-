#if UNITY_EDITOR
using System;
namespace Mamavon.Funcs
{
    public static class TupleExtensions
    {
        static void TestCode()
        {
            (int min, int big) valueInt = (20, 10).SortMinMaxTaple();
            valueInt = valueInt.SwapTuple();
            (float min, float big) valueFloat = (MathF.PI, MathF.E).SortMinMaxTaple();
            (valueFloat = valueFloat.SwapTuple()).Debuglog("float�^�^�v��");
        }
        /// <summary>
        /// �^�v���^���ŏ��ƍő�Ń\�[�g����g�����\�b�h
        /// (3,2)�œn������(2,3)�ŋA���Ă���
        /// </summary>
        /// <typeparam name="T">�l�^</typeparam>
        /// <returns>(�ŏ��l�A�ő�l)</returns>
        public static (T, T) SortMinMaxTaple<T>(this (T, T) getTuple) where T : IComparable<T>
        {
            T a = getTuple.Item1;
            T b = getTuple.Item2;

            if (a.CompareTo(b) <= 0)
            {
                return (a, b); // a �����������������ꍇ
            }
            else
            {
                return (b, a); // b ���������ꍇ
            }
        }
        public static (T, T) SwapTuple<T>(this (T, T) getTuple) where T : IComparable<T>
        {
            return (getTuple.Item2, getTuple.Item1);
        }
    }
}
#endif