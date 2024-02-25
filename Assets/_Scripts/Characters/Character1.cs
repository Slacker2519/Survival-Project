using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character1 : BaseCharacter
{



    public List<IBuff> Buffs;

    private void Start()
    {
        Buffs[0].Shoot();
        TakeDamage(70);
    }
    private void Update()
    {
        MoveCharacter();
    }

    public override void TakeDamage(long dame)
    {
        base.TakeDamage(dame);
        this.PostEvent(EventID.OnPlayerTakeDamage);
    }



    public class UImanager : MonoBehaviour
    {

        private void Start()
        {
            this.RegisterEvent(EventID.OnPlayerTakeDamage, WhenPlayerTakeDameUPdateUI);
        }
        /// <summary>
        /// Example event excute when player take damage.
        /// </summary>
        public void WhenPlayerTakeDameUPdateUI(object a)
        {

        }
    }


}

public interface IBuff
{
    void Shoot();
}

public class ShotGun : MonoBehaviour,IBuff
{
    public GameObject bullet;
    public void Shoot()
    {

        Debug.Log("ban tum lum");
    }
}

public class MachineGun : IBuff
{

    public void Shoot()
    {
        Debug.Log("ban lien thanh");
    }
}
