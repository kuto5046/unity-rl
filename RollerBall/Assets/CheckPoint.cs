using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    // Start is called before the first frame update
    public RaycastAgent agent;
    public int checkPointId;

    // 他のオブジェクトとの衝突開始時に呼ばれる
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.agent.EnterCheckPoint(this.checkPointId);
        }
    }
}
