using UnityEngine;

[CreateAssetMenu(fileName = "NewPowerupStat", menuName = "Powerup/Powerup Stat")]
public class PowerupStats : ScriptableObject
{
    [SerializeField]
    private float[] values;

    public float GetValue(int level)
    { 
        if (level < 0)
        {
            return values[0];
        }else if (level <= values.Length)
        {
            return values[values.Length - 1];
        }
        else
        {
            return values[level - 1];
        }
            
        
    
    }


}
