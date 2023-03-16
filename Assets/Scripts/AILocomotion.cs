using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AILocomotion : Vehicle
{
    private CharacterController controller;
    private Rigidbody theRigidbody;

    // 角色每次移动距离
    private Vector3 moveDistance;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        theRigidbody = GetComponent<Rigidbody>();
        moveDistance = new Vector3(0, 0, 0);

        base.Start();
    }

    private void FixedUpdate()
    {
        // 计算速度
        velocity += acceleration * Time.fixedDeltaTime;
        // 限制速度，低于最大速度
        if (velocity.sqrMagnitude > sqrMaxSpeed)
            velocity = velocity.normalized * maxSpeed;
        moveDistance = velocity * Time.fixedDeltaTime;
        // 如果角色在平面移动，将y置为0
        if (isPlanar)
        {
            velocity.y = 0;
            moveDistance.y = 0;
        }

        // 如果存在控制器，利用控制器移动
        if (controller != null) controller.SimpleMove(velocity);
        // 如果没有控制器也没有刚体，或者刚体但需要动力学移动
        else if (theRigidbody == null || theRigidbody.isKinematic)
            transform.position += moveDistance;
        // 用刚体控制移动
        else theRigidbody.MovePosition(theRigidbody.position + moveDistance);

        // 更新朝向
        if (velocity.sqrMagnitude > 0.00001)
        {
            // 通过当前朝向与速度方向的插值，计算新方向
            Vector3 newForward = Vector3.Slerp(transform.forward, velocity, damping * Time.deltaTime);
            if (isPlanar) newForward.y = 0;
            // 将向前的方向设置为新朝向
            transform.forward = newForward;
        }

        // 播放动画
        Play();
    }


    void Play()
    {
    }
}