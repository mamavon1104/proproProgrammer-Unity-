using Cysharp.Threading.Tasks;
using Mamavon.Data;
using Mamavon.Funcs;
using System;
using System.Threading;
using TMPro;
using UniRx;
using UnityEngine;

namespace Mamavon.Code
{
    public enum TextDisplayMode
    {
        Normal,
        FadeIn
    }

    public class ChangeMyText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textComponent;
        [SerializeField] private TextData textData;
        [SerializeField] private TextDisplayMode displayMode = TextDisplayMode.Normal;

        [Header("�ꕶ���ꕶ���`�悷��Ƃ��̊Ԋu")]
        [SerializeField, Range(0f, 10f)] private float letterDisplayInterval = 0.1f;
        [Header("�t�F�[�h�C�����̕����Ԋu")]
        [SerializeField, Range(0f, 10f)] private float fadeInCharacterInterval = 0.05f;

        private readonly Subject<Unit> onStarted = new Subject<Unit>();
        private readonly Subject<Unit> onFinished = new Subject<Unit>();

        public IObservable<Unit> OnStarted => onStarted;
        public IObservable<Unit> OnFinished => onFinished;

        private CancellationTokenSource cancellationTokenSource;

        private void Start()
        {
            if (textComponent == null)
                textComponent = GetComponent<TextMeshProUGUI>();

            InputObservableUtility.OnKeyDown(KeyCode.Space)
                .Subscribe(_ => ChangeText().Forget())
                .AddTo(this);
        }

        [ContextMenu("�e�L�X�g�`�F���W���s")]
        public async UniTaskVoid ChangeText()
        {
            cancellationTokenSource?.Cancel();
            cancellationTokenSource = new CancellationTokenSource();

            onStarted.OnNext(Unit.Default);

            try
            {
                if (letterDisplayInterval <= 0)
                {
                    textComponent.text = textData.Text;
                }
                else
                {
                    switch (displayMode)
                    {
                        case TextDisplayMode.Normal:
                            await DisplayTextCharByCharAsync(textData.Text, letterDisplayInterval, cancellationTokenSource.Token);
                            break;
                        case TextDisplayMode.FadeIn:
                            await FadeInCharactersByCharacterAsync(textData.Text, fadeInCharacterInterval, letterDisplayInterval, cancellationTokenSource.Token);
                            break;
                    }
                }

                onFinished.OnNext(Unit.Default);
            }
            catch (OperationCanceledException)
            {
                // �L�����Z�����ꂽ�ꍇ�̏���
            }
        }

        private async UniTask DisplayTextCharByCharAsync(string text, float interval, CancellationToken cancellationToken)
        {
            textComponent.text = "";
            foreach (char character in text)
            {
                textComponent.text += character;
                await UniTask.Delay(TimeSpan.FromSeconds(interval), cancellationToken: cancellationToken);
            }
        }

        private async UniTask FadeInCharactersByCharacterAsync(string text, float characterInterval, float fadeDuration, CancellationToken cancellationToken)
        {
            textComponent.text = text;
            textComponent.ForceMeshUpdate();

            TMP_TextInfo textInfo = textComponent.textInfo;
            Color32[] newVertexColors;

            // ���ׂĂ̕����𓧖��ɂ���
            SetAllCharactersTransparent(textInfo);

            // �ꕶ�����t�F�[�h�C��
            for (int i = 0; i < textInfo.characterCount; i++)
            {
                if (!textInfo.characterInfo[i].isVisible) continue;

                int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;
                newVertexColors = textInfo.meshInfo[materialIndex].colors32;
                int vertexIndex = textInfo.characterInfo[i].vertexIndex;

                // �t�F�[�h�C��������񓯊��ŊJ�n
                FadeInCharacterAsync(newVertexColors, vertexIndex, fadeDuration, cancellationToken).Forget();

                // ���̕����̃t�F�[�h�C���J�n�܂őҋ@
                await UniTask.Delay(TimeSpan.FromSeconds(characterInterval), cancellationToken: cancellationToken);
            }

            // ���ׂĂ̕����̃t�F�[�h�C������������܂őҋ@
            await UniTask.Delay(TimeSpan.FromSeconds(fadeDuration), cancellationToken: cancellationToken);
        }

        private void SetAllCharactersTransparent(TMP_TextInfo textInfo)
        {
            for (int i = 0; i < textInfo.characterCount; i++)
            {
                if (!textInfo.characterInfo[i].isVisible) continue;

                int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;
                Color32[] vertexColors = textInfo.meshInfo[materialIndex].colors32;
                int vertexIndex = textInfo.characterInfo[i].vertexIndex;

                vertexColors[vertexIndex + 0].a = 0;
                vertexColors[vertexIndex + 1].a = 0;
                vertexColors[vertexIndex + 2].a = 0;
                vertexColors[vertexIndex + 3].a = 0;
            }
            textComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
        }

        private async UniTaskVoid FadeInCharacterAsync(Color32[] colors, int vertexIndex, float fadeDuration, CancellationToken cancellationToken)
        {
            float startTime = Time.time;
            while (Time.time - startTime < fadeDuration)
            {
                if (cancellationToken.IsCancellationRequested) return;

                float t = (Time.time - startTime) / fadeDuration;
                byte alpha = (byte)(Mathf.Lerp(0, 255, t));

                colors[vertexIndex + 0].a = alpha;
                colors[vertexIndex + 1].a = alpha;
                colors[vertexIndex + 2].a = alpha;
                colors[vertexIndex + 3].a = alpha;

                textComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

                await UniTask.Yield(cancellationToken);
            }

            // ���S�ɕs�����ɂ���
            SetCharacterOpaque(colors, vertexIndex);
            textComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
        }

        private void SetCharacterOpaque(Color32[] colors, int vertexIndex)
        {
            colors[vertexIndex + 0].a = 255;
            colors[vertexIndex + 1].a = 255;
            colors[vertexIndex + 2].a = 255;
            colors[vertexIndex + 3].a = 255;
        }

        public void StopTextDisplay()
        {
            cancellationTokenSource?.Cancel();
        }

        private void OnDisable()
        {
            StopTextDisplay();
        }
    }
}