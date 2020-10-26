using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{

    public float fov = 360f;
    public int numAristas = 360;
    public float anguloInicial = 0;
    public float distanciaVision = 8f;
    public LayerMask layerMask;
    
    private Mesh mesh;
    private Vector3 origen;

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        origen = Vector3.zero;
    }

    private void LateUpdate()
    {
        UpdateMesh();
    }

    private void GenerarMesh() {

        Vector3[] vertices;
        int[] triangles;

        vertices = new Vector3[]
        {
            new Vector3 (0,0,0),
            new Vector3 (1,0,0),
            new Vector3 (0,-1,0)
        };

        triangles = new int[] {
            0, 1, 2
        };

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        
    }

    private void UpdateMesh()
    {
        float anguloActual = anguloInicial;
        float incrementoAngulo = fov / numAristas;

        Vector3[] vertices = new Vector3[numAristas + 1];
        int[] triangulos = new int[numAristas * 3];

        vertices[0] = origen;

        int indiceVertices = 1;
        int indiceTriangulos = 0;

        for (int i = 0; i < numAristas; i++)
        {
            Vector3 verticeActual;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origen, GetVectorFromAngle(anguloActual), distanciaVision, layerMask);

            if (raycastHit2D.collider == null)
            {
                // No hit
                verticeActual = origen + GetVectorFromAngle(anguloActual) * distanciaVision;
            }
            else
            {
                // Hit object
                verticeActual = raycastHit2D.point;
            }

            vertices[indiceVertices] = verticeActual;

            if (i > 0)
            {
                triangulos[indiceTriangulos + 0] = 0;
                triangulos[indiceTriangulos + 1] = indiceVertices - 1;
                triangulos[indiceTriangulos + 2] = indiceVertices;

                indiceTriangulos += 3;
            }

            indiceVertices++;
            anguloActual -= incrementoAngulo;
        }

        // Formamos el último triángulo
        triangulos[indiceTriangulos + 0] = 0;
        triangulos[indiceTriangulos + 1] = indiceVertices - 1;
        triangulos[indiceTriangulos + 2] = 1;


        mesh.vertices = vertices;
        mesh.triangles = triangulos;
    }

    Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    public void SetOrigin(Vector3 newOrigin) {
        origen = newOrigin;
    }
}
