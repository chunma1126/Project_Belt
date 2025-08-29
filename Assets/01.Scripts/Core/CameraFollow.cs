using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Unit[] targets;
    [SerializeField] private Sprite[] targetHeadSprites;
    [SerializeField] private Vector2[] headSizes;
    
    [SerializeField] private Image targetHeadImage;

    private Unit prevUnit;
    private CinemachineCamera _cinemachineCamera;
    private int targetIndex = 0;
    private void Start()
    {
        _cinemachineCamera = GetComponent<CinemachineCamera>();

        MoveTarget(0);
    }
    
    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.N))
        {
            NextTarget();
        }*/
    }

    public void NextTarget()
    {
        ++targetIndex;
        targetIndex %= targets.Length;
        
        MoveTarget(targetIndex);
    }
    
    private void MoveTarget(int _targetIndex)
    {
        targetHeadImage.sprite = targetHeadSprites[(_targetIndex + 1) % targets.Length];
        targetHeadImage.rectTransform.sizeDelta = headSizes[(_targetIndex + 1) % targets.Length];
        
        _cinemachineCamera.Follow = targets[_targetIndex].transform;
        
        if(prevUnit != null)
            prevUnit.SelectUnit();
        
        targets[_targetIndex].SelectUnit();
        prevUnit = targets[_targetIndex];
    }
}
