﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //this class is based off of the Tank Tutorials "TankManager"
    //class in the tanks tutorial on the Unity Website

    [SerializeField]
    private float dampTime = 0.2f;

    [SerializeField]
    private float screenEdgebuffer = 4f;

    [SerializeField]
    private float minimumSize = 6.5f;

    public Transform[] targets;

    public Camera camera;
    private float zoomSpeed;
    private Vector3 moveVelocity;
    private Vector3 desiredPosition;

    private void Awake()
    {
        camera = GetComponentInChildren<Camera>();
    }

    private void FixedUpdate()
    {
        Move();
        Zoom();
    }

    private void Move()
    {
        FindAveragePosition();
        transform.position = Vector3.SmoothDamp(transform.position,
            desiredPosition, ref moveVelocity, dampTime);
    }

    private void FindAveragePosition()
    {
        Vector3 averagePosition = new Vector3();
        int numberOfTargets = 0;

        for(int i = 0; i< targets.Length; i++)
        {
            if (!targets[i].gameObject.activeSelf)
                continue;

            averagePosition += targets[i].position;
            numberOfTargets++;
        }

        if (numberOfTargets > 0)
            averagePosition /= numberOfTargets;

        averagePosition.y = transform.position.y;

        desiredPosition = averagePosition;
    }

    private void Zoom()
    {
        float requiredSize = FindRequiredSize();
        camera.orthographicSize = Mathf.SmoothDamp(camera.orthographicSize,
            requiredSize, ref zoomSpeed, dampTime);
    }

    private float FindRequiredSize()
    {
        Vector3 desiredLocalPosition = transform.InverseTransformPoint(desiredPosition);
        float size = 0f;
        for (int i = 0; i < targets.Length; i++)
        {
            Vector3 targetLocalPosition = transform.InverseTransformPoint(targets[i].position);
            Vector3 desiredPositionToTarget = targetLocalPosition - desiredLocalPosition;

            size = Mathf.Max(size, Mathf.Abs(desiredPositionToTarget.y));
            size = Mathf.Max(size, Mathf.Abs(desiredPositionToTarget.x) / camera.aspect);
        }

        size += screenEdgebuffer;
        size = Mathf.Max(size, minimumSize);

        return size;
    }

    public void SetStartPositionAndSize()
    {
        FindAveragePosition();
        transform.position = desiredPosition;
        camera.orthographicSize = FindRequiredSize();
    }
}
