using UnityEngine;

namespace Mamavon.Funcs
{
    public enum TextColor
    {
        White,
        Black,
        Red,
        Blue,
        Green,
        Yellow,
        Magenta,
        Cyan,
    }
    internal static class DebugExtensions
    {
        /// <summary>
        /// TextColor�ɍ������F��Ԃ��܂��B
        /// </summary>
        /// <param name="color">TextColor</param>
        /// <returns>Color�^�̐F��Ԃ�</returns>
        public static Color ConvertEnumToColor(TextColor color)
        {
            string colorCode = ConvertEnumToColorCode(color);
            if (ColorUtility.TryParseHtmlString(colorCode, out Color parsedColor))
            {
                return parsedColor.Debuglog();
            }
            return Color.white; // �f�t�H���g�̐F�Ƃ��Ĕ���Ԃ�
        }
        /// <summary>
        /// Enum�̖��O���������ɕϊ����ĐF�Ƃ��Ďg�p
        /// </summary>
        private static string ConvertEnumToColorCode(TextColor color)
        {
            return color.ToString().ToLower();
        }
        private static string GetColorString(TextColor color)
        {
            return color == TextColor.White ? "" : $"<color={ConvertEnumToColorCode(color)}>";
        }

        /// <summary>
        /// Debug.Log���s���܂��B
        /// �����Ȃ��̏ꍇ�̓N���X����\�����܂���B
        /// </summary>
        public static T Debuglog<T>(this T value, TextColor color = TextColor.White)
        {
            Debug.Log($"{GetColorString(color)} {typeof(T).Name}: {value} </color>");
            return value;
        }

        /// <summary>
        /// Debug.LogWarning���s���܂��B
        /// �����Ȃ��̏ꍇ�̓N���X����\�����܂���B
        /// </summary>
        public static T DebuglogWarning<T>(this T value, TextColor color = TextColor.Yellow)
        {
            Debug.LogWarning($"{GetColorString(color)} {typeof(T).Name}: {value} </color>");
            return value;
        }

        /// <summary>
        /// Debug.LogError���s���܂��B
        /// �����Ȃ��̏ꍇ�̓N���X����\�����܂���B
        /// </summary>
        public static T DebuglogError<T>(this T value, TextColor color = TextColor.Red)
        {
            Debug.LogError($"{GetColorString(color)} {typeof(T).Name}: {value} </color>");
            return value;
        }
    }
}