using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Status : MonoBehaviour
{
    public GameObject player;

    public int maxHealth = 3;
    public int currentHealth = 3;
    public HealthController healthBar;
    public bool isInvincible = false;
    
    private PlayerMovement playerCon;
    private EnemyManager enemyManager;

    public int coinCount;
    [SerializeField]
    private TextMeshProUGUI coinCounterText;

    [SerializeField]
    private int killCount;
    [SerializeField]
    private TextMeshProUGUI killCounterText;

    [SerializeField]
    private Animator gameManagerAnim;

    void Start()
    {
        player = this.gameObject;
        currentHealth = maxHealth;
        playerCon = GetComponent<PlayerMovement>();
        enemyManager = GameObject.FindWithTag("EnemyManager").GetComponent<EnemyManager>();
        healthBar = GetComponentInParent<HealthController>();
    }

   public void TakeDamage(Collider coll)
    {
        if (!isInvincible)
        {
            Debug.Log("DamageTaken");
            Vector3 direction = coll.transform.position - player.transform.position;
            playerCon.rb.AddForce(-direction.normalized * 50f, ForceMode.Impulse);
            currentHealth -= coll.gameObject.GetComponent<Enemy>().damage;
            gameManagerAnim.SetInteger("Health", currentHealth);
            gameManagerAnim.SetTrigger("PlayerHurt");
            if (currentHealth == 0)
            {
                playerCon.enabled = false;
                SetActiveWeapon(null);
            }
            else
            {
                isInvincible = true;
                StartCoroutine(InvisibilityWait(2f));
            }
        }
    }

    private IEnumerator InvisibilityWait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isInvincible = false;
    }

    public void SetActiveWeapon(GameObject weaponName)
    {

        GameObject[] weapons = GameObject.FindGameObjectsWithTag("Weapon");
        for (int i = 0; i < weapons.Length; i++)
        {
            for (int j = 0; j < weapons[i].transform.childCount; j++)
            {
                weapons[i].transform.gameObject.active = false;
            }
        }

        if (weaponName != null)
        {
            weaponName.SetActive(true);
        }
    }

    public void UpdateCoinCounter()
    {
        coinCounterText.text = coinCount.ToString();
    }

    public void IncreaseKillcount()
    {
        killCount++;
        killCounterText.text = killCount.ToString();
        if (killCount % 50 == 0)
        {
            enemyManager.ThreatLevelIncrease();
        }
    }
}