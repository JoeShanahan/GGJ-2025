using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class IngredientData : ScriptableObject
{
    public string IngredientName;
    public Color Tint;
    public GameObject BottlePrefab;
    public AudioClip BottleClink;
}
