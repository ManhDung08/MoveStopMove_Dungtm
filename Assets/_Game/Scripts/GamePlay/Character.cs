using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator anim;


    [SerializeField] protected SkinnedMeshRenderer characterMesh;
    [SerializeField] protected Transform weaponTransform;
    [SerializeField] protected LayerMask characterLayer;
    [SerializeField] protected GameObject attackRange; 
    [SerializeField] protected WeaponData weaponData;
    [SerializeField] protected PantData pantData;


    protected GameObject weaponPrefab;
    protected SkinnedMeshRenderer PantMesh;
    protected bool isMoving = false;
    protected bool isAttacking = false;
    protected bool isDead = false;
    protected float delayAttacktime = 0.333f;  //tg attack bắt đầu sau khi animation attack trigger
    protected float cooldownAttacktime = 1f;

    public bool IsDead => isDead;

    protected void Start()
    {
        OnInit();
    }
    protected enum AnimationName
    {
        idle,
        run, 
        attack, 
        win, 
        dead
    }

    protected AnimationName currentAninName = AnimationName.idle;

    protected void ChangeAnim(AnimationName animationName)
    {
        if(currentAninName != animationName)
        {
            anim.ResetTrigger(currentAninName.ToString());
            currentAninName = animationName;
            anim.SetTrigger(currentAninName.ToString());
        }
    }

    protected void Attack(GameObject enemy)
    {
        if (!isAttacking && !isDead)
        {
            Vector3 direction = new Vector3(enemy.transform.position.x - transform.position.x, 0f, enemy.transform.position.z - transform.position.z);
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
            ChangeAnim(AnimationName.attack);
            isAttacking = true;
            StartCoroutine(SpawnWeapon(delayAttacktime, enemy));
        }
    }

    protected IEnumerator SpawnWeapon(float delayTime, GameObject enemy)
    {
        yield return new WaitForSeconds(delayTime);
        weaponTransform.gameObject.SetActive(false);
        //Direction of weapon
        Vector3 enemyDirection = new Vector3(enemy.transform.position.x - weaponTransform.position.x, 0f, enemy.transform.position.z - weaponTransform.position.z);
        Quaternion targetRotation = Quaternion.LookRotation(enemyDirection, Vector3.up);
        targetRotation.eulerAngles = new Vector3(-90f, targetRotation.eulerAngles.y, targetRotation.eulerAngles.z);

        GameObject weaponInstance = Instantiate(weaponPrefab, weaponTransform.position, targetRotation);
        weaponInstance.GetComponent<WeaponController>().AttackToDirection(enemyDirection);
        StartCoroutine(ResetAttack(cooldownAttacktime));    
    }

    protected IEnumerator ResetAttack(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        if (!isDead)
        {
            ChangeAnim(AnimationName.idle);
            weaponTransform.gameObject.SetActive(true);
            isAttacking = false;
            yield return new WaitForSeconds(delayTime);
        }
    }


    protected virtual void OnInit()
    {
        attackRange.SetActive(true);
    }


    public void OnDeath()
    {
        ChangeAnim(AnimationName.dead);
        isDead = true;
        StartCoroutine(DeactiveSelf());
    }

    private IEnumerator DeactiveSelf()
    {
        yield return new WaitForSeconds(2.2f);
        gameObject.SetActive(false);
    }
}
