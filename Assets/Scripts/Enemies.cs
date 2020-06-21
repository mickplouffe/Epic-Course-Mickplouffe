using UnityEngine;
using UnityEngine.AI;

public class Enemies : MonoBehaviour
{
    public GameObject _target, _deathExplosion;
    public NavMeshAgent agent;
    BoxCollider _collider;
    [SerializeField] Animator animator;
    [SerializeField] int _health = 1, _defaultHealth, _warFundValue = 10;
    [SerializeField] float _cleanUpTime = 5;
    [SerializeField] bool _isDead = false;


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
        animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider>();

        _defaultHealth = _health;
        agent.SetDestination(_target.transform.position);
    }

    void DiyingSequence()
    {
        agent.enabled = false;

        if (_deathExplosion != null)
            Instantiate(_deathExplosion, transform.position + Vector3.up, Quaternion.identity);

        _collider.enabled = false;
        Invoke("Diying", _cleanUpTime);
    }

    void Diying()
    {       

        gameObject.transform.position = Vector3.down * 20;

        if (animator != null)
            animator.SetBool("isDead", false);

        _health = _defaultHealth;

        gameObject.tag = "Enemy";

        _collider.enabled = true;

        Invoke("Hide", 0.2f);

    }

    void Hide()
    {
        this.gameObject.SetActive(false);       

    }

    public void TakeDamage(int dmg = 1)
    {
        _health -= dmg;   

        if (_health <= 0)
        {
            
            
            gameObject.tag = "DeadEnemy";
            if (animator != null)
                animator.SetBool("isDead", true);

            GameManager.Instance.ChangeWarFunds(_warFundValue);
            DiyingSequence();
        }
    }

}
