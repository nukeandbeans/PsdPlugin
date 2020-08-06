using System;
using System.IO;
using PhotoshopFile;
using UnityEditor;
using UnityEngine;

public class PsdTextureExporter : PsdExporter
{
	public static bool ExportWithoutSubfolders;

	public readonly string RootFolder;
	private bool skipGeneration;

	public PsdTextureExporter(string asset) : base(asset)
	{
		RootFolder = Path.Combine("Assets/Resources/UI", Psd.Name);
	}

	public PsdTextureExporter(PsdFile psd) : base(psd)
	{
		RootFolder = Path.Combine("Assets/Resources/UI", Psd.Name);
	}

	protected override void OnStart()
	{
		skipGeneration = Directory.Exists(RootFolder);
		if (skipGeneration) return;
		Directory.CreateDirectory(RootFolder);
		AssetDatabase.StartAssetEditing();
	}

	protected override void ExportArtLayer(Layer layer)
	{
		if (skipGeneration) return;
		var path = GetTexturePath(layer);

		// decode the layer into a texture
		var texture = ImageDecoder.DecodeImage(layer);
		File.WriteAllBytes(path, texture.EncodeToPNG());
		AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceSynchronousImport);
	}

	protected override void ExportFolderLayer(Layer layer)
	{
		// don't create folder if exporting without subfolders
		if (skipGeneration || ExportWithoutSubfolders) return;
		var path = Path.Combine(RootFolder, GetPath(layer));
		path = Path.Combine(path, TrimAllSpaces(layer.Name));
		Directory.CreateDirectory(path);
	}

	protected override void ExportTextLayer(Layer layer) { }

	protected override void OnEnd(Exception e = null)
	{
		if (skipGeneration) return;
		AssetDatabase.StopAssetEditing();
	}

	public string GetTexturePath(Layer layer)
	{
		// if exporting without subfolders then it's just root folder
		var path = ExportWithoutSubfolders ? RootFolder : Path.Combine(RootFolder, GetPath(layer));
		return Path.Combine(path, TrimAllSpaces(layer.Name) + ".png");
	}
}