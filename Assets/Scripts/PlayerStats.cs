using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int HealthPoints;

    public int ExtraJumps;
    public float JumpForce;

    public float AccelerationSpeedMultiplyer;
    public float DecelerationSpeedMultiplyer;
    public float HorizontalSpeedMultiplyer;
    public float MaxSpeed;
    public float BaseSpeed;

    public float Coins;
    public float BaseCoinValue;
    public float CoinMultiplyer;

    public void UpgradeHealthPoints()
    {
        HealthPoints += 1;
        PlayerPrefs.SetInt(nameof(HealthPoints), HealthPoints);
    }

    public void UpgradeExtraJumps()
    {
        ExtraJumps++;
        PlayerPrefs.SetInt(nameof(ExtraJumps), ExtraJumps);
    }

    public void UpgradeJumpForce()
    {
        JumpForce += 0.2f;
        PlayerPrefs.SetFloat(nameof(JumpForce), JumpForce);
    }

    public void UpgradeAccelerationSpeed()
    {
        AccelerationSpeedMultiplyer += 0.1f;
        PlayerPrefs.SetFloat(nameof(AccelerationSpeedMultiplyer), AccelerationSpeedMultiplyer);
    }

    public void UpgradeDecelerationSpeed()
    {
        DecelerationSpeedMultiplyer += 0.1f;
        PlayerPrefs.SetFloat(nameof(DecelerationSpeedMultiplyer), DecelerationSpeedMultiplyer);
    }

    public void UpgradeHorizontalSpeed()
    {
        HorizontalSpeedMultiplyer += 0.1f;
        PlayerPrefs.SetFloat(nameof(HorizontalSpeedMultiplyer), HorizontalSpeedMultiplyer);
    }

    public void UpgradeMaxSpeed()
    {
        MaxSpeed += 0.2f;
        PlayerPrefs.SetFloat(nameof(MaxSpeed), MaxSpeed);
    }

    public void UpgradeBaseSpeed()
    {
        BaseSpeed += 0.1f;
        PlayerPrefs.SetFloat(nameof(BaseSpeed), MaxSpeed);
    }

    public void UpgradeBaseCoinValue()
    {
        BaseCoinValue += 1f;
        PlayerPrefs.SetFloat(nameof(BaseCoinValue), BaseCoinValue);
    }

    public void UpgradeCoinMultiplyer()
    {
        CoinMultiplyer += 0.1f;
        PlayerPrefs.SetFloat(nameof(CoinMultiplyer), CoinMultiplyer);
    }


    //Risky to use this method, as it will reset all stats to default values
    public void ResetStats()
    {
        HealthPoints = 1;
        ExtraJumps = 0;
        JumpForce = 5;
        AccelerationSpeedMultiplyer = 1.5f;
        DecelerationSpeedMultiplyer = 1.5f;
        MaxSpeed = 10;
        BaseCoinValue = 1;
        CoinMultiplyer = 1;

        PlayerPrefs.SetInt(nameof(HealthPoints), HealthPoints);

        PlayerPrefs.SetInt(nameof(ExtraJumps), ExtraJumps);
        PlayerPrefs.SetFloat(nameof(JumpForce), JumpForce);

        PlayerPrefs.SetFloat(nameof(AccelerationSpeedMultiplyer), AccelerationSpeedMultiplyer);
        PlayerPrefs.SetFloat(nameof(DecelerationSpeedMultiplyer), DecelerationSpeedMultiplyer);
        PlayerPrefs.SetFloat(nameof(HorizontalSpeedMultiplyer), HorizontalSpeedMultiplyer);
        PlayerPrefs.SetFloat(nameof(MaxSpeed), MaxSpeed);
        PlayerPrefs.SetFloat(nameof(BaseSpeed), BaseSpeed);

        PlayerPrefs.SetFloat(nameof(Coins), Coins);
        PlayerPrefs.SetFloat(nameof(BaseCoinValue), BaseCoinValue);
        PlayerPrefs.SetFloat(nameof(CoinMultiplyer), CoinMultiplyer);
    }
    void Start()
    {
        HealthPoints = PlayerPrefs.GetInt(nameof(HealthPoints), 1);

        ExtraJumps = PlayerPrefs.GetInt(nameof(ExtraJumps), 0);
        JumpForce = PlayerPrefs.GetFloat(nameof(JumpForce), 5);

        AccelerationSpeedMultiplyer = PlayerPrefs.GetFloat(nameof(AccelerationSpeedMultiplyer), 1.5f);
        DecelerationSpeedMultiplyer = PlayerPrefs.GetFloat(nameof(DecelerationSpeedMultiplyer), 1.5f);
        HorizontalSpeedMultiplyer = PlayerPrefs.GetFloat(nameof(HorizontalSpeedMultiplyer), 1.5f);
        MaxSpeed = PlayerPrefs.GetFloat(nameof(MaxSpeed), 10);
        BaseSpeed = PlayerPrefs.GetFloat(nameof(BaseSpeed), 5);

        Coins = PlayerPrefs.GetFloat(nameof(Coins), 0);
        BaseCoinValue = PlayerPrefs.GetFloat(nameof(BaseCoinValue), 1);
        CoinMultiplyer = PlayerPrefs.GetFloat(nameof(CoinMultiplyer), 1);

        NewMethod();
    }

    private void NewMethod()
    {
        var extraJumpsLevel = PlayerPrefs.GetInt("EJ", 0);
        var jumpForceLevel = PlayerPrefs.GetInt("JF", 0);
        var accelerationSpeedLevel = PlayerPrefs.GetInt("AS", 0);
        var decelerationSpeedLevel = PlayerPrefs.GetInt("DS", 0);
        var horizontalSpeedLevel = PlayerPrefs.GetInt("HS", 0);
        var maxSpeedLevel = PlayerPrefs.GetInt("MS", 0);
        var baseSpeedLevel = PlayerPrefs.GetInt("BS", 0);
        var baseCoinValueLevel = PlayerPrefs.GetInt("BCV", 0);
        var coinMultiplyerLevel = PlayerPrefs.GetInt("CM", 0);

        for (int i = 0; i < extraJumpsLevel; i++)
        {
            UpgradeExtraJumps();
        }

        for (int i = 0; i < jumpForceLevel; i++)
        {
            UpgradeJumpForce();
        }

        for (int i = 0; i < accelerationSpeedLevel; i++)
        {
            UpgradeAccelerationSpeed();
        }

        for (int i = 0; i < decelerationSpeedLevel; i++)
        {
            UpgradeDecelerationSpeed();
        }

        for (int i = 0; i < horizontalSpeedLevel; i++)
        {
            UpgradeHorizontalSpeed();
        }

        for (int i = 0; i < maxSpeedLevel; i++)
        {
            UpgradeMaxSpeed();
        }

        for (int i = 0; i < baseSpeedLevel; i++)
        {
            UpgradeBaseSpeed();
        }

        for (int i = 0; i < baseCoinValueLevel; i++)
        {
            UpgradeBaseCoinValue();
        }

        for (int i = 0; i < coinMultiplyerLevel; i++)
        {
            UpgradeCoinMultiplyer();
        }
    }
}
