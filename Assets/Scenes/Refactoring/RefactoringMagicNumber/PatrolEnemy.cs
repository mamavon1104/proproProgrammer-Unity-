using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    [SerializeField] List<Transform> moveTargets;
    [SerializeField] private CharacterController controller;
    [SerializeField] float enemySpeed = 0.5f;
    readonly float targetPositionDistance = 1.5f;

    private int currentTargetIndex = 0;
    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        controller = this.gameObject.AddComponent<CharacterController>();
    }

    void Update()
    {
        transform.LookAt(moveTargets[currentTargetIndex].transform);
        moveDirection = moveTargets[currentTargetIndex].transform.position - transform.position;
        controller.SimpleMove(moveDirection * enemySpeed);

        if (Vector3.Distance(transform.position, moveTargets[currentTargetIndex].transform.position) < targetPositionDistance)
        {
            currentTargetIndex = (++currentTargetIndex) % moveTargets.Count;
        }
    }
}
