using UnityEngine;

[CreateAssetMenu(fileName = "怪物資料", menuName = "Data/怪物資料")]
public class EnemyData : ScriptableObject
{
    
    [Header("移動速度"), Range(0, 50)]
    public float speed;
    [Header("血量"), Range(100, 5000)]
    public float hp;
    [Header("攻擊力"), Range(10, 1000)]
    public float atk;
    [Header("攻擊延遲"), Range(1, 100)]
    public float atkdelay;
    [Header("冷卻時間"), Range(1, 100)]
    public float cd;
    [Header("停止距離"), Range(0.2f, 100)]
    public float stopdis;

    
    [Header("碰撞半徑"), Range(0.1f,10)]
    public float collisionRadius = 5f;
    [Header("射線的長度"), Range(0.1f, 10)]
    public float radialLength = 1.1f;
    [Header("攻擊長度"), Range(0.1f, 100)]
    public float atkLength = 5f;


    [Header("子彈速度"), Range(0.1f, 5000)]
    public float bulletspeed;
    
    [Header("碰撞的力量"), Range(0, 5000)]
    public float moveForce;
    [Header("往上的力量"), Range(0, 5000)]
    public float jumpForce;
}
