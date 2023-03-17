using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringForSeek : Steering
{
    // 需要寻找的目标
    public GameObject target;

    // 预期速度
    private Vector3 desiredVelocity;

    // 获得被操控AI，便于查询AI角色最大速度信息
    private Vehicle m_vehicle;

    // 最大速度
    private float maxSpeed;

    // 是否平面运动
    private bool isPlanar;

    private void Start()
    {
        m_vehicle = GetComponent<Vehicle>();
        maxSpeed = m_vehicle.maxSpeed;
        isPlanar = m_vehicle.isPlanar;
    }

    
    public override Vector3 Force()
    {
        desiredVelocity = (target.transform.position - transform.position).normalized * maxSpeed;
        if (isPlanar)
            desiredVelocity.y = 0;
        return (desiredVelocity - m_vehicle.velocity);
    }
}