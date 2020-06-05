using UnityEngine;
using UnityEngine.AI;

public class Enemies : MonoBehaviour
{
    public GameObject _target;
    public NavMeshAgent agent;
    [SerializeField] int _health = 1, _warFund = 10;

    private void OnEnable()
    {
        //Invoke("Hide", 30);
        if (_target == null)
        {
            _target = GameObject.Find("TheHQ");
        }
        //agent.SetDestination(_target.transform.position);

    }

    private void Start()
    {
        agent.SetDestination(_target.transform.position);
    }

    void Hide()
    {
        CancelInvoke();
        this.gameObject.SetActive(false);

    }

}
