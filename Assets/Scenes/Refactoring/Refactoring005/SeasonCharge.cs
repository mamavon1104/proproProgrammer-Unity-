using System;
using UnityEngine;

public class SeasonCharge : MonoBehaviour
{
    private int month;
    readonly int summerStartMonth = 6;
    readonly int summerEndMonth = 8;

    readonly int winterStartMonth = 11;
    readonly int winterEndMonth = 2;

    [SerializeField] int quantity;
    [SerializeField] float winterRate;
    [SerializeField] int winterServiceCharge;
    [SerializeField] float summerRate;

    // Start is called before the first frame update
    void Start()
    {
        DateTime currentDate = DateTime.Now;
        month = currentDate.Month;
        Debug.Log("¡‚Í" + month + "ŒŽ");
        int charge;
        if (IsSummerNow())
        {
            charge = Mathf.CeilToInt(quantity * summerRate);
        }
        else if (IsWinterNow())
        {
            charge = Mathf.CeilToInt(quantity * winterRate) + winterServiceCharge;
        }
        else
            charge = quantity;

        Debug.Log("’l’i‚Í" + charge + "ŒŽ");
    }

    private bool IsWinterNow()
    {
        return month >= winterStartMonth || month <= winterEndMonth;
    }
    private bool IsSummerNow()
    {
        return month >= summerStartMonth && month <= summerEndMonth;
    }
}

public class MoveCharacter : MonoBehaviour
{
    [SerializeField] float speed = 30f;
    [SerializeField] Rigidbody _rig = default;

    [SerializeField] float inputThreshold = 0.01f;

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        if (IsMovementAllowed(moveX) || IsMovementAllowed(moveZ))
        {
            _rig.AddForce(new Vector3(moveX, 0, moveZ) * speed * Time.deltaTime, ForceMode.VelocityChange);
        }
        else
            _rig.velocity = new Vector3(0, _rig.velocity.y, 0);
    }

    private bool IsMovementAllowed(float moveVec)
    {
        return moveVec >= inputThreshold || moveVec <= -inputThreshold;
    }
}
