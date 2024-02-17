using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigDataManager : SingletonMono<ConfigDataManager>
{
    public DataAssets DataAssets => dataAssets;
    private DataAssets dataAssets;
}
