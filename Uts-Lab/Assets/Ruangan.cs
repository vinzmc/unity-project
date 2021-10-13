using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruangan : MonoBehaviour
{
 public float width = 4f;
    public float height = 4f;
    public float thick = 4f;

    public Material cubeMaterial;
    public Texture myTexture;
    
    // Start is called before the first frame update
    void Start()
    {
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[24];
        var uvs = new Vector2[vertices.Length];

        cubeMaterial.mainTexture = myTexture;

        vertices[0] = new Vector3(width,height,thick);
        vertices[1] = new Vector3(width,-height,thick);
        vertices[2] = new Vector3(width,height,-thick);
        vertices[3] = new Vector3(width,-height,-thick);

        vertices[4] = new Vector3(width,height,thick);
        vertices[5] = new Vector3(-width,height,thick);
        vertices[6] = new Vector3(width,height,-thick);
        vertices[7] = new Vector3(-width,height,-thick);

        vertices[8] = new Vector3(width,height,thick);
        vertices[9] = new Vector3(-width,height,thick);
        vertices[10] = new Vector3(width,-height,thick);
        vertices[11] = new Vector3(-width,-height,thick);

        vertices[12] = new Vector3(-width,height,thick);
        vertices[13] = new Vector3(-width,-height,thick);
        vertices[14] = new Vector3(-width,height,-thick);
        vertices[15] = new Vector3(-width,-height,-thick);

        vertices[16] = new Vector3(width,-height,thick);
        vertices[17] = new Vector3(-width,-height,thick);
        vertices[18] = new Vector3(width,-height,-thick);
        vertices[19] = new Vector3(-width,-height,-thick);

        vertices[20] = new Vector3(width,height,-thick);
        vertices[21] = new Vector3(-width,height,-thick);
        vertices[22] = new Vector3(width,-height,-thick);
        vertices[23] = new Vector3(-width,-height,-thick);

        uvs[0] = new Vector2(0.75f, 0.66f);
        uvs[1] = new Vector2(0.75f, 0.34f);
        uvs[2] = new Vector2(1.0f, 0.66f);
        uvs[3] = new Vector2(1.0f, 0.34f);
        uvs[4] = new Vector2(0.75f, 0.66f);
        uvs[5] = new Vector2(0.5f, 0.66f);
        uvs[6] = new Vector2(0.75f, 1.0f);
        uvs[7] = new Vector2(0.5f, 1.0f);
        uvs[8] = new Vector2(0.75f, 0.66f);
        uvs[9] = new Vector2(0.5f, 0.66f);
        uvs[10] = new Vector2(0.75f, 0.34f);
        uvs[11] = new Vector2(0.5f, 0.34f);
        uvs[12] = new Vector2(0.5f, 0.66f);
        uvs[13] = new Vector2(0.5f, 0.34f);
        uvs[14] = new Vector2(0.25f, 0.66f);
        uvs[15] = new Vector2(0.25f, 0.34f);
        uvs[16] = new Vector2(0.75f, 0.34f);
        uvs[17] = new Vector2(0.5f, 0.34f);
        uvs[18] = new Vector2(0.75f, 0.0f);
        uvs[19] = new Vector2(0.5f, 0.0f);
        uvs[20] = new Vector2(0.0f, 0.66f);
        uvs[21] = new Vector2(0.25f, 0.66f);
        uvs[22] = new Vector2(0.0f, 0.34f);
        uvs[23] = new Vector2(0.25f, 0.34f);
        
        mesh.vertices = vertices;

        var colors = new Color32[vertices.Length];
        mesh.colors32 = colors;
        mesh.uv = uvs;

        mesh.triangles = new int[] {
            2, 1, 0,
            3, 1, 2,
            5, 7, 6,
            5, 6, 4,
            11, 9, 8,
            10, 11, 8,
            13, 14, 12,
            13, 15, 14,
            17, 18, 19,
            17, 16, 18,
            23, 20, 21,
            23, 22, 20
        };

    
        mesh.RecalculateNormals();
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshRenderer>().material = cubeMaterial;
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            myTexture.filterMode = SwitchFilterModes();
            Debug.Log("Filter mode: " + myTexture.filterMode);
        }

    }

    FilterMode SwitchFilterModes(){
        switch(myTexture.filterMode){
            case FilterMode.Bilinear:
                myTexture.filterMode = FilterMode.Point;
                break;
            case FilterMode.Point:
                myTexture.filterMode = FilterMode.Trilinear;
                break;
            case FilterMode.Trilinear:
                myTexture.filterMode = FilterMode.Bilinear;
                break;
        }

        return myTexture.filterMode;
    }

}
