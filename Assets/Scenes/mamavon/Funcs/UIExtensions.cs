using System;
using UniRx;
using UnityEngine.UI;
namespace Mamavon.Funcs
{
    public static class UIExtensions
    {
        /// <summary>
        /// �{�^���̊g�����\�b�h�ŁA�A�Ŗh�~�̏������͂₭�����Ⴂ�܂��B
        /// </summary>
        /// <param name="debounceTime">�A�Ŗh�~�̎��ԁA�f�t�H���g0.1�b</param>
        /// <returns>IObservable<Unit>�AThrottleFirst�܂ł̏���</returns>
        public static IObservable<Unit> OnClickThrottle(this Button button, float debounceTime = 0.1f)
        {
            return button.OnClickAsObservable()
                .ThrottleFirst(TimeSpan.FromSeconds(debounceTime));
        }
    }
}
