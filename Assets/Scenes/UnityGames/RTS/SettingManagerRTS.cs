using UnityEngine;

public class SettingManagerRTS : MonoBehaviour
{
    [ContextMenu("SetAllColliders")]
    void SetAllColliders()
    {
        var entities = FindObjectsByType<BattleSystemRTS>(FindObjectsSortMode.None);
        foreach (var entity in entities)
        {
            entity.SetChildCollider();
        }
    }
}
