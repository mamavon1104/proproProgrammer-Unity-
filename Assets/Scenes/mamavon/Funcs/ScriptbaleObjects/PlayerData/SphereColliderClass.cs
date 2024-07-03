using UnityEngine;

namespace Mamavon.Funcs
{

    [CreateAssetMenu(fileName = "SphereColliderScriptsObjs", menuName = "Mamavon Packs/Object Scripts/Sphere Collider ScriptObjs")]
    public class SphereColliderClass : ScriptableObject
    {
        [Header("SphereColliderの半径")] public float radius = 0.5f;
        [Header("下に伸ばすための距離")] public float length = 0.1f;
        [Header("地面のレイヤーを設定")] public LayerMask groundLayer = 1 << 0;
        [Header("描画する時間をここに")] public byte drawSeconds = 2;
        [Header("描画する時はこれをtrueに")] public bool isDraw = false;

        /// <summary>
        /// Radiusがぴったり(transform.size / 2)だと何故か上手に行かないのでこれで引いてあげないといけない。
        /// </summary>
        public const float RADIUS_TOLERANCE = 0.0001f;
    }
}
