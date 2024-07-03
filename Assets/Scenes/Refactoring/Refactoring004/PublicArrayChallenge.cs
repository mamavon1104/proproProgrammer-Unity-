using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class PublicArrayChallenge : MonoBehaviour
{
    [SerializeField] private List<string> m_playerNames;
    public ReadOnlyCollection<string> getNames { get => m_playerNames.AsReadOnly(); } //Ç±Ç¢Ç¬ÇÃÇ®Ç©Ç∞Ç≈ì«Ç›éÊÇËêÍóp

    public void AddValues(string str)
    {
        Debug.Log(str);
        m_playerNames.Add(str);
    }
    public void RemoveValues(string str)
    {
        Debug.Log(str);
        m_playerNames.Remove(str);
    }
}
