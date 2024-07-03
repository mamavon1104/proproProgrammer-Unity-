using UnityEngine;

public class TestReplaceTypeCode : MonoBehaviour
{
    private class Employee
    {
        private EmployeeType _type;

        public EmployeeType Type => _type;

        private int _monthlySalary;
        private int _commission;
        private int _bonus;

        internal enum EmployeeType
        {
            Engineer,
            Salesman,
            Manager
        }

        private Employee(EmployeeType type)
        {
            _type = type;
        }

        public void SetSalary(int monthlySalary, int commission, int bonus)
        {
            _monthlySalary = monthlySalary;
            _commission = commission;
            _bonus = bonus;
        }

        public int PayAmount()
        {
            switch (_type)
            {
                case EmployeeType.Engineer:
                    return _monthlySalary;
                case EmployeeType.Salesman:
                    return _monthlySalary + _commission;
                case EmployeeType.Manager:
                    return _monthlySalary + _bonus;
                default:
                    return 0;
            }
        }
    }
}