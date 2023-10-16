# Business Grade Graphic Engine extensions for Godot Engine

The bg2 engine graphics engine is designed to create lightweight 3D web applications, with a specific native file format to extend the information associated with the 3D models. The intention of this is to generate a file format that can be used for enterprise applications, with portable 3D models.

bg2 engine only intends to facilitate this integration between 3D information and application data for general use. For this reason, it only implements the minimum necessary part to be able to create web applications that use these files in an agile way. But there are many things that bg2 engine does not do: mobile applications, extended reality, video games, etc.

Godot Engine, due to its power and Open Source license (identical to bg2 engine), is the perfect complement to supply all the areas that bg2 engine does not cover.
This repository contains bg2 engine file integration plugins with Godot for:

- Loading 3D models from bg2 files
- Saving nodes from godot to bg2 format
- 3D models editing tools for bg2 engine

This project is also intended to replace the bg2 Composer editor.

## Requirements

Pou can use this plugin with Godot 4.1 .NET, as the code is developed in C#. However, you can use all the new nodes and tools of the plugin with GDScript.

## How to use

In the `demo-project` example you have a Godot Engine project with the plugin add-on. To use the plugin in your project, copy the `addons/bg2e_godot_plugin` folder to the `addons` folder in your project. If your project is not C#, you may have to create the solution manually via the menu `Project` > `Tools` > `C#` > `Create C# Solution`.


## Debug

The recomendation is to use Visual Studio Code as text editor and IDE. You need to install the extension C# Dev Kit from Microsoft. Optionally, you can install the extension godot-tools to debug GDScript in Visual Studio Code. You also need to configure the external editor path in Godot Engine Editor Settings (Text Editor > External).

To launch Godot from VS Code debuger, you need to set up the GODOT4 environment variable, pointing to the Godot 4 executable (in macOS, the executable is the file `Godot.app/Content/MacOS/Godot`. Do not set the application package).

Now, you can set break points in the code an debug the source code with the Visual Studio Code debugger. Visual Studio Code will launch Godot in game mode when you start debugging. Note that there is no way to debug editor plugins. To do it, the easiest way is to include the code you want to debug using nodes in a game, and then use that nodes to create the editor plugins.

