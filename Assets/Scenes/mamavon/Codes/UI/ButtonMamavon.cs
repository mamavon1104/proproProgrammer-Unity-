using Mamavon.Funcs;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Mamavon.Code
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Button))]
    public class ButtonMamavon : MonoBehaviour
    {
        /// <summary>
        /// �ݒ肳��Ă�unityEvents;
        /// </summary>
        [Header("�{�^���C�x���g"), SerializeField] UnityEvent events;
        [Header("�{�^���A�����œ������"), SerializeField] Button button;
        [Header("�f�o�b�O���邩�ǂ���"), SerializeField] bool isDebug = false;
        private void Start()
        {
            if (button == null)
                button = GetComponent<Button>();

            button.OnClickThrottle().Subscribe(_ =>
            {
                if (isDebug)
                    events?.Debuglog().Invoke();
                else
                    events?.Invoke();
            }).AddTo(button);
        }
    }
}