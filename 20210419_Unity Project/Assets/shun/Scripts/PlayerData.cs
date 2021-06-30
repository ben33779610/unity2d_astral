using UnityEngine;


[CreateAssetMenu(fileName = "玩家資料", menuName = "Data/玩家資料")]
public class PlayerData : ScriptableObject
{
    // 能力值
    [Header("生命值")][Range(100, 10000)]
    public int hp = 100;

    [Header("魔力值")][Range(100, 10000)]
    public int mp = 100;

    [Header("攻擊力")][Range(100, 10000)]
    public int atk = 100;

    [Header("防禦力")][Range(100, 10000)]
    public int def = 100;

    [Header("攻擊長度"), Range(0.1f, 100)]
    public float atkLength = 5f;

    [Header("水平推力")][Range(0, 10000)]
    public float xForce;
    [Header("垂直向上推力")][Range(0, 10000)]
    public float yForce = 5000.0f;

    [Header("碰撞的力量"), Range(0, 5000)]
    public float moveForce;
    [Header("往上的力量"), Range(0, 5000)]
    public float jumpForce;
}
