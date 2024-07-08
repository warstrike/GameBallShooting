using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IkTests : MonoBehaviour
{
    [Range(0,1f)]
 public   float leftFootPositionWeights;
    [Range(0,1f)]
    public  float rightFootPositionWeights;
    [Range(0,1f)]
    public float leftHandPositionWeights;
    [Range(0,1f)]
    public  float rightHandPositionWeights;
    
    public Transform leftFootObj;
    public Transform RightFootObj;
    
    public Transform leftHandObj;
    public Transform RightHandObj;
    private Animator animator;

    public bool LeftFootUseRotation=false;
    public bool RightFootUseRotation=false;
    public bool LeftHandUseRotation=false;
    public bool RightHandUseRotation=false;
    private float GlobalWeight=0;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnAnimatorIK(int layerIndex)
    {
        float  leftFootPositionWeight = Mathf.Min(leftFootPositionWeights, GlobalWeight);
        float  rightFootPositionWeight = Mathf.Min(rightFootPositionWeights, GlobalWeight);
        float  leftHandPositionWeight = Mathf.Min(leftHandPositionWeights, GlobalWeight);
        float  rightHandPositionWeight = Mathf.Min(rightHandPositionWeights, GlobalWeight);
       // animator.SetIKHintPosition(AvatarIKHint.);
        if (leftFootObj)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, leftFootPositionWeight);
            animator.SetIKPosition(AvatarIKGoal.LeftFoot, leftFootObj.position);
            if (LeftFootUseRotation)
            {
                animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, leftFootPositionWeight);
                animator.SetIKRotation(AvatarIKGoal.LeftFoot, leftFootObj.localRotation);
            }
        }
        if (RightFootObj)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, rightFootPositionWeight);
            animator.SetIKPosition(AvatarIKGoal.RightFoot, RightFootObj.position);
            if (RightFootUseRotation)
            {
                animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, rightFootPositionWeight);
                animator.SetIKRotation(AvatarIKGoal.RightFoot, RightFootObj.localRotation);
            }
        }
        if (leftHandObj)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, leftHandPositionWeight);
            animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandObj.position);
            if (LeftHandUseRotation)
            {
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, leftHandPositionWeight);
                animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandObj.rotation);
            }
        }
        if (RightHandObj)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, rightHandPositionWeight);
            animator.SetIKPosition(AvatarIKGoal.RightHand, RightHandObj.position);
            if (RightHandUseRotation)
            {
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, rightHandPositionWeight);
                animator.SetIKRotation(AvatarIKGoal.RightHand, RightHandObj.rotation);
            }
        }
        
       
    }

    public void StartBlend()
    {
        StartCoroutine(StartBlends());
    }

    public void StopBlend(bool definitive=false)
    {
        StartCoroutine(StopBlends(definitive));
    }

    IEnumerator StartBlends()
    {
        float speed = 0.05f;
        float curentValue = 0;
        while (curentValue<=1)
        {
            curentValue += speed;
            
            yield return new WaitForSeconds(0.01f);
            GlobalWeight = curentValue;
        }
        yield return null;
    }
    IEnumerator StopBlends(bool definitive=false)
    {
        float speed = 0.05f;
        float curentValue = GlobalWeight;
        while (curentValue>=0)
        {
            curentValue -= speed;
            
            yield return new WaitForSeconds(0.02f);
            GlobalWeight = curentValue;
        }

        if (definitive)
        {
            this.enabled = false;
        }
        yield return null;
    }

   
}
