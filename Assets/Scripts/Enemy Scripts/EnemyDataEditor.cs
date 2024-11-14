using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyData))]
public class EnemyDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EnemyData enemyData = (EnemyData)target;

        enemyData.enemyName = EditorGUILayout.TextField("Enemy Name", enemyData.enemyName);
        enemyData.enemySprite = (Sprite)EditorGUILayout.ObjectField("Sprite", enemyData.enemySprite, typeof(Sprite), false);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Stats", EditorStyles.boldLabel);
        enemyData.health = EditorGUILayout.FloatField("Health", enemyData.health);
        enemyData.speed = EditorGUILayout.FloatField("Speed", enemyData.speed);
        enemyData.damage = EditorGUILayout.IntField("Damage", enemyData.damage);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Combat", EditorStyles.boldLabel);
        enemyData.attackRange = EditorGUILayout.FloatField("Attack Range", enemyData.attackRange);
        enemyData.attackCooldown = EditorGUILayout.FloatField("Attack Cooldown", enemyData.attackCooldown);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Loot", EditorStyles.boldLabel);
        SerializedProperty lootItems = serializedObject.FindProperty("lootItems");
        SerializedProperty lootDropChances = serializedObject.FindProperty("lootDropChances");
        EditorGUILayout.PropertyField(lootItems, true);
        EditorGUILayout.PropertyField(lootDropChances, true);

        serializedObject.ApplyModifiedProperties();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Animations & Abilities", EditorStyles.boldLabel);
        enemyData.animatorController = (RuntimeAnimatorController)EditorGUILayout.ObjectField("Animator Controller", enemyData.animatorController, typeof(RuntimeAnimatorController), false);

        enemyData.hasSpecialAbility = EditorGUILayout.Toggle("Has Special Ability", enemyData.hasSpecialAbility);
        if (enemyData.hasSpecialAbility)
        {
            enemyData.abilityName = EditorGUILayout.TextField("Ability Name", enemyData.abilityName);
        }

        EditorGUILayout.Space();
        enemyData.enemyPrefab = (GameObject)EditorGUILayout.ObjectField("Prefab", enemyData.enemyPrefab, typeof(GameObject), false);
    }
}
