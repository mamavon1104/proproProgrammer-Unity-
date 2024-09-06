using System;
using UnityEngine.Events;

namespace Mamavon.Funcs
{
    public static class ConvertExtensions
    {
        /// <summary>
        /// UnityEvent��Action�ɕϊ����܂��B
        /// </summary>
        /// <param name="unityEvent">�ϊ�����UnityEvent</param>
        /// <returns>UnityEvent�ɑ�������Action</returns>
        public static Action ConvertToAction(this UnityEvent unityEvent)
        {
            return () => unityEvent?.Invoke();
        }

        /// <summary>
        /// UnityEvent<T>��Action<T>�ɕϊ����܂��B
        /// </summary>
        /// <typeparam name="T">�p�����[�^�̌^</typeparam>
        /// <param name="unityEvent">�ϊ�����UnityEvent<T></param>
        /// <returns>UnityEvent<T>�ɑ�������Action<T></returns>
        public static Action<T> ConvertToAction<T>(this UnityEvent<T> unityEvent)
        {
            return (arg) => unityEvent?.Invoke(arg);
        }

        /// <summary>
        /// Action��UnityEvent�ɕϊ����܂��B
        /// </summary>
        /// <param name="action">�ϊ�����Action</param>
        /// <returns>Action�ɑ�������UnityEvent</returns>
        public static UnityEvent ConvertToUnityEvent(this Action action)
        {
            UnityEvent unityEvent = new UnityEvent();
            unityEvent.AddListener(() => action?.Invoke());
            return unityEvent;
        }

        /// <summary>
        /// Action<T>��UnityEvent<T>�ɕϊ����܂��B
        /// </summary>
        /// <typeparam name="T">�p�����[�^�̌^</typeparam>
        /// <param name="action">�ϊ�����Action<T></param>
        /// <returns>Action<T>�ɑ�������UnityEvent<T></returns>
        public static UnityEvent<T> ConvertToUnityEvent<T>(this Action<T> action)
        {
            UnityEvent<T> unityEvent = new UnityEvent<T>();
            unityEvent.AddListener((arg) => action?.Invoke(arg));
            return unityEvent;
        }
    }
}