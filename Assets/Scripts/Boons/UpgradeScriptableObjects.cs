using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Stats Boosts" , menuName = "ScriptableObjects/Stats Boosts")]
public class UpgradeScriptableObjects : ScriptableObject
{
    public string boonName;
    public string boonDescription;
    public Sprite boonImage;
    public float value;
    public float weighting;

    public string GetBoonName() => boonName;
    public string GetBoonDescription() => boonDescription;
    public Sprite GetBoonImage() => boonImage;
    public float GetValue() => value;

    public float GetWeighting() => weighting;   
}
