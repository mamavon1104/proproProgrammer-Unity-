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

    public Vector3 _startPos;
    public Vector3 _endPos;
    public float s_x;//始点x座標
    public float s_z;//始点z座標
    public float e_x;//終点x座標
    public float e_z;//終点z座標

    private void Start()
    {
        InitializeAgents();
        this.UpdateAsObservable().Subscribe(_ =>
        {
            if (!_isSelect)
            {
                if (Input.GetMouseButtonDown(0))
                    SelectCharacterStart();
                else if (Input.GetMouseButtonUp(0))
                    SelectCharacterEnd();
            }
            else if (Input.GetMouseButtonDown(0))
                SelectGroundPosition();
        });
    }

    /// <summary>
    /// NavMeshAgent関係などの初期化。
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
    /// 動いているかどうか判定をする
    /// </summary>
    /// <returns>
    /// 動いていればtrueを返し、
    /// 動いて無ければfalse
    /// </returns>
    private bool IsAgentMoving(NavMeshAgent agent)
    {
        return !agent.isStopped && agent.remainingDistance > agent.stoppingDistance;
    }

    /// <summary>
    /// Agent達の取得をする関数
    /// </summary>
    /// <returns>　取得出来たらtrue,その二はfalse</returns>
    private bool TryGetClickedAgent(out List<NavMeshAgent> agentsArray)
    {
        agentsArray = new List<NavMeshAgent>();

        s_x = Mathf.Min(_startPos.x, _endPos.x);
        e_x = Mathf.Max(_startPos.x, _endPos.x);
        s_z = Mathf.Min(_startPos.z, _endPos.z);
        e_z = Mathf.Max(_startPos.z, _endPos.z);

        if (Mathf.Abs(e_x - s_x) <= 0.5f && Mathf.Abs(e_x - s_x) <= 0.5f)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, Mathf.Infinity).Debuglog("raycast"))
                if (hit.transform.TryGetComponent<NavMeshAgent>(out var agent).Debuglog("try getcompnent navmeshagents"))
                    agentsArray.Add(agent);
        }
        else
        {
            foreach (var agent in _selectedAgentsDic.Keys)
            {
                var posi = agent.transform.position;
                if ((posi.x >= s_x && posi.x <= e_x) && (s_z <= posi.z && e_z >= posi.z))
                    agentsArray.Add(agent);
            }
        }

        return agentsArray.Count > 0; //このまま返すの美しい
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
    /// マウスのポジションから位置を判定する
    /// </summary>
    /// <return> 地面をクリックしたらtrue,その他はelse </return>
    private bool TryGetClickPosition(out Vector3 position)
    {
        position = Vector3.zero;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hit, 100))
        {
            if (!hit.transform.TryGetComponent<NavMeshAgent>(out _)) //navmeshが付いてなかったら地面と見なし侵攻。
            {
                position = hit.point;
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Agent達を動かす。
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
    /// 非選択状態に戻す。
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