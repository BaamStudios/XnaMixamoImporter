XnaMixamoImporter
==========

Content pipeline tools for importing animated models from mixamo.com to XNA

What you need:
- XNA 4
- Blender 2.65 (not 2.70 or later)
- Autodesk FBX Converter 2013 (or later)
- 7-Zip
- A character on mixamo.com

What you get:
- A binary .fbx file with your character meshes and embedded textures
- Multiple .anim files with one animation each

How to import the character model:

- Export your character with the t-pose animation on mixamo.com as "include skin" and "Collada for blender (.dae zipped)"
- Drag the downloaded .zip file on zip2tex.bat
- You get a new .fbx file with the unanimated character in the same directory as the .zip file
- Add the .fbx file to your xna content project, process with SkinnedModelProcessor from the xna sample
- Load the Model from the .fbx file with ContentManager.Load<Model>(modelPath)

How to import the animations:
- Export your character with the desired animation (only one at a time) on mixamo.com as "include skin" and "Collada for blender (.dae zipped)".
- Drag the downloaded .zip file on zip2anim.bat
- You get a new .anim file in the same directory as the .zip file
- Add the .anim files to your xna game project, activate "copy if newer" in the properties
- Load the SkinningData from the .anim files with BaamStudios.AnimationController.LoadSkinningData(animFile) or use BaamStudios.AnimationController.AnimationController.AddAnimation(name, file)

Known bugs:
- The zip2tex.bat only works with one texture per mesh. Some models (e.g. dude.fbx) use one mesh with multiple textures.

TODO: 
- change xna skinned model example to use a sample mixamo animation
- create proper documentation
- fix zip2tex.bat to work with models that have one mesh with multiple textures
