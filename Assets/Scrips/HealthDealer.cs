using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDealer : MonoBehaviour
{
 
    [SerializeField] float bonusHealth = 25f;

  public float GetBonusHealth()
    {
        return bonusHealth;
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
