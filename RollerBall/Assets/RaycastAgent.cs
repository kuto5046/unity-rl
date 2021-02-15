using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;

// RollerAgnet
public class RaycastAgent : Agent
{
    // Start is called before the first frame update
    public Transform target;
    Rigidbody rBody;

    // 初期化時に呼ばれる
    public override void Initialize()
    {
        this.rBody = GetComponent<Rigidbody>();
    }

    // Episode開始時に呼ばれる
    public override void OnEpisodeBegin()
    {
        // RaycastAgentが床から落下している時
        if (this.transform.localPosition.y < 0)
        {
            // RaycastAgentの位置と速度をリセット
            this.rBody.angularVelocity = Vector3.zero;
            this.rBody.velocity = Vector3.zero;
            this.transform.localPosition = new Vector3(0.0f, 0.5f, 0.0f);
        }

        // Targetの位置のリセット
        target.localPosition = new Vector3(
        Random.value*8-4, 0.5f, Random.value*8-4);
    }

    // 行動実行時に呼ばれる
    public override void OnActionReceived(float[] vectorAction)
    {
        // RaycastAgentに力を加える
        Vector3 dirToGo = Vector3.zero;
        Vector3 rotateDir = Vector3.zero;
        int action = (int)vectorAction[0];
        if (action==1) dirToGo == transform.forward; 
        if (action==2) dirToGo == transform.forward * -1.0f; 
        if (action==3) rotateDir == transform.up * -1.0f; 
        if (action==4) rotateDir == transform.up;
        this.transform.Rotate(rateteDir, Time.deltaTime * 200f);
        this.rBody.AddForce(dirToGo * 0.4f, ForceMode.VelocityChange);

        // RaycastAgentがTargetの位置に到着
        float distanceToTarget = Vector3.Distance(
            this.transform.localPosition, target.localPosition);
        if (distanceToTarget < 1.42f)
        {
            AddReward(1.0f);
            EndEpisode();
        }

        // RaycastAgentが落下
        if (this.transform.localPosition.y < 0)
        {
            EndEpisode();
        }
    }

    
    public override void Heuristic(float[] actionsOut)
    {   
        actionOut[0] = 0;
        if (Input.GetKey(KeyCode.UpArrow)) actionOut[0] = 1;
        if (Input.GetKey(KeyCode.DownArrow)) actionOut[0] = 2;
        if (Input.GetKey(KeyCode.LeftArrow)) actionOut[0] = 3;
        if (Input.GetKey(KeyCode.RightArrow)) actionOut[0] = 4;
        actionsOut[1] = Input.GetAxis("Vertical");
    }
}