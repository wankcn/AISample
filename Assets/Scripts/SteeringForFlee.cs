using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringForFlee : Steering
{
    public GameObject target;

    // 设置AI预警并开始逃跑的范围
    public float fearDistance = 20;
    private Vector3 desiredVelocity; // 预期速度
    private Vehicle m_vehicle;
    private float maxSpeed;

    private void Start()
    {
        m_vehicle = GetComponent<Vehicle>();
        maxSpeed = m_vehicle.maxSpeed;
    }

    public override Vector3 Force()
    {
        Vector3 tmpPos = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 tmpTargetPos = new Vector3(target.transform.position.x, 0, target.transform.position.z);
        // 如果AI与目标距离大于离开距离，返回0向量
        if (Vector3.Distance(tmpPos, tmpTargetPos) > fearDistance)
            return new Vector3(0, 0, 0);
        // 如果距离<逃跑距离 计算逃跑需要的操控向量
        desiredVelocity = (transform.position - target.transform.position).normalized * maxSpeed;
        return (desiredVelocity - m_vehicle.velocity);
    }
}