#if TOOLS
using Godot;
using System;
using bg2e;

[Tool]
public partial class Bg2ePlugin : EditorPlugin
{
	protected FileDialog ImportBg2FileDialog = null;

	protected Bg2ToolsNode Tools = null;
	
	public override void _EnterTree()
	{
		AddToolMenuItem("Import .bg2 Models", Callable.From(this.ImportBg2Model));

		var script = GD.Load<Script>("res://addons/bg2e_godot_plugin/src/Bg2ToolsNode.cs");
		var icon = GD.Load<Texture2D>("res://addons/bg2e_godot_plugin/icons/tools.png");
		AddCustomType("Bg2ToolsNode", "Node3D", script, icon);
	}

	public override void _ExitTree()
	{
		// Clean-up of the plugin goes here.
	}
	
	

	public void ImportBg2Model()
	{
		if (Tools == null) {
			var root = GetTree().Root;
			Tools = new Bg2ToolsNode();
			root.AddChild(Tools);
		}
		Tools.ImportBg2Model(Tools.GetSelectedNode(), GetTree().EditedSceneRoot);
	}
	
}
#endif
