public enum DamageType 
{ 
    Default,
    OverTime
}
[System.Serializable]
public class DamageValue
{
    public DamageType damageType;
    public int value;
    public bool damageAble;
    public DamageValue(DamageType type, bool damageAble, int value)
    {
        damageType = type;
        this.damageAble = damageAble;
        this.value = value;
    }
}