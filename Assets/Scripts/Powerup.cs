using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : ScriptableObject
{
    public bool isActive;
    [SerializeField]
    private int[] UpgradeCosts;
    [SerializeField]
    protected int currentLevel = 1;
    [SerializeField]
    protected int maxLevel = 3;
    [SerializeField]
    protected PowerupStats duration;

    public float GetDuration() { return duration.GetValue(currentLevel); }

    private void Awake()
    {
        LoadPowerUpStats();
    }

    private void OnValidate()
    {
        currentLevel = Mathf.Min(currentLevel, maxLevel);
        currentLevel = Mathf.Max(currentLevel, 1);
    }
    public bool isMaxedOut()
    {
        return currentLevel == maxLevel;
    }

    public int GetNextUpgradeCost()
    {
        if (!isMaxedOut())
        {
            return UpgradeCosts[currentLevel - 1];

        }
        else
        {
            return -1;
        }
    }

    public void Upgrade()
    {
        if (isMaxedOut()) return;
        currentLevel++;
        SavePowerUpLevel();
    }

    private void SavePowerUpLevel()
    {
        string key = name + "Level";
        PlayerPrefs.SetInt(key,currentLevel);
    }

    private void LoadPowerUpStats()
    {
        string key = name = "Level";
        if (PlayerPrefs.HasKey(key))
        {
            currentLevel = PlayerPrefs.GetInt(key);
        }
    }
    public override string ToString()
    {
        string text = $"{name}\bLVL. {currentLevel}";
        if (isMaxedOut())
        {
            text += "(MAX)";
        }

        return text;
    }

    public string UpgradeCostString()
    {
        if (!isMaxedOut())
        {
            return $"UPGRADE\nCOST: {GetNextUpgradeCost()}";
        }
        else
        {
            return "MAXED OUT";
        }
    }
}
