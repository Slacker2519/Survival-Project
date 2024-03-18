using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Elite.GangGang.Utils
{
    public static class GameUtilities
    {
        public static List<string> goldText = new List<string>
        {
            "", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v"
        };

        public static List<string> countryCodeEU = new List<string>
        {
            "AT", "BE", "BG","HR","CY","CZ","DK","EE","FI","FR","DE","GR","HU","IE","IT","LT","LU","MT","NL","PL","PT","RO","SK","ES","SE"
        };

        public static List<int> progressMax = new List<int>
        {
            0,3,4,5,9,14,16,19,21,1000
        };

        public static Dictionary<int, int> WeedOrder = new Dictionary<int, int>
        {
            { 4, 30 },
            { 2, 20 }
        };

        public static Dictionary<int, int> VapeOrder = new Dictionary<int, int>
        {
            { 4, 9 },
            { 2, 6 },
            { 0, 4 }
        };

        //public static Resource GetOrder(int weedLevel, int vapeLevel)
        //{
        //    int num = 0;
        //    Resource resource = new Resource();

        //    if (vapeLevel >= 0)
        //    {
        //        num = (int)Random.Range(0, 2f);
        //    }

        //    if (num == 0)
        //    {
        //        resource.resourceName = ResourceEnums.Weed;
        //        foreach (var key in WeedOrder.Keys)
        //        {
        //            var tmp = weedLevel - key;
        //            if (tmp >= 0)
        //            {
        //                resource.amount = WeedOrder[key];
        //                return resource;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        resource.resourceName = ResourceEnums.Vape;
        //        foreach (var key in VapeOrder.Keys)
        //        {
        //            var tmp = vapeLevel - key;
        //            if (tmp >= 0)
        //            {
        //                resource.amount = VapeOrder[key];
        //                return resource;
        //            }
        //        }
        //    }

        //    return null;
        //}

        public static T GetComponentByName<T>(GameObject go, string name)
            where T : Component
        {
            T[] buffer = go.GetComponentsInChildren<T>(true);
            if (buffer != null)
            {
                for (int i = 0; i < buffer.Length; i++)
                {
                    if (buffer[i] != null && buffer[i].name == name)
                    {
                        return buffer[i];
                    }
                }
            }
            return null;
        }

        public static GameObject GetGameObjectByName(GameObject objInput, string strFindName)
        {
            GameObject ret = null;
            if (objInput != null)
            {
                Transform[] objChildren = objInput.GetComponentsInChildren<Transform>(true);
                if (objChildren != null)
                {
                    for (int i = 0; i < objChildren.Length; ++i)
                    {
                        if ((objChildren[i].name == strFindName))
                        {
                            ret = objChildren[i].gameObject;
                            break;
                        }
                    }
                }
            }
            return ret;
        }

        public static void CreateDir(DirectoryInfo dir)
        {
            var parent = dir.Parent;
            if (!parent.Exists)
                CreateDir(parent);

            dir.Create();
        }

        public static FileStream CreateFile(string filePath)
        {
            string dirPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(dirPath))
            {
                CreateDir(new DirectoryInfo(dirPath));
            }
            return new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Write, 1024 * 2, false);
        }

        public static string ConvertGold(double num, string free ="")
        {
            int index = 0;

            while (num >= 1000)
            {
                num /= 1000;
                index++;
            }
            num = Math.Round(num, 2);
            if(!string.IsNullOrEmpty(free) && num <= 0)
            {
                return free;
            }
            return $"{num}{goldText[index]}";
        }

        //public static bool IsMaximumHire(CharacterEnums characterName)
        //{
        //    double maximum = 0;
        //    if(characterName == CharacterEnums.Operator)
        //    {
        //        foreach (var key in GameController.ReqirementOperator.Keys)
        //        {
        //            maximum += GameController.ReqirementOperator[key];
        //        }
        //        return maximum <= GameManager.GetPlayerResource((ResourceEnums)characterName);
        //    }
        //    else if(characterName == CharacterEnums.Distributor)
        //    {
        //        maximum = GameController.MaxDistribuitor;
        //        if(GameManager.Instance.GameData.TutorialStep < 5002)
        //        {
        //            return true;
        //        }
        //        return maximum <= GameManager.GetPlayerResource((ResourceEnums)characterName) + GameManager.Instance.GameData.Distributors.Count;
        //    }
        //    return false;
        //}

        public static void SetText(this Text text, string message)
        {
            text.text = message;
            if (text.verticalOverflow == VerticalWrapMode.Truncate || text.horizontalOverflow == HorizontalWrapMode.Wrap)
                return;
            int totalLength = 0;
            Font myFont = text.font;  //chatText is my Text component
            CharacterInfo characterInfo = new CharacterInfo();

            char[] arr = message.ToCharArray();

            foreach (char c in arr)
            {
                myFont.RequestCharactersInTexture(text.text, text.fontSize, text.fontStyle);
                myFont.GetCharacterInfo(c, out characterInfo, text.fontSize);

                totalLength += characterInfo.advance;
            }
            text.rectTransform.sizeDelta = new Vector2(totalLength, text.rectTransform.sizeDelta.y);
        }

        public static void SetTMPText(this TMP_Text text, string message)
        {
            text.text = message;
            text.rectTransform.sizeDelta = text.GetPreferredValues();
        }

        public static bool IsNewDate(long timeCheck)
        {
            DateTimeOffset timeCheckSpan = DateTimeOffset.FromUnixTimeSeconds(timeCheck);
            DateTimeOffset nowTime = DateTimeOffset.Now;
            if (Mathf.Abs(timeCheckSpan.Day - nowTime.Day) == 0)
            {
                if (Mathf.Abs(timeCheckSpan.Month - nowTime.Month) == 0)
                {
                    if (Math.Abs(timeCheckSpan.Year - nowTime.Year) == 0)
                        return false;
                }
                return true;
            }
            return true;
        }

        public static string LeftTimeToString(int leftTime)
        {
            int hours = (int)leftTime / 3600;
            int mins = (int)((leftTime - hours * 3600) / 60);
            int secs = (int)(leftTime - hours * 3600 - mins * 60);
            return string.Format("{0}h{1}m{2}s", hours < 10 ? "0" + hours : hours.ToString(), mins < 10 ? "0" + mins : mins.ToString(), secs < 10 ? "0" + secs : secs.ToString());
        }

        //public static AudioType GetBackgroundMusic(int stageId)
        //{
        //    //string str = $"Background{stageId}";
        //    string str = $"BackgroundZoo";
        //    AudioType result;
        //    return Enum.TryParse<AudioType>(str, true, out result) ? result : AudioType.MaxValue;
        //}

        public static IEnumerator ScrollToTop(this ScrollRect scrollRect)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(scrollRect.content.GetComponent<RectTransform>());
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            scrollRect.verticalNormalizedPosition = 1;
        }

        public static IEnumerator GetCountry(Action<string> countryCode)
        {
            string uri = $"http://www.geoplugin.net/json.gp";

            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {
                yield return webRequest.SendWebRequest();
                if (webRequest.result != UnityWebRequest.Result.Success)
                {
                    countryCode?.Invoke("");
                    Debug.LogError("Can't get country code due to network error:" + webRequest.error);
                }
                else
                {
                    var jsonData = webRequest.downloadHandler.text;
                    try
                    {
                        GeoLocation geolocation = JsonUtility.FromJson<GeoLocation>(jsonData);
                        countryCode?.Invoke(geolocation.geoplugin_countryCode);
                    }
                    catch(Exception ex)
                    {
                        Debug.LogError($"Can't parse json data: {jsonData} || error:" + ex.Message);
                        countryCode?.Invoke("");
                    }
                }
            }
        }

        #region Get enemies number

        public static List<int> MaxEnemyNumber = new List<int> { 20, 30, 40, 50, 100 };
        public static List<float> SpawnEnemyDuration = new List<float> { .2f, .2f, .2f, .2f, 
            .1f, .1f, .1f, .1f, .1f, .1f, .1f, .1f, .1f };
        public static List<int> EnemySpawnNumber = new List<int> { 2, 3, 4, 5, 6, 10, 11, 12, 13, 17, 18, 19, 20 };

        public static int GetEnemyMaxNumber(int waveNumber)
        {
            if (waveNumber >= MaxEnemyNumber.Count)
            {
                return 100;
            }
            return MaxEnemyNumber[waveNumber];
        }

        public static float GetEnemySpawnDuration(int waveNumber)
        {
            if (waveNumber >= SpawnEnemyDuration.Count)
            {
                return 0.1f;
            }
            return SpawnEnemyDuration[waveNumber];
        }

        public static int GetEnemySpawnNumber(int waveNumber)
        {
            if (waveNumber >= EnemySpawnNumber.Count)
            {
                return 20;
            }
            return EnemySpawnNumber[waveNumber];
        }

        #endregion
    }

    public class GeoLocation
    {
        public string geoplugin_request;
        public string geoplugin_city;
        public string geoplugin_region;
        public string geoplugin_regionCode;
        public string geoplugin_regionName;
        public string geoplugin_areaCode;
        public string geoplugin_dmaCode;
        public string geoplugin_countryCode;
        public string geoplugin_countryName;
        public int geoplugin_inEU;
        public string geoplugin_continentCode;
        public string geoplugin_continentName;
        public string geoplugin_timezone;
    }
}