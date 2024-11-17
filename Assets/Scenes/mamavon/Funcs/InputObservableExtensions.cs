using System;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Mamavon.Funcs
{
    /// <summary>
    /// IObservable<long>�𗘗p���đ΂���UniRX�𔭓����܂�
    /// </summary>
    public static class InputObservableUtility
    {
        /// <summary>
        /// ���L���ꂽUpdate��Observable�B
        /// EveryUpdate��Share���邱�ƂőS�̂Ŏg���邩��y���ėǂ��˂��Ď��炵���ł���B
        /// </summary>
        private static readonly IObservable<long> UpdateObservable = Observable.EveryUpdate().Share();

        /// <summary>
        /// �}�E�X�N���b�N�i�������u�ԁj
        /// </summary>
        /// <param name="buttonId">�}�E�X�{�^����ID�i�f�t�H���g�͍��N���b�N�j</param>
        /// <returns>�}�E�X�{�^���������ꂽ�u�Ԃ�ʒm����Observable</returns>
        public static IObservable<long> OnMouseDown(int buttonId = 0)
            => UpdateObservable.Where(_ => Input.GetMouseButtonDown(buttonId));

        /// <summary>
        /// �}�E�X�N���b�N�i�������u�ԁj
        /// </summary>
        /// <param name="buttonId">�}�E�X�{�^����ID�i�f�t�H���g�͍��N���b�N�j</param>
        /// <returns>�}�E�X�{�^���������ꂽ�u�Ԃ�ʒm����Observable</returns>
        public static IObservable<long> OnMouseUp(int buttonId = 0)
            => UpdateObservable.Where(_ => Input.GetMouseButtonUp(buttonId));

        /// <summary>
        /// �}�E�X�N���b�N�i�����Ă���ԁj
        /// </summary>
        /// <param name="buttonId">�}�E�X�{�^����ID�i�f�t�H���g�͍��N���b�N�j</param>
        /// <returns>�}�E�X�{�^����������Ă���Ԃ�ʒm����Observable</returns>
        public static IObservable<long> OnMouseHold(int buttonId = 0)
            => UpdateObservable.Where(_ => Input.GetMouseButton(buttonId));

        /// <summary>
        /// �L�[���́i�������u�ԁj
        /// </summary>
        /// <param name="key">�Ď�����L�[</param>
        /// <returns>�w�肳�ꂽ�L�[�������ꂽ�u�Ԃ�ʒm����Observable</returns>
        public static IObservable<long> OnKeyDown(KeyCode key)
            => UpdateObservable.Where(_ => Input.GetKeyDown(key));

        /// <summary>
        /// �L�[���́i�������u�ԁj
        /// </summary>
        /// <param name="key">�Ď�����L�[</param>
        /// <returns>�w�肳�ꂽ�L�[�������ꂽ�u�Ԃ�ʒm����Observable</returns>
        public static IObservable<long> OnKeyUp(KeyCode key)
            => UpdateObservable.Where(_ => Input.GetKeyUp(key));

        /// <summary>
        /// �L�[���́i�����Ă���ԁj
        /// </summary>
        /// <param name="key">�Ď�����L�[</param>
        /// <returns>�w�肳�ꂽ�L�[��������Ă���Ԃ�ʒm����Observable</returns>
        public static IObservable<long> OnKeyHold(KeyCode key)
            => UpdateObservable.Where(_ => Input.GetKey(key));

        /// <summary>
        /// �C�ӂ̃L�[���́i�������u�ԁj
        /// </summary>
        /// <returns>�C�ӂ̃L�[�������ꂽ�u�Ԃ�ʒm����Observable</returns>
        public static IObservable<KeyCode> OnAnyKeyDown()
            => UpdateObservable
                .Where(_ => Input.anyKeyDown)
                .SelectMany(_ => Enum.GetValues(typeof(KeyCode)).Cast<KeyCode>())
                .Where(key => Input.GetKeyDown(key));

        /// <summary>
        /// �C�ӂ̃L�[���́i�������u�ԁj
        /// </summary>
        /// <returns>�C�ӂ̃L�[�������ꂽ�u�Ԃ�ʒm����Observable</returns>
        public static IObservable<KeyCode> OnAnyKeyUp()
            => UpdateObservable
                .SelectMany(_ => Enum.GetValues(typeof(KeyCode)).Cast<KeyCode>())
                .Where(key => Input.GetKeyUp(key));

        /// <summary>
        /// �C�ӂ̃L�[���́i�����Ă���ԁj
        /// </summary>
        /// <returns>�C�ӂ̃L�[��������Ă���Ԃ�ʒm����Observable</returns>
        public static IObservable<KeyCode> OnAnyKeyHold()
            => UpdateObservable
                .Where(_ => Input.anyKey)
                .SelectMany(_ => Enum.GetValues(typeof(KeyCode)).Cast<KeyCode>())
                .Where(key => Input.GetKey(key));

        /// <summary>
        /// �{�^�����́i�������u�ԁj
        /// </summary>
        /// <param name="buttonName">�Ď�����{�^���̖��O</param>
        /// <returns>�w�肳�ꂽ�{�^���������ꂽ�u�Ԃ�ʒm����Observable</returns>
        public static IObservable<long> OnButtonDown(string buttonName)
            => UpdateObservable.Where(_ => Input.GetButtonDown(buttonName));

        /// <summary>
        /// �{�^�����́i�������u�ԁj
        /// </summary>
        /// <param name="buttonName">�Ď�����{�^���̖��O</param>
        /// <returns>�w�肳�ꂽ�{�^���������ꂽ�u�Ԃ�ʒm����Observable</returns>
        public static IObservable<long> OnButtonUp(string buttonName)
            => UpdateObservable.Where(_ => Input.GetButtonUp(buttonName));

        /// <summary>
        /// �{�^�����́i�����Ă���ԁj
        /// </summary>
        /// <param name="buttonName">�Ď�����{�^���̖��O</param>
        /// <returns>�w�肳�ꂽ�{�^����������Ă���Ԃ�ʒm����Observable</returns>
        public static IObservable<long> OnButtonHold(string buttonName)
            => UpdateObservable.Where(_ => Input.GetButton(buttonName));

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="axisName">�Ď����鎲�̖��O</param>
        /// <returns>�w�肳�ꂽ���̓��͒l��ʒm����Observable</returns>
        public static IObservable<float> OnAxisInput(string axisName)
            => UpdateObservable.Select(_ => Input.GetAxis(axisName));
    }
}