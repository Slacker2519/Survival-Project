using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Gameplay
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] protected Image healthBarFillBg;
        [SerializeField] protected Image healthBarFill;
        [SerializeField] protected TMP_Text healthText;

        [SerializeField] bool autoHideHealthBar;

        Coroutine animHealthCorou;
        Coroutine waitHideHealthBarCorou;

        ////BodyBase bodyAttach;


        //public void DeActiveObject()
        //{
        //    if (animHealthCorou != null)
        //    {
        //        StopCoroutine(animHealthCorou);
        //    }

        //    if (waitHideHealthBarCorou != null)
        //    {
        //        StopCoroutine(waitHideHealthBarCorou);
        //    }
        //    StopCoroutine(ShowHealthBarIfAuto());
        //    StopCoroutine(AnimHealthBar());
        //}

        //public void Setup(BodyBase bodyBase)
        //{
        //    bodyAttach = bodyBase;
        //    gameObject.SetActive(!autoHideHealthBar);
        //}

        //public void UpdateHealthBar(bool isAnim)
        //{
        //    if (bodyAttach == null)
        //    {
        //        return;
        //    }

        //    if (bodyAttach.CurrentHealth <= 0)
        //    {
        //        StopAllCoroutines();
        //        return;
        //    }

        //    healthText.text = bodyAttach.CurrentHealth.ToString();
        //    healthBarFill.fillAmount = (float)bodyAttach.CurrentHealth / bodyAttach.TotalHealth;

        //    if (animHealthCorou != null)
        //    {
        //        StopCoroutine(animHealthCorou);
        //    }

        //    if (isAnim)
        //    {
        //        if (healthBarFill.fillAmount >= healthBarFillBg.fillAmount)
        //        {
        //            healthBarFillBg.fillAmount = healthBarFill.fillAmount;
        //        }
        //        else
        //        {
        //            animHealthCorou = StartCoroutine(AnimHealthBar());
        //        }
        //    }
        //    else
        //    {
        //        healthBarFillBg.fillAmount = healthBarFill.fillAmount;
        //    }

        //    if (autoHideHealthBar)
        //    {
        //        if (waitHideHealthBarCorou != null)
        //        {
        //            StopCoroutine(waitHideHealthBarCorou);
        //        }

        //        StartCoroutine(ShowHealthBarIfAuto());
        //    }
        //}

        //IEnumerator AnimHealthBar()
        //{
        //    while (healthBarFillBg.fillAmount > healthBarFill.fillAmount)
        //    {
        //        healthBarFillBg.fillAmount -= Time.deltaTime / 5;
        //        yield return null;
        //    }
        //}

        //IEnumerator ShowHealthBarIfAuto()
        //{
        //    gameObject.SetActive(true);
        //    yield return new WaitForSeconds(2);
        //    gameObject.SetActive(false);
        //}
    }
}