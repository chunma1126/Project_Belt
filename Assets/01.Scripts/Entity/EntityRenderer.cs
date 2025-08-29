using System.Collections;
using DG.Tweening;
using UnityEngine;

public class EntityRenderer : MonoBehaviour,IEntityComponent
{
    [SerializeField] private Transform root;
    
    private SpriteRenderer _spriteRenderer;
    private Material _originMaterial;
    
    private Entity entity;
    
    public int lookDirection = 1;
    public bool lookRight = true;

    private static readonly int OutlineThicknessID = Shader.PropertyToID("_OutlineThickness");
    
    public void Initialize(Entity _entity)
    {
        entity = _entity;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originMaterial = _spriteRenderer.material;
    }
    

    public void Flip()
    {
        lookDirection *= -1;
        lookRight = !lookRight;
        
        root.Rotate(0,180 ,0);
    }
    
    
    public void ChangeMaterial(Material _mat)
    {
        _spriteRenderer.material = _mat;
    }
    
    public void ChangeMaterial(Material _mat , float _time)
    {
        StartCoroutine(ChangeMatRoutine(_mat,_time));
    }
    
    private IEnumerator ChangeMatRoutine(Material _mat , float _time)
    {
        _spriteRenderer.material = _mat;
        yield return new WaitForSeconds(_time);
        _spriteRenderer.material = _originMaterial;
    }

    public void SelectEntity(bool _isSelect)
    {
        _originMaterial.SetFloat(OutlineThicknessID, _isSelect ? 1 : 0);
    }
    
    
}
