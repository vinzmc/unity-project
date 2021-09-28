using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ChallengeFive : MonoBehaviour
{
    public int Size;
    public Material material;
    private Vector3[] vertices;
    private Mesh mesh;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Generate());
        material = GetComponent<MeshRenderer>().material;
        material = Resources.Load<Material>("ProceduralGrid/Materials/UV");
        // material.shader = Shader.Find("Unlit/Texture");
        material.shader = Shader.Find("Standard");
        material.mainTexture = Resources.Load<Texture>("ProceduralGrid/Textures/UV");
        GetComponent<MeshRenderer>().material = material;
    }
    //IEnumerator interface to produce vertices one by one in Scene View
    private IEnumerator Generate()
    {
        //delay for simulation proccess
        WaitForSeconds wait = new WaitForSeconds(0.1f);
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";

        //define the number of vertices
        vertices = new Vector3[(Size + 1) * (Size + 1) * 6];
        var verticesTemp = new List<Vector3>();

        //declare UVs Vector and tangent vector
        Vector2[] uv = new Vector2[vertices.Length];

        // define tangent vector
        Vector4[] tangents = new Vector4[vertices.Length];

        //define tangent direction
        Vector4 tangent = new Vector4(1f, 1f, 0f, -1f);


        // kodingan frankenstein (banyak dosanya)
        //z-
        int i = 0;
        for (int y = 0; y <= Size; y++)
        {
            for (int x = 0; x <= Size; x++, i++)
            {
                vertices[i] = new Vector3(x, y);
                verticesTemp.Add(new Vector3(x, y));
                tangents[i] = tangent;
                uv[i] = new Vector2((float)x / Size, (float)y / Size);
                yield return wait;
            }
        }
        //x+
        for (int y = 0; y <= Size; y++)
        {
            for (int x = 0; x <= Size; x++, i++)
            {
                vertices[i] = new Vector3(Size, y, x);
                verticesTemp.Add(new Vector3(Size, y, x));
                tangents[i] = tangent;
                uv[i] = new Vector2((float)x / Size, (float)y / Size);
                yield return wait;
            }
        }
        //z+
        for (int y = 0; y <= Size; y++)
        {
            for (int x = Size; x >= 0; x--, i++)
            {
                vertices[i] = new Vector3(x, y, Size);
                verticesTemp.Add(new Vector3(x, y, Size));
                tangents[i] = tangent;
                uv[i] = new Vector2((float)x / Size, (float)y / Size);
                yield return wait;
            }
        }
        //x-
        for (int y = 0; y <= Size; y++)
        {
            for (int x = Size; x >= 0; x--, i++)
            {
                vertices[i] = new Vector3(0, y, x);
                verticesTemp.Add(new Vector3(0, y, x));
                tangents[i] = tangent;
                uv[i] = new Vector2((float)x / Size, (float)y / Size);
                yield return wait;
            }
        }
        //y+
        for (int y = 0; y <= Size; y++)
        {
            for (int x = 0; x <= Size; x++, i++)
            {
                vertices[i] = new Vector3(x, Size, y);
                verticesTemp.Add(new Vector3(x, Size, y));
                tangents[i] = tangent;
                uv[i] = new Vector2((float)x / Size, (float)y / Size);
                yield return wait;
            }
        }
        //y-
        for (int y = 0; y <= Size; y++)
        {
            for (int x = Size; x >= 0; x--, i++)
            {
                vertices[i] = new Vector3(x, 0, y);
                verticesTemp.Add(new Vector3(x, 0, y));
                tangents[i] = tangent;
                uv[i] = new Vector2((float)x / Size, (float)y / Size);
                yield return wait;
            }
        }
        mesh.SetVertices(verticesTemp);
        mesh.uv = uv;

        //procedurally generated triangles
        int[] triangles = new int[Size * Size * 36];
        int ti = 0;
        int vi = 0;
        for (int k = 0; k < 6; k++)
        {
            for (int y = 0; y < Size; y++, vi++)
            {
                for (int x = 0; x < Size; x++, ti += 6, vi++)
                {
                    triangles[ti] = vi;
                    triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                    triangles[ti + 4] = triangles[ti + 1] = vi + Size + 1;
                    triangles[ti + 5] = vi + Size + 2;
                    mesh.triangles = triangles;
                    yield return wait;
                }
            }
            vi += Size + 1;
        }
        //assigning normals and tangents
        mesh.tangents = tangents;
        mesh.RecalculateNormals();
    }

    //Gizmos are visual aid for Unity in Scene View
    private void OnDrawGizmos()
    {
        if (vertices != null)
        {
            Gizmos.color = Color.black;
            for (int i = 0; i < vertices.Length; i++)
            {
                Gizmos.DrawSphere(vertices[i], 0.1f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
