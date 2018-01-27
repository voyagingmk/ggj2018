﻿using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class circleCtrl : MonoBehaviour
{
    public List<int> collideList;
    public int roleID = 0;
    public float Radius = 6;          //外半径  
    public float innerRadius = 3;     //内半径
    public float angleDegree = 360;   //扇形或扇面的角度
    public int Segments = 60;         //分割数  
    public float end = 15.0f;
    public float r = 0.01f;
    public float ir = 0.005f;
    public float Spd = 0.3f;
    public float len = 2.0f;
    private MeshFilter meshFilter;

    void Start()
    {

    }

    void Update()
    {
        if (r <= ir || Spd <= 0)
        {
            meshFilter.mesh = new Mesh();
            Destroy(this.gameObject);
            return;
        }
        meshFilter = GetComponent<MeshFilter>();
        if (r >= end)
        {
            Spd -= 0.5f;
            Spd = Mathf.Max(0, Spd);
            float spd = Time.deltaTime * Spd;
            ir += spd;
        }
        else
        {
            float spd = Time.deltaTime * Spd;
            r += spd;
            ir += spd;
            ir = Mathf.Max(0.0f, Mathf.Min(r - len, ir));
        }
        meshFilter.mesh = CreateMesh(r, ir, angleDegree, Segments);

    }

    Mesh CreateMesh(float radius, float innerradius, float angledegree, int segments)
    {
        //vertices(顶点):
        int vertices_count = segments * 2 + 2;              //因为vertices(顶点)的个数与triangles（索引三角形顶点数）必须匹配
        Vector3[] vertices = new Vector3[vertices_count];
        float angleRad = Mathf.Deg2Rad * angledegree;
        float angleCur = angleRad;
        float angledelta = angleRad / segments;
        for (int i = 0; i < vertices_count; i += 2)
        {
            float cosA = Mathf.Cos(angleCur);
            float sinA = Mathf.Sin(angleCur);

            vertices[i] = new Vector3(radius * cosA, 0, radius * sinA);
            vertices[i + 1] = new Vector3(innerradius * cosA, 0, innerradius * sinA);
            angleCur -= angledelta;
        }

        //triangles:
        int triangle_count = segments * 6;
        int[] triangles = new int[triangle_count];
        for (int i = 0, vi = 0; i < triangle_count; i += 6, vi += 2)
        {
            triangles[i] = vi;
            triangles[i + 1] = vi + 3;
            triangles[i + 2] = vi + 1;
            triangles[i + 3] = vi + 2;
            triangles[i + 4] = vi + 3;
            triangles[i + 5] = vi;
        }

        //uv:
        Vector2[] uvs = new Vector2[vertices_count];
        for (int i = 0; i < vertices_count; i++)
        {
            uvs[i] = new Vector2(vertices[i].x / radius / 2 + 0.5f, vertices[i].z / radius / 2 + 0.5f);
        }

        //负载属性与mesh
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
        return mesh;
    }
}