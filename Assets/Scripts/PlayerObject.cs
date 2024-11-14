using UnityEngine;

[CreateAssetMenu(fileName = "PlayerObject", menuName = "ScriptableObjects/PlayerObject", order = 1)]
public class PlayerObject : ScriptableObject
{
    public float damage;
    public float health;
    public float defense;
    public float speed;
}
