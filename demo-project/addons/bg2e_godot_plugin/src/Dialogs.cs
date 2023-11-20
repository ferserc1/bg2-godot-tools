
using Godot;

namespace bg2e
{
	public class Dialogs
	{
		public static FileDialog CreateOpenFileDialog(string [] filters, bool multi = false)
		{
			var dialog = new FileDialog();
			foreach (var filter in filters)
			{
				dialog.AddFilter(filter);
			}
			dialog.Access = FileDialog.AccessEnum.Filesystem;
			dialog.FileMode = multi ? FileDialog.FileModeEnum.OpenFiles : FileDialog.FileModeEnum.OpenFiles;
			dialog.MinSize = new Vector2I(700, 550);
			return dialog;
		}

		public static FileDialog CreateSaveFileDialog(string [] filters)
		{
			var dialog = new FileDialog();
			foreach (var filter in filters)
			{
				dialog.AddFilter(filter);
			}
			dialog.Access = FileDialog.AccessEnum.Filesystem;
			dialog.FileMode = FileDialog.FileModeEnum.SaveFile;
			dialog.MinSize = new Vector2I(700, 550);
			return dialog;
		}
	}
}
