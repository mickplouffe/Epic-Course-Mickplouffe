using UnityEngine;
using UnityEngine.AI;

public class Enemies : MonoBehaviour, IDamagable
{
    public GameObject _target, _deathExplosion;
    public NavMeshAgent agent;
    BoxCollider _collider;
    [SerializeField] Animator animator;

    [field: SerializeField]
    public int Health { get; set; }

    [SerializeField] int _defaultHealth, _warFundValue = 10, _bulletDamage = 1, _bodyDamage = 1;
    [SerializeField] float _cleanUpTime = 5;
    [SerializeField] bool _isDead = false, _isDissolved = false;

    private void OnEnable()
    {
        GameManager.resetGameEvent += ResetEnemy;


        if (_target == null)
        {
            _target = GameObject.Find("TheHQ");
        }

    }

    private void OnDisable() => GameManager.resetGameEvent -= ResetEnemy;


    private void Start()
    {
        animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider>();

        _defaultHealth = Health;
        agent.SetDestination(_target.transform.position);
    }

    void DiyingSequence()
    {
        agent.enabled = false;

        if (_deathExplosion != null)
            Instantiate(_deathExplosion, transform.position + Vector3.up, Quaternion.identity);


        _collider.enabled = false;
        if (animator != null)
            animator.SetBool("isDissolved", true);
        Invoke("Dissolving", _cleanUpTime);

    }
    void Dissolving()
    {        
        Invoke("Diying", 0);
    }

    void Diying()
    {
        gameObject.transform.position = Vector3.down * 20;

        if (animator != null)
            animator.SetBool("isDead", false);

        Health = _defaultHealth;

        gameObject.tag = "Enemy";

        _collider.enabled = true;

        Invoke("Hide", 0.2f);
    }

    public int GetBodyDamage()
    {
        return _bodyDamage;
    }

    void Hide()
    {
        this.gameObject.SetActive(false);
        animator.SetBool("isDissolved", false);
    }

    public void TakeDamage(int dmg = 1)
    {
        Health -= dmg;   

        if (Health <= 0)
        {           
            
            gameObject.tag = "DeadEnemy";

            if (animator != null)
                animator.SetBool("isDead", true);

            GameManager.Instance.AddWarFunds(_warFundValue);
            DiyingSequence();
        }
    }

    public void ResetEnemy()
    {
        Health = 1;
        TakeDamage();
    }

}
