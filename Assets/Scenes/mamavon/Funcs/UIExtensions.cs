using System;
using UniRx;
using UnityEngine.UI;
namespace Mamavon.Funcs
{
    public static class UIExtensions
    {
        /// <summary>
        /// �{�^���̊g�����\�b�h�ŁA�A�Ŗh�~�������葁�������Ⴂ�܂��B
        /// </summary>
        /// <param name="action">�����ꂽ���ɔ��΂���Action</param>
        /// <param name="throttleSeconds">�A�Ŗh�~�̎��ԁA�f�t�H���g0.1�b</param>
        /// <returns>IDisposable�AUniRX��Oncolick����Subscribe�܂�</returns>
        public static IDisposable OnClickThrottleSubscribe(this Button button, Action action, float throttleSeconds = 0.1f)
        {
            return button.OnClickAsObservable()
                .ThrottleFirst(TimeSpan.FromSeconds(throttleSeconds))
                .Subscribe(_ => action());
        }
    }
}
