using Mamavon.Funcs;
using UnityEngine;

//この考え方は慣れないとできねえけど出来たら最強じゃんね;
//  ↑コードちょっと長くなりそうだけど何処に何を書いているのかが隔離されるから分かりやすさという面ではある程度あるかも。
interface ISeason
{
    int GetCost(int baseCost);
}
class Spring : ISeason
{
    private readonly static float springIncrease = 1.1f;
    public int GetCost(int baseCost) => (int)(baseCost * springIncrease);
}
class Summer : ISeason
{
    public int GetCost(int baseCost) => baseCost;
}
class Autumn : ISeason
{
    private readonly static int autumnDiscount = 20;
    public int GetCost(int baseCost) => baseCost - autumnDiscount;
}
class Winter : ISeason
{
    private readonly static int winterDiscount = 10;
    public int GetCost(int baseCost) => baseCost - winterDiscount;
}
public class SeasonPolymorphism : MonoBehaviour
{
    ISeason nowSeason = new Spring(); //ベースクラスを継承した奴の為にこいつで型宣言するっしょ；
    int GetCostSeason(int baseCost)
    {
        return nowSeason.GetCost(baseCost)
                        .Debuglog();
    }
}
