using UnityEngine;
using UnityEngine.AI;

public class Enemies : MonoBehaviour
{
    public GameObject _target;
    public NavMeshAgent agent;
    [SerializeField] int _health = 1, _defaultHealth, _warFundValue = 10;

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
        _defaultHealth = _health;
        agent.SetDestination(_target.transform.position);
    }

    void Hide()
    {
        //CancelInvoke();
        this.gameObject.SetActive(false);
        _health = _defaultHealth;

    }

    public void TakeDamage(int dmg = 1)
    {
        _health -= dmg;
        Debug.LogWarning("Taking Damage!");

        if (_health <= 0)
        {
            GameManager.Instance.ChangeWarFunds(_warFundValue);
            Hide();
        }
    }

}
