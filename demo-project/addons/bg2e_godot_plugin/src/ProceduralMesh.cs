using bg2io;
using Godot;
using System;
using System.Collections.Generic;

[Tool]
public partial class ProceduralMesh : MeshInstance3D
{
    [Export] public float Width = 1.0f;

    [Export] public float Height { get; set; } = 1.0f;

    private float w = 0.0f;
    private float h = 0.0f;

    public bool IsUpdated()
    {
        return w == Width && h == Height;
    }

    public void UpdateMesh()
    {
        w = Width;
        h = Height;

        var w2 = w / 2.0f;
        var h2 = h / 2.0f;

        var vert = new List<Vector3>();
        vert.Add(new Vector3(-w2,-h2, 0));
        vert.Add(new Vector3(-w2, h2, 0));
        vert.Add(new Vector3( w2, h2, 0));
        
        var norm = new List<Vector3>();
        norm.Add(new Vector3(0.0f, 0.0f, 1.0f));
        norm.Add(new Vector3(0.0f, 0.0f, 1.0f));
        norm.Add(new Vector3(0.0f, 0.0f, 1.0f));
        
        var uv = new List<Vector2>();
        uv.Add(new Vector2(0.0f, 0.0f));
        uv.Add(new Vector2(0.0f, 1.0f));
        uv.Add(new Vector2(1.0f, 1.0f));
        
        var indices = new List<int>();
        indices.Add(0);
        indices.Add(1);
        indices.Add(2);

        var surfaceArray = new Godot.Collections.Array();
        surfaceArray.Resize((int)ArrayMesh.ArrayType.Max);
        surfaceArray[(int)Mesh.ArrayType.Vertex] = vert.ToArray();
        surfaceArray[(int)Mesh.ArrayType.Normal] = norm.ToArray();
        surfaceArray[(int)Mesh.ArrayType.TexUV] = uv.ToArray();
        surfaceArray[(int)Mesh.ArrayType.Index] = indices.ToArray();

        var arrayMesh = new ArrayMesh();
        arrayMesh.AddSurfaceFromArrays(Mesh.PrimitiveType.Triangles, surfaceArray);

        vert = new List<Vector3>();
        vert.Add(new Vector3( w2, h2, 0));
        vert.Add(new Vector3( w2,-h2, 0));
        vert.Add(new Vector3(-w2,-h2, 0));
        
        norm = new List<Vector3>();
        norm.Add(new Vector3(0.0f, 0.0f, 1.0f));
        norm.Add(new Vector3(0.0f, 0.0f, 1.0f));
        norm.Add(new Vector3(0.0f, 0.0f, 1.0f));
        
        uv = new List<Vector2>();
        uv.Add(new Vector2(1.0f, 1.0f));
        uv.Add(new Vector2(1.0f, 0.0f));
        uv.Add(new Vector2(0.0f, 0.0f));

        indices = new List<int>();
        indices.Add(0);
        indices.Add(1);
        indices.Add(2);

        surfaceArray = new Godot.Collections.Array();
        surfaceArray.Resize((int)ArrayMesh.ArrayType.Max);
        surfaceArray[(int)Mesh.ArrayType.Vertex] = vert.ToArray();
        surfaceArray[(int)Mesh.ArrayType.Normal] = norm.ToArray();
        surfaceArray[(int)Mesh.ArrayType.TexUV] = uv.ToArray();
        surfaceArray[(int)Mesh.ArrayType.Index] = indices.ToArray();

        arrayMesh.AddSurfaceFromArrays(Mesh.PrimitiveType.Triangles, surfaceArray);

        Mesh = arrayMesh;
    }

    public override void _Process(double delta)
    {
        if (!IsUpdated()) {
            UpdateMesh();
        }
    }
}
