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

        BaseCharacter bodyAttach;
        bool isAnim = true;
        float totalHealth;

        public void DeActiveObject()
        {
            if (animHealthCorou != null)
            {
                StopCoroutine(animHealthCorou);
            }

            if (waitHideHealthBarCorou != null)
            {
                StopCoroutine(waitHideHealthBarCorou);
            }
            StopCoroutine(ShowHealthBarIfAuto());
            StopCoroutine(AnimHealthBar());
        }

        public void Setup(BaseCharacter bodyBase)
        {
            bodyAttach = bodyBase;
            totalHealth = bodyAttach.BaseStat.Health;
            UpdateHealthBar(null);
            this.RegisterEvent(EventID.OnPlayerTakeDamage,UpdateHealthBar);
            //gameObject.SetActive(!autoHideHealthBar);
        }

        public void UpdateHealthBar(object data)
        {
            if (bodyAttach == null)
            {
                return;
            }

            //if (bodyAttach.CharStats.Health <= 0)
            //{
            //    StopAllCoroutines();
            //    return;
            //}

            healthText.text = bodyAttach.BaseStat.Health.ToString();
            healthBarFill.fillAmount = (float)bodyAttach.BaseStat.Health / totalHealth;

            if (animHealthCorou != null)
            {
                StopCoroutine(animHealthCorou);
            }

            if (isAnim)
            {
                if (healthBarFill.fillAmount >= healthBarFillBg.fillAmount)
                {
                    healthBarFillBg.fillAmount = healthBarFill.fillAmount;
                }
                else
                {
                    animHealthCorou = StartCoroutine(AnimHealthBar());
                }
            }
            else
            {
                healthBarFillBg.fillAmount = healthBarFill.fillAmount;
            }

            if (autoHideHealthBar)
            {
                if (waitHideHealthBarCorou != null)
                {
                    StopCoroutine(waitHideHealthBarCorou);
                }

                StartCoroutine(ShowHealthBarIfAuto());
            }
        }

        IEnumerator AnimHealthBar()
        {
            while (healthBarFillBg.fillAmount > healthBarFill.fillAmount)
            {
                healthBarFillBg.fillAmount -= Time.deltaTime / 5;
                yield return null;
            }
        }

        IEnumerator ShowHealthBarIfAuto()
        {
            gameObject.SetActive(true);
            yield return new WaitForSeconds(2);
            gameObject.SetActive(false);
        }
    }
}