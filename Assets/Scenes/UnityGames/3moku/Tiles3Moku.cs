using Mamavon.Funcs;
using moku3;
using UnityEngine;


public class Tiles3Moku : MonoBehaviour
{
    Manager3Mokunarabe manager;
    private (int x, int y) num;

    public (int x, int y) SetValues
    {
        set
        {
            "aca".Debuglog();
            num = value;
        }
    }
    public Manager3Mokunarabe SetManager
    {
        set => manager = value;
    }

    public void OnClickObj()
    {
        "aa".Debuglog();
        manager.SetPieces(num);
    }
}
