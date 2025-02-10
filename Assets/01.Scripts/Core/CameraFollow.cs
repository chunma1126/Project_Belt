using Unity.Cinemachine;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Unit[] targets;
    [SerializeField] private Transform follow;
    
    private CinemachineCamera _cinemachineCamera;
    private int targetIndex = 0;
    private void Start()
    {
        _cinemachineCamera = GetComponent<CinemachineCamera>();

        _cinemachineCamera.Follow = follow;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            NextTarget();
        }
                
        if (Input.GetKeyDown(KeyCode.A))
        {
            PrevTarget();
        }
    }

    private void NextTarget()
    {
        if(targetIndex >= targets.Length - 1)return;
        
        ++targetIndex;
        MoveTarget(targetIndex);
        
    }
    
    private void PrevTarget()
    {
        if(targetIndex <= 0)return;
        
        --targetIndex;
        MoveTarget(targetIndex);
        
    }

    private void MoveTarget(int _targetIndex)
    {
        follow = targets[_targetIndex].transform;
    }
    
    
}
