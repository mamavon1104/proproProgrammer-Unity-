using Mamavon.Funcs;
using UnityEngine;
using UnityEngine.Events;

namespace Mamavon.Code
{
    public class InvokeEvents : MonoBehaviour
    {
        /// <summary>
        /// �ݒ肳��Ă�unityEvents;
        /// </summary>
        [Header("�{�^���C�x���g"), SerializeField] UnityEvent events;
        [Header("�f�o�b�O���邩�ǂ���"), SerializeField] bool isDebug = false;

        [ContextMenu("Inspector������s")]
        public void InvokeMyEvents()
        {
            if (isDebug)
                events?.Debuglog().Invoke();
            else
                events?.Invoke();
        }
    }
}