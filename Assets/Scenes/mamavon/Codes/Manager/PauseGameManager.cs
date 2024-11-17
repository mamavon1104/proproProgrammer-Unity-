// https://gomafrontier.com/unity/3720�@�R�s�y��

using Mamavon.Funcs;
using System;
using UniRx;
using UnityEngine;

namespace Mamavon.Useful
{
    /// <summary>
    /// �Q�[�����|�[�Y���Ă��邩�ǂ����������Őݒ肵�܂��B
    /// </summary>
    public class PauseGameManager : SingletonMonoBehaviour<PauseGameManager>
    {
        private Subject<Unit> pauseSubject = new Subject<Unit>();
        private Subject<Unit> resumeSubject = new Subject<Unit>();

        [Header("�|�[�Y���Ă��܂����H"), SerializeField] private bool isPaused = false;

        /// <summary>
        /// �|�[�Y�����ǂ���
        /// </summary>
        public bool IsPaused => isPaused;
        public IObservable<Unit> OnPaused
        {
            get { return pauseSubject; }
        }

        public IObservable<Unit> OnResumed
        {
            get { return resumeSubject; }
        }

        public void ChangePauseState()
        {
            if (!isPaused)
                Pause();
            else
                Resume();
        }

        [ContextMenu("�Q�[���|�[�Y")]
        private void Pause()
        {
            isPaused = true;
            isPaused.Debuglog("�|�[�Y���ł� :");
            pauseSubject.OnNext(Unit.Default);
        }

        [ContextMenu("�Q�[���ĊJ")]
        private void Resume()
        {
            isPaused = false;
            isPaused.Debuglog("�|�[�Y���������܂����B :");
            resumeSubject.OnNext(Unit.Default);
        }
    }
}