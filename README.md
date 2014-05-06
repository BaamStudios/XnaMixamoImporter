mixamo2xna
==========

Content pipeline tools for importing animated models from mixamo.com to XNA

What you need:
- XNA 4
- Blender 2.65 (not 2.70 or later)
- Autodesk FBX Converter 2013 (or later)
- 7-Zip

How it works:
- Export your character with the t-pose animation on mixamo.com as "include skin" and ".dae for blender".
- Run zip2tex.bat with the downloaded zip as parameter.
- You should get a .fbx file with the same name as the zip file.
- Export your character with the desired animation (only one at a time) on mixamo.com as "include skin" and ".dae for blender".
- Run zip2anim.bat with the downloaded zip as parameter.
- You should get a .anim file with the same name as the zip file.
- Add the fbx file to the xna content project, process with SkinnedModelProcessor from the xna sample.
- Add the anim files to the xna game project, activate "copy if newer" in the properties.
- Load the model ingame with ContentManager.Load<Model>(path);
- Load the anim files with new BaamStudios.MixamoAnimation(path);
- Use BaamStudios.MixamoAnimation.SkinningData to draw the model.

TODO: 
- prepare and upload files
- create proper documentation
