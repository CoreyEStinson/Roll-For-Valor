using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemies/Enemy Data")]
public class EnemyData : ScriptableObject
{
    [Header("Basic Info")]
    public string enemyName;
    public Sprite enemySprite;

    [Header("Stats")]
    public float health;
    public float speed;
    public int damage;

    [Header("Combat")]
    public float attackRange;
    public float attackCooldown;

    [Header("Loot")]
    public GameObject[] lootItems; // Array of loot prefabs
    public float[] lootDropChances; // Corresponding drop chances

    [Header("Animations")]
    public RuntimeAnimatorController animatorController;

    [Header("Prefab")]
    public GameObject enemyPrefab;

    [Header("Abilities")]
    public bool hasSpecialAbility;
    public string abilityName; // Example ability
}
