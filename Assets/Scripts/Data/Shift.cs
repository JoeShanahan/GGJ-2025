using UnityEngine;

[CreateAssetMenu]
public class Shift : ScriptableObject
{
    public int stage;
    public GameState.HorrorLevel horrorLevel;
    
    public OrderInfo[] orders;
}
