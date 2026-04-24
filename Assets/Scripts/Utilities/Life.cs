using UnityEngine;
using UnityEngine.Events;

public class Life : MonoBehaviour
{
    [SerializeField] int maxhp;
    [SerializeField] private int hp;
    [Space(5)]
    [SerializeField] private float destroyTime = 1f;

    [Header("// UNITY EVENTS -----------------------------------------------------------------------------------------")]
    [Space(10)]
    [SerializeField] private UnityEvent<int> OnHPChanged;
    [SerializeField] private UnityEvent OnDeath;
    [SerializeField] private UnityEvent OnHit;

    #region// GET --------------------------------------------------------
    public int GetHp()
    { return hp; }
    #endregion


    // GAME //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    private void Awake()
    {
        hp = maxhp;
    }


    // FUNCTIONS //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    public void TakeDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            // SetUp the Death sequence
            OnHPChanged.Invoke(hp);

            GetComponent<Collider2D>().enabled = false;
            this.tag = "Untagged";
            OnDeath.Invoke();

            Debug.Log($"{gameObject.name} is dead");
            Destroy(gameObject, destroyTime);
        }
        else
        {
            OnHPChanged.Invoke(hp);
            OnHit.Invoke();

            Debug.Log($"{gameObject.name} has {hp}/{maxhp}");
        }
    }


    public void TakeHeal(int amout)
    {
        hp += amout;
        OnHPChanged.Invoke(hp);

        if (hp > maxhp) hp = maxhp;
        Debug.Log($"{gameObject.name} ha {hp}/{maxhp}");
    }
}