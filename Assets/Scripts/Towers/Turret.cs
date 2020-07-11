using UnityEngine;

public class Turret : MonoBehaviour, ITurret, IDamagable
{
    [SerializeField] int _warFundCost = 100;
    public GameObject UpgradeTurret;

    [SerializeField] GameObject UI_Elements;

    [field: SerializeField]
    public int Health { get; set; }


    TurretTargeting turretTargeting;
    Animator animator;


    private void OnEnable() => CameraController.DeselectAll += OnDeselect;
    private void OnDisable() => CameraController.DeselectAll -= OnDeselect;

    private void Start()
    {
        turretTargeting = GetComponent<TurretTargeting>();
        animator = GetComponent<Animator>();
    }

    public int WarFundCost => _warFundCost;

    public void TakeDamage(int damageAmount) => Health -= damageAmount;

    void OnDeselect()
    {
        if (UI_Elements != null && animator != null)
            animator.SetBool("isUIUP", false);

        if (turretTargeting != null)        
            turretTargeting.OnDeselected();
        
    }

    public void OnSelect()
    {
        if (UI_Elements != null && animator != null)
            animator.SetBool("isUIUP", true);

        if (turretTargeting != null)        
            turretTargeting.OnSelected();        
    }
}