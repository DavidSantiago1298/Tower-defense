using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "Scriptable Objects/GunData")]
public class GunData : ScriptableObject
{
    public float fireRate = 1f;

    public float damage = 20f;

    public string fireSoundName;
    
}
