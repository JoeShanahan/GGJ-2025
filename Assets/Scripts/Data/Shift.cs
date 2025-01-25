using UnityEngine;

[CreateAssetMenu]
public class Shift : ScriptableObject
{
    public GameState.Stage stage;
    public GameState.HorrorLevel horrorLevel;
    
    public OrderInfo[] orders;
}
