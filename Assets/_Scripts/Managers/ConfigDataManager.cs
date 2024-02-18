using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigDataManager : SingletonMono<ConfigDataManager>
{
    // Config data is the base data when you start game fresh new
    public DataAssets DataAssets => dataAssets;
    [SerializeField] private DataAssets dataAssets;
}
