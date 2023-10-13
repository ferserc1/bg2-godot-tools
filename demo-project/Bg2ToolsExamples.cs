using Godot;
using System;

public partial class Bg2ToolsExamples : Bg2ToolsNode
{
    protected void _OnImportFileButtonPressed()
    {
        var owner = GetTree().Root;
        var test = GetNode("/root/Main/Test");
        var parent = test as Node3D;
        ImportBg2Model(parent, owner);
    }
}
