using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Animator mAnimator = null;
    public Transform firePoint;
    public GameObject BulletPrefab;
    
    //IEnumerator waiter(){yield return new WaitForSeconds(2);}
    //System. Threading. Thread. Sleep(100);
   
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(BulletPrefab,firePoint.position,firePoint.rotation);
            if(mAnimator.gameObject.activeSelf)
            {
            mAnimator.SetTrigger("Shoot");
            }
            //GameObject tmp = Instantiate<GameObject>(BulletPrefab);
        }
    }
    //void Shoot()
    //{
    //    Instantiate(BulletPrefab, firePoint.position, firePoint.rotation);
    //}
    //IEnumerator waiter(){}
}
