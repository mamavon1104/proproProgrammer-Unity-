using Mamavon.Funcs;
using UnityEngine;

//���̍l�����͊���Ȃ��Ƃł��˂����Ǐo������ŋ�������;
//  ���R�[�h������ƒ����Ȃ肻�������ǉ����ɉ��������Ă���̂����u������邩�番����₷���Ƃ����ʂł͂�����x���邩���B
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
    ISeason nowSeason = new Spring(); //�x�[�X�N���X���p�������z�ׂ̈ɂ����Ō^�錾���������G
    int GetCostSeason(int baseCost)
    {
        return nowSeason.GetCost(baseCost)
                        .Debuglog();
    }
}
