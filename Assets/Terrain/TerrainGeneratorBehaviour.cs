using UnityEngine;
using System;

public class TerrainGeneratorBehaviour : MonoBehaviour
{
    Mesh mesh;
    Vector3 [] vertices;
    MeshFilter meshFilter;
    MeshCollider meshCollider;
    Vector2 [] uv;
    int [] triangles;
    public int terrainXSize = 20;
    public int terrainZSize = 20;
    public int cellXSize = 10;
    public int cellZSile = 10;
    public float height = 15;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mesh = new Mesh();
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();
        meshFilter.mesh = mesh;
        

        GenerateVertices();
        GenerateTriangles();
        meshCollider.sharedMesh  = mesh;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateTriangles(){

        int vert = 0;
        int tris = 0;
        int depth = terrainXSize;
        triangles = new int[6*terrainXSize*terrainZSize];

        for(int j =0; j < terrainZSize; j++){
            for(int i =0; i < terrainXSize; i++){
                
                triangles[tris++] = vert;
                triangles[tris++] = vert + depth+1;
                triangles[tris++] = vert+1;
                triangles[tris++] = vert+1;
                triangles[tris++] = vert+depth+1;
                triangles[tris++] = vert+depth+2;
                // i+1
                // i+depth
                // i + depth +1
                vert++;
            }
            vert++;
        }
        mesh.triangles  = triangles ;

    }


    void GenerateVertices(){
        int i = 0;
        vertices = new Vector3[(terrainXSize+1)*(terrainZSize+1)];
        uv = new Vector2[vertices.Length];
        System.Random rnd = new System.Random();
        for(int z =0; z <= terrainZSize; z++){
            for(int x =0; x <= terrainXSize; x++){
                float xx = (x-terrainXSize/2) * cellXSize;
                float zz = (z-terrainZSize/2) * cellZSile;
                //Mathf.Sin(xx+zz*terrainXSize) +
                float yy = ( (float) rnd.NextDouble ()/3f + Mathf.PerlinNoise(x,z))*height;

                uv[i] = new Vector2( (float)x / terrainXSize, (float)z/terrainZSize);
                vertices[i++] = new Vector3(xx,yy,zz);
            }
        }
        mesh.vertices = vertices;
        mesh.uv = uv;
    }

    void OnDrawGismos(){
        if (vertices != null)
            for(int i =0; i < vertices.Length; i++){
                Gizmos.DrawSphere(vertices[i], 0.1f);
            }

    }

}
