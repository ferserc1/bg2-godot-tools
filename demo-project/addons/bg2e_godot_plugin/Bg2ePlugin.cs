#if TOOLS
using Godot;
using System;
using bg2e;

[Tool]
public partial class Bg2ePlugin : EditorPlugin
{
	protected FileDialog ImportBg2FileDialog = null;
	
	public override void _EnterTree()
	{
		AddToolMenuItem("Import .bg2 Models", Callable.From(this.ImportBg2Model));
	}

	public override void _ExitTree()
	{
		// Clean-up of the plugin goes here.
	}
	
	public void ImportBg2Model()
	{
		var rootNode = _GetSelectedNode();
		if (rootNode is Node3D) {
			if (ImportBg2FileDialog == null) {
				ImportBg2FileDialog = Dialogs.CreateOpenFileDialog(new string[]{"*.bg2", "*.vwglb"});
				this.AddChild(ImportBg2FileDialog);
				ImportBg2FileDialog.FilesSelected += _ImportFiles;
			}
			ImportBg2FileDialog.Show();
		}
		else {
			GD.PrintErr("Error: The current scene is not a Node3D scene.");
		}
		
	}
	
	protected Node3D _GetSelectedNode()
	{
		var eds = GetEditorInterface().GetSelection();
		var sel = eds.GetSelectedNodes();
		if (sel.Count > 0 && sel[0] is Node3D) {
			return sel[0] as Node3D;
		}
		else {
			return null;
		}
	}
	
	protected string _GetNodeNameFromPath(string path)
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
	
	protected void _ImportFiles(string [] paths) {
		foreach (var path in paths) {
			var scene = GetTree().EditedSceneRoot;
			var rootNode = _GetSelectedNode();
			if (scene != null && rootNode != null) {
				var newNode = new Node3D();
				newNode.Name = _GetNodeNameFromPath(path);
				newNode.Position = new Vector3(0.0f, 0.0f, 0.0f);
				rootNode.AddChild(newNode);
				newNode.Owner = scene;
			}
		}
	}
}
#endif
