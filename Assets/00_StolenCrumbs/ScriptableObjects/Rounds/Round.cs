using UnityEngine;

[CreateAssetMenu(fileName = "Round", menuName = "ScriptableObjects/Round")]
public class Round : ScriptableObject
{
    public enum EnemyType
    {
        ant1, fly1, beetle1, ant2, fly2, beetle2, ant3, fly3, beetle3,
    }
    public int numberOfEnemies;
    public float rate;
    public EnemyType[] enemies;
    [Range(0f, 1f)] public float[] enemiesProbs;
}
