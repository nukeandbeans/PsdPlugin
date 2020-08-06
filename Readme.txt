Photoshop PSD FileType Plugin for Paint.NET

Fork Description
------------
In this fork we removed parts of code related to writing PSD file since we are only interested in reading it.
We also added our code for exporting layers into PNG textures and a few Unity specific functionalities.
System.Drawing.Rectangle is replaced with UnityEngine.Rect and Size is replaced with UnityEngine.Vector2.
In order to create a custom exported, that for example create UI elements you should extend PsdExporter
and implement your logic in overrides to these methods:

```
void OnStart();
void OnEnd(Exception e = null);

void ExportArtLayer(Layer layer);
void ExportTextLayer(Layer layer);
void ExportFolderLayer(Layer layer);
```

Original Description
------------

This plugin allows Paint.NET to load and save Photoshop .PSD files.

Perfect round-trip compatibility is possible for features that are
common to both Paint.NET and Adobe Photoshop.  Other features are
preserved with varying levels of visual fidelity.

For more details, or to report bugs, please refer to the website:
  https://www.psdplugin.com/


Installation
-------------

1. Exit all instances of Paint.NET.
2. Copy Photoshop.dll into the C:\Program Files\Paint.NET\FileTypes
   directory.
3. Restart Paint.NET, which will automatically detect the plugin.

A copy of Paint.NET can be obtained from the official website:
  http://www.getpaint.net/


Licensing
----------

This software is open-source, under the MIT and BSD licenses.  Please see
LICENSE.TXT for complete licensing and attribution information.
