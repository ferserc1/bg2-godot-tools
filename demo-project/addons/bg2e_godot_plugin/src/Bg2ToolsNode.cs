using bg2e;
using Godot;

[Tool]
public partial class Bg2ToolsNode : Node3D {

    protected EditorPlugin _EditorPlugin = null;
    protected FileDialog ImportBg2FileDialog = null;

    protected EditorPlugin _GetEditorPlugin() {
        if (_EditorPlugin == null) {
            _EditorPlugin = new EditorPlugin();
        }
        return _EditorPlugin;
    }

    Node3D _AddParentNode = null;
    Node _AddOwnerNode = null;

    public void ImportBg2Model(Node3D parent = null, Node owner = null)
    {
        var rootNode = parent;
        if (rootNode == null && Engine.IsEditorHint()) {
            rootNode = GetSelectedNode();
        }
        else if (rootNode == null) {
            GD.PrintErr("Error: No root node specified");
            return;
        }

        _AddParentNode = rootNode;
        _AddOwnerNode = owner;
        if (rootNode is Node3D) {
            if (ImportBg2FileDialog == null) {
                ImportBg2FileDialog = Dialogs.CreateOpenFileDialog(new string[]{"*.bg2","*.vwglb"});
                this.AddChild(ImportBg2FileDialog);
                ImportBg2FileDialog.FilesSelected += _ImportFiles;
            }
            ImportBg2FileDialog.Show();
        }
        else {
            GD.PrintErr("Error: The current scene is not a Node3D scene.");
        }
    }

    public Node3D GetSelectedNode()
    {
        var eds = _GetEditorPlugin().GetEditorInterface().GetSelection();
        var sel = eds.GetSelectedNodes();
        if (sel.Count > 0 && sel[0] is Node3D) {
            return sel[0] as Node3D;
        }
        return null;
    }

    public string GetNodeNameFromPath(string path)
	{
		var splitString = path.Split("/");
		if (splitString.Length < 2) {
			splitString = path.Split("\\");
		}
		if (splitString.Length > 0) {
			return splitString[splitString.Length - 1];
		}
		return path;
	}

    protected void _ImportFiles(string [] paths)
    {
        foreach (var path in paths) {
            var scene = _AddOwnerNode;
            var rootNode = _AddParentNode;
            if (scene != null && rootNode != null) {
                var newNode = new Bg2Mesh();
                rootNode.AddChild(newNode);
                newNode.Owner = scene;
                
                newNode.LoadMeshFromFile(path);
                newNode.Name = GetNodeNameFromPath(path);
                rootNode.Position = new Vector3(0.0f, 0.0f, 0.0f);
                
            }
        }
    }
}
