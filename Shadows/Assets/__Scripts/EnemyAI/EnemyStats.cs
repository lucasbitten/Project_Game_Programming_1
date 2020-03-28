using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "AI/EnemyStats")]
public class EnemyStats : ScriptableObject
{

    // General Information
    [Header("General Information")]
    public int scoreValue = 10;

    // State Information
    [Header("State Information")]

    [Header("State - PATROL")]
    public float patrolSpeed = 1;

    [Header("State - CHASE")]
    public float chaseSpeed = 2;
    public float chaseRange = 5;

    [Header("State - SEARCH")] 
    public float timeToSearch = 5;
    public float searchSpeed = 2;

    [Header("State - ATTACK")]
    public float attackRange = 0.5f;
    public float attackRate = 1;
    public int attackDamage = 1;

    // Health Information
    [Header("Healt Information")]
    public int maxHealth = 3;
    public int currentHealth;

    // Audio Information

}
