using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BlendShapeAnimation : AnimationElementPart
{
    public SkinnedMeshRenderer Mesh;
    public int index;
    public float Speed;
    public UnityEvent OnComplete;
    public float EndValue;
    void Start()
    {
        
    }
    [EasyButtons.Button]
    public override void Play()
    {
        StartCoroutine(Animation());
    }

    IEnumerator Animation()
    {
        float StartValue = Mesh.GetBlendShapeWeight(index);
        float curentValue = 0;
        while (curentValue< 1f)
        {
            curentValue += Time.deltaTime * Speed;
            Mesh.SetBlendShapeWeight(index,Mathf.Lerp(StartValue,EndValue,curentValue));
            yield return new WaitForEndOfFrame();
        }
        OnComplete?.Invoke();
        yield return new WaitForEndOfFrame();
    }
}
