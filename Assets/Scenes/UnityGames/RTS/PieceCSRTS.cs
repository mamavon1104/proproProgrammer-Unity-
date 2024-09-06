using Mamavon.Funcs;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.AI;

public class PieceCSRTS : MonoBehaviour
{
    [SerializeField] private List<NavMeshAgent> _agentsList = new List<NavMeshAgent>();
    [SerializeField] private bool _isSelect;
    private Dictionary<NavMeshAgent, ReactiveProperty<bool>> _selectedAgentsDic = new Dictionary<NavMeshAgent, ReactiveProperty<bool>>();

    [SerializeField] LayerMask ignoreLayers;
    [SerializeField] Vector3 _startPos, _endPos;
    [SerializeField] float start_x, start_z;    //�n�_x,z���W
    [SerializeField] float end_x, end_z;        //�I�_x,z���W

    private void Start()
    {
        InitializeAgents(); //����������
        this.UpdateAsObservable().Subscribe(_ =>
        {
            if (!_isSelect)
            {
                if (Input.GetMouseButtonDown(0))
                    SelectCharacterStart(); //�ʒu�L�^�J�n
                else if (Input.GetMouseButtonUp(0))
                    SelectCharacterEnd();   //�ʒu�L�^�I��
            }
            else if (Input.GetMouseButtonDown(0))
                SelectGroundPosition(); //�ړ���I��
        });
    }

    /// <summary>
    /// NavMeshAgent�֌W�Ȃǂ̏������B
    /// </summary>
    private void InitializeAgents()
    {
        if (_agentsList.Count == 0)
            GetAgents();

        foreach (var agent in _agentsList)
        {
            var isSelected = new ReactiveProperty<bool>(false);
            _selectedAgentsDic.Add(agent, isSelected);

            isSelected.Subscribe(selected =>
            {
                var renderer = agent.GetComponent<MeshRenderer>();
                if (renderer != null && renderer.material != null)
                    renderer.material.color = selected ? Color.yellow : Color.blue;
            });
        }
    }

    /// <summary>
    /// �����Ă��邩�ǂ������������
    /// </summary>
    /// <returns>
    /// �����Ă����true��Ԃ��A
    /// �����Ė������false
    /// </returns>
    private bool IsAgentMoving(NavMeshAgent agent)
    {
        return !agent.isStopped && agent.remainingDistance > agent.stoppingDistance;
    }

    /// <summary>
    /// Agent�B�̎擾������֐�
    /// </summary>
    /// <returns>�@�擾�o������true,���̓��false</returns>
    private bool TryGetClickedAgent(out List<NavMeshAgent> agentsArray)
    {
        agentsArray = new List<NavMeshAgent>();

        start_x = Mathf.Min(_startPos.x, _endPos.x);
        end_x = Mathf.Max(_startPos.x, _endPos.x);
        start_z = Mathf.Min(_startPos.z, _endPos.z);
        end_z = Mathf.Max(_startPos.z, _endPos.z);

        //�I�����Ă���͈͂�����������raycast
        if (Mathf.Abs(end_x - start_x) <= 0.5f && Mathf.Abs(end_x - start_x) <= 0.5f)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, ~ignoreLayers).Debuglog("raycast", TextColor.Red))
                if (hit.transform.TryGetComponent<NavMeshAgent>(out var agent).Debuglog("try getcompnent navmeshagents"))
                    agentsArray.Add(agent);
        }
        else //�I�����Ă���͈͂�臒l�𒴂��Ă���ꍇ�I�u�W�F�N�g�̈ʒu�Ŕ͈͑I�����s��
        {
            foreach (var agent in _selectedAgentsDic.Keys)
            {
                var posi = agent.transform.position;
                if ((posi.x >= start_x && posi.x <= end_x) && (start_z <= posi.z && end_z >= posi.z))
                    agentsArray.Add(agent);
            }
        }
        return agentsArray.Count > 0; //�������ŕԂ��̔�����
    }

    void GetMousePosition(out Vector3 pos)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            pos = hit.point;
        }
        else
        {
            pos = Vector3.zero;
        }
    }
    private void SelectCharacterStart()
    {
        GetMousePosition(out _startPos);
    }
    private void SelectCharacterEnd()
    {
        GetMousePosition(out _endPos);

        if (!TryGetClickedAgent(out var agents))
            return;

        foreach (var agent in agents)
        {
            if (IsAgentMoving(agent))
                continue;

            _isSelect = true;
            _selectedAgentsDic[agent].Value = true;
        }
    }

    private void SelectGroundPosition()
    {
        if (TryGetClickPosition(out var position))
        {
            MoveSelectedAgents(position);
        }
        DeselectAllAgents();
    }

    /// <summary>
    /// �}�E�X�̃|�W�V��������ʒu�𔻒肷��
    /// </summary>
    /// <return> �n�ʂ��N���b�N������true,���̑���else </return>
    private bool TryGetClickPosition(out Vector3 position)
    {
        position = Vector3.zero;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hit, 100))
        {
            if (!hit.transform.TryGetComponent<NavMeshAgent>(out _)) //navmesh���t���ĂȂ�������n�ʂƌ��Ȃ��N�U�B
            {
                position = hit.point;
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Agent�B�𓮂����B
    /// </summary>
    private void MoveSelectedAgents(Vector3 destination)
    {
        var selectedAgents = _selectedAgentsDic.Where(kv => kv.Value.Value)
                                            .Select(kv => kv.Key);
        foreach (var agent in selectedAgents)
        {
            agent.SetDestination(destination);
        }
    }

    /// <summary>
    /// ��I����Ԃɖ߂��B
    /// </summary>
    private void DeselectAllAgents()
    {
        _isSelect = false;
        foreach (var agent in _selectedAgentsDic.Keys)
        {
            _selectedAgentsDic[agent].Value = false;
        }
    }

    [ContextMenu("GetAgents")]
    void GetAgents()
    {
        _agentsList = new List<NavMeshAgent>(FindObjectsByType<NavMeshAgent>(FindObjectsSortMode.None));
    }
}