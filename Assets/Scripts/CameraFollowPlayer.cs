using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void followPlayer() {
        var vcam = GetComponent<CinemachineVirtualCamera>();
        vcam.Follow = GameManager.Instance.player.transform;
    }
}
