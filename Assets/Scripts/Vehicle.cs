using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    #region value

    // AI角色包含的操控行为列表
    private Steering[] steerings;

    // 最大速度
    public float maxSpeed = 10;

    // 可施加力最大值
    public float maxForce = 100;

    // 最大速度平方，预先算出来并存储节省资源
    protected float sqrMaxSpeed;

    // 质量
    public float mass = 1;

    // 速度
    public Vector3 velocity;

    // 转向时速度
    public float damping = 0.9f;

    // 操控力的计算间隔时间，为达更高帧率，不需要每帧更新
    public float computeInterval = 0.2f;

    // 是否在二维平面上，如果是，计算两个GO距离时忽略y值的不同
    public bool isPlanar = true;

    // 计算得到的力
    private Vector3 steeringForce;

    // 角色加速度
    protected Vector3 acceleration;

    // 计时器
    private float timer;

    #endregion value

    protected void Start()
    {
        steeringForce = new Vector3(0, 0, 0);
        sqrMaxSpeed = maxSpeed * maxSpeed;
        timer = 0;

        // 获得当前AI角色操控行为列表
        steerings = GetComponents<Steering>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        steeringForce = new Vector3(0, 0, 0);
        // 如果距离上次计算操控力时间大于设定时间间隔computeInterval,再次计算操控力
        if (timer > computeInterval)
        {
            // 把操控行为列表中的所有操控行为对应的操控力进行带权重的求和
            foreach (var s in steerings)
            {
                if (s.enabled) steeringForce += s.Force() * s.weight;
            }
        }

        // 使操控里不大于maxForce
        steeringForce = Vector3.ClampMagnitude(steeringForce, maxForce);

        // 力除以质量，求出加速度;
        acceleration = steeringForce / mass;

        // 重新从0开始计时;
        timer = 0;
    }
}