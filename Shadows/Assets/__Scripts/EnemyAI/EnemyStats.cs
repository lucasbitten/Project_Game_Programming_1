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
    [Header("State PATROL")]
    public float patrolSpeed = 1.0f;

    [Header("State CHASE")] 
    public float chaseSpeed = 5.0f;
    public float chaseRange = 5.0f;

    [Header("State ATTACK")] 
    public float attackRange = 0.1f;
    public float attackRate = 1.0f;
    public float attackDamage = 10.0f;


    // Health Information

    // Audio Information
}
