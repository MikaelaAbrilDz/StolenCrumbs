using UnityEngine;

[CreateAssetMenu(fileName = "Round", menuName = "ScriptableObjects/Round")]
public class Round : ScriptableObject
{
    public enum EnemyType
    {
        ant, fly, beetle
    }
    public int numberOfEnemies;
    public float rate;
    public EnemyType[] enemies;
    [Range(0f, 1f)] public float[] enemiesProbs;
}
