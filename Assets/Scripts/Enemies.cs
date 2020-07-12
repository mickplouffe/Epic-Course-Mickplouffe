using UnityEngine;
using UnityEngine.AI;

public class Enemies : MonoBehaviour, IDamagable
{
    public GameObject _target, _deathExplosion;
    public NavMeshAgent agent;
    BoxCollider _collider;
    [SerializeField] Animator animator;
    [SerializeField] GameObject healthBarObj;
    Material healBarMaterial;

    [field: SerializeField]
    public int Health { get; set; }

    [SerializeField] int _defaultHealth, _warFundValue = 10, _bodyDamage = 1;
    [SerializeField] float _cleanUpTime = 5;

    [Header("HealthBar Animation Settings")]
    [SerializeField] float healthBarAnimationTime = 2;
    float desiredHealth, initialHealth, currentHealth;


    private void OnEnable()
    {
        GameManager.resetGameEvent += ResetEnemy;
        if (_target == null)
            _target = GameObject.Find("TheHQ");
    }

    private void OnDisable() => GameManager.resetGameEvent -= ResetEnemy;

    private void Start()
    {
        animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider>();

        _defaultHealth = Health;
        agent.SetDestination(_target.transform.position);

        healBarMaterial = healthBarObj.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material;

        currentHealth = 1.05f - ((float)Health / _defaultHealth);
        initialHealth = currentHealth;
        desiredHealth = currentHealth;
    }

    private void FixedUpdate()
    {
        HealthBarAnimation();
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
    void Dissolving() => Invoke("Diying", 0);

    void Diying()
    {
        gameObject.transform.position = Vector3.down * 20;

        if (animator != null)
            animator.SetBool("isDead", false);

        Health = _defaultHealth;

        gameObject.tag = "Enemy";

        currentHealth = Health;
        initialHealth = currentHealth;
        desiredHealth = currentHealth;
        healthBarObj.SetActive(true);

        healBarMaterial.SetVector("_offset", Vector2.zero);
        healBarMaterial.SetVector("_colorLerp", Vector2.zero);

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
        initialHealth = 1.05f - ((float)Health / _defaultHealth);        
        Health -= dmg;

        if (healBarMaterial != null)
        {            
            desiredHealth = 1.05f - ((float)Health / _defaultHealth);            
        }

        if (Health <= 0)
        {
            gameObject.tag = "DeadEnemy";

            if (animator != null)
                animator.SetBool("isDead", true);

            GameManager.Instance.AddWarFunds(_warFundValue);
            DiyingSequence();
        }
    }

    void HealthBarAnimation()
    {
        if (currentHealth > 1)
        {
            healthBarObj.SetActive(false);
        }

        if (currentHealth != desiredHealth && healthBarObj.activeSelf)
        {
            if (initialHealth < desiredHealth)
            {
                currentHealth += (healthBarAnimationTime * Time.fixedDeltaTime) * (desiredHealth - initialHealth);
                if (currentHealth >= desiredHealth)
                    currentHealth = desiredHealth;
            }
            else
            {
                currentHealth -= (healthBarAnimationTime * Time.fixedDeltaTime) * (initialHealth - desiredHealth);
                if (currentHealth <= desiredHealth)
                    currentHealth = desiredHealth;
            }

            healBarMaterial.SetVector("_offset", new Vector2(currentHealth, 0));
            healBarMaterial.SetVector("_colorLerp", new Vector2(currentHealth, currentHealth));
        }
    }

    public void ResetEnemy()
    {
        Health = 1;
        TakeDamage();
    }
}
