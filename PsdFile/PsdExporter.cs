using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using PhotoshopFile;
using UnityEngine;

public abstract class PsdExporter
{
	public readonly PsdFile Psd;

	public PsdExporter(string asset)
	{
		Psd = new PsdFile(asset, new LoadContext());
	}

	public PsdExporter(PsdFile psd)
	{
		Psd = psd;
	}

	public void Export()
	{
		OnStart();
		try
		{
			ExportTree(Psd.LayerTree);
			OnEnd();
		}
		catch (Exception e)
		{
			Debug.LogException(e);
			OnEnd(e);
		}
	}

	protected virtual void OnStart() { }

	protected virtual void OnEnd(Exception e = null) { }

	private void ExportTree(List<Layer> layerTree)
	{
		for (int i = layerTree.Count - 1; i >= 0; i--)
		{
			ExportLayer(layerTree[i]);
		}
	}

	private void ExportLayer(Layer layer)
	{
		if (!layer.Visible)
		{
			return;
		}

		if (layer.IsFolder)
		{
			ExportFolderLayer(layer);
			ExportTree(layer.Children);
		}
		else if (layer.IsTextLayer)
		{
			ExportTextLayer(layer);
		}
		else
		{
			ExportArtLayer(layer);
		}
	}

	protected abstract void ExportArtLayer(Layer layer);

	protected abstract void ExportTextLayer(Layer layer);

	protected abstract void ExportFolderLayer(Layer layer);

	protected static string TrimAllSpaces(string value)
	{
		return Regex.Replace(value, @"\s+", string.Empty);
	}

	public static string GetPath(Layer layer)
	{
		string path = "";
		while (layer.Parent != null)
		{
			path = Path.Combine(TrimAllSpaces(layer.Parent.Name), path);
			layer = layer.Parent;
		}

		return path;
	}
}