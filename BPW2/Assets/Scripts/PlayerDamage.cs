using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public int playerHP = 50;
    public int tileDamage = 10;
    public int bossHP = 50;
    public int attackDamage = 5;
    private bool DamageCooldown = false;
    private bool BossDamageCooldown = false;
    public float DamageCooldownFloat = 2f;
    public float DamageCooldownBossFloat = 2f;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "AttackTile" && DamageCooldown == false)
        {
            playerHP -= tileDamage;
            Debug.Log(playerHP);
        }

        if (collision.gameObject.tag == "Boss" && BossDamageCooldown == false)
        {
            bossHP -= attackDamage;
            Debug.Log(bossHP);
        }
    }

    void Update()
    {
        StartCoroutine(DamageCooldownCheck());
    }

    IEnumerator DamageCooldownCheck()
    {
        if (DamageCooldown == true)
        {
            yield return new WaitForSeconds(DamageCooldownFloat);
            DamageCooldown = false;
        }

        if (BossDamageCooldown == true)
        {
            yield return new WaitForSeconds(DamageCooldownBossFloat);
            BossDamageCooldown = false;
        }
    }

}
