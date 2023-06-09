using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Mesh generator code from youtube tutorial: created by Brackeys
// Video Link: https://www.youtube.com/watch?v=64NblGkAabk
public class MeshGeneratorLand : MonoBehaviour
{
    Mesh mesh; // Ref to game objects mesh.

    // Arrays for vertices and triangles within grid.
    Vector3[] vertices;
    int[] triangles;

    // Size of the meshes X and Z axis.
    public int xSize = 20;
    public int zSize = 20;

    // Perlin noise multiplier parameters.
    public float perlinNoiseMultiplierX = .2f;
    public float perlinNoiseMultiplierZ = .2f;
    public float perlinNoiseResultMultiplier = 2f;
    // Ref to random green vlaue for texture Mesh.
    public float greenValue;

    // Ref to meshes texture.
    private Texture2D texture;

    private void Start()
    {
        // Assign new mesh and mesh filter.
        mesh = new Mesh(); 
        GetComponent<MeshFilter>().mesh = mesh;
        // Create and assign new texture.
        texture = new Texture2D(xSize, zSize);
        GetComponent<Renderer>().material.mainTexture = texture;

        // Create the meshes shape, and apply the new texture.
        CreateShape();
        TextureMesh();

    }

    private void Update()
    {
        // Apply new shape to scene.
        UpdateMesh();

        Debug.Log("GreenValue is " + greenValue);
        Debug.Log("PerlinNoise Value is " + perlinNoiseResultMultiplier);
    }

    private void CreateShape()
    {
        // Create array with the meshes vertices.
        // Each lenght has 2 verts, however 2 lengths contain 3 verts, so + 1 to x and y values.
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
       
        // Loop over each vertices on the z and x axis and assign them a position within the grid.
        // i = index for each vertices, so we can edit each of them locally within the for loop.
        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                // Add perlin noise to the y value of the vertices, making more natural like mesh terrain.
                // Multiply x and z values and perlin noise result for more exaggereated results.
                float perlinValueY = Mathf.PerlinNoise
                    (x * perlinNoiseMultiplierX, z * perlinNoiseMultiplierZ) * perlinNoiseResultMultiplier;

                // Set the current vertices as a new Vector3 using the current x and z values.
                vertices[i] = new Vector3 (x, perlinValueY, z);

                i++; // Move onto the next vertices, incrementing the current i value by 1.
            }
            
        }
        // Int array for 2 triangles within each quad of the mesh grid.
        // There are 6 points of triangles in 1 quad. 1 quad or 6 triangles * size of grid. 
        triangles = new int[xSize * zSize * 6];

        // Use current vertices and triangle point in the array to offset the next triangles position.
        int currentVertices = 0;
        int currentTriangles = 0;
        
        // Use for loop to add triangles to each vertices point within the grid.
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                // Triangles [0~2] = 1st triangle. Triangles [3~6] = 2nd triangle.
                triangles[currentTriangles + 0] = currentVertices + 0;
                triangles[currentTriangles + 1] = currentVertices + xSize + 1;
                triangles[currentTriangles + 2] = currentVertices + 1;
                triangles[currentTriangles + 3] = currentVertices + 1;
                triangles[currentTriangles + 4] = currentVertices + xSize + 1;
                triangles[currentTriangles + 5] = currentVertices + xSize + 2;

                currentVertices++; // Add current vert to move to next vert point.
                currentTriangles += 6; // When a loop of 6 triangles have been made, add 6 to the current triangles.

                
            }
            currentVertices++; // After for loop on x axis is completed, move to the next vertices.
        }
    }

    private void TextureMesh()
    {
        // Loop through x and z size 
        for (int x = 0; x < xSize; x++)
        {
            for (int z = 0; z < zSize; z++)
            {
                // Use perlin noise to create gradient for materials.
                float gradientValue = Mathf.PerlinNoise(x, z);
                greenValue = Random.Range(0,1.1f);
                // Set the pixel colour using the vlaue from the perlin noise method and random green value.
                texture.SetPixel(x, z, new Color(gradientValue, greenValue, gradientValue));
            }
        }
        texture.Apply(); // Apply new texture.
    }

    private void UpdateMesh()
    {
        
        mesh.Clear(); // Clear any previous mesh data.
        
        // Update vertices and triangles arrays.
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        // Recaluclate normals for meshes lighting.
        mesh.RecalculateNormals();
    }

    public void ReGenerateMesh()
    {
        // Used on red button.
        // Re-gen terrain with a random range for perlin noise result multiplier.
        perlinNoiseResultMultiplier = Random.Range(0, 15);
        CreateShape();
        TextureMesh();
        UpdateMesh();
    }
    

    private void OnDrawGizmos()
    {
        // When  there is no vertices, stop drawing.
        if (vertices == null)
            return;
        // Draw a sphere at each vertices point using the vertices array using Gizmos .
        for (int i = 0; i < vertices.Length; i++)
        {
            float sphereRadius = 0.1f;
            Gizmos.DrawSphere(vertices[i], sphereRadius);
        }
    }


}
