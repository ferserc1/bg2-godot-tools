using bg2io;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

[Tool]
public partial class Bg2Mesh : Node3D
{
    // Important: call this function AFTER the node has been added to the scene
    public void LoadMeshFromFile(string path)
    {
        try {
            var reader = new Bg2FileReader();
            GD.Print("Loading bg2 file: ", path);
            var bg2File = reader.Open(path);
            LoadMesh(bg2File);
        }
        catch (Exception err) {
            GD.PrintErr(err.Message);
        }
    }

    protected Vector3[] _ToVector3Array(float[] data)
    {
        var result = new List<Vector3>();

        for (int i = 0; i < data.Length; i+=3) {
            result.Add(new Vector3(data[i], data[i + 1], data[i + 2]));
        }

        return result.ToArray();
    }

    protected Vector2[] _ToVector2Array(float[] data)
    {
        var result = new List<Vector2>();

        for (int i = 0; i < data.Length; i+=2) {
            result.Add(new Vector2(data[i], data[i + 1]));
        }

        return result.ToArray();
    }

    // Important: call this function AFTER the node has been added to the scene
    public void LoadMesh(Bg2File bg2File)
    {
        var scene = GetTree().EditedSceneRoot;
        GD.Print("Loading bg2 model version: ", bg2File.GetVersion());
        foreach (var plist in bg2File.polyLists) {
            var surfaceArray = new Godot.Collections.Array();
            surfaceArray.Resize((int)ArrayMesh.ArrayType.Max);
            surfaceArray[(int)Mesh.ArrayType.Vertex] = _ToVector3Array(plist.vertex);
            surfaceArray[(int)Mesh.ArrayType.Normal] = _ToVector3Array(plist.normal);
            surfaceArray[(int)Mesh.ArrayType.TexUV] = _ToVector2Array(plist.t0Coord);
            if (plist.t1Coord != null) {
                surfaceArray[(int)Mesh.ArrayType.TexUV2] = _ToVector2Array(plist.t1Coord);
            }

            // Reverse triangles because the bg2 model front face is ccw
            var index = new List<int>();
            for (int i = 0; i < plist.index.Length; i += 3) {
                index.Add(plist.index[i + 2]);
                index.Add(plist.index[i + 1]);
                index.Add(plist.index[i]);
            }
            surfaceArray[(int)Mesh.ArrayType.Index] = index.ToArray();

            var arrayMesh = new ArrayMesh();
            arrayMesh.AddSurfaceFromArrays(Mesh.PrimitiveType.Triangles, surfaceArray);

            var polyList = new Bg2PolyList();
            AddChild(polyList);
            polyList.Name = plist.name;
            polyList.Owner = scene;
            polyList.Mesh = arrayMesh;
        }
    }

    public void ExportMesh(String path) {
        var children = GetChildren();
        GD.Print("File selected: ", path);
        foreach (var child in children) {
            GD.Print("Children");
            if (child is Bg2PolyList) {
                var polyList = child as Bg2PolyList;
                GD.Print("Exporting ", polyList.Name);
                var meshTool = new MeshDataTool();
                var mesh = new ArrayMesh();
                mesh.AddSurfaceFromArrays(Mesh.PrimitiveType.Triangles, polyList.Mesh.SurfaceGetArrays(0));
                meshTool.CreateFromSurface(mesh, 0);
                
                for (var i = 0; i < meshTool.GetVertexCount(); i++) {
                    var vertex = meshTool.GetVertex(i);
                    var normal = meshTool.GetVertexNormal(i);
                    var texCoord0 = meshTool.GetVertexUV(i);
                    var texCoord1 = meshTool.GetVertexUV2(i);
                    GD.Print("Vertex ", i, ": ", vertex, ", Normal:", normal, ", UV0: ", texCoord0, ", UV1", texCoord1);
                }

            }
        }
    }
}