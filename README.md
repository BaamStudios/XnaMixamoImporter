XnaMixamoImporter
==========

Content pipeline tools for importing animated models from mixamo.com to XNA

##Motivation:
- Fbx files from mixamo.com are using the fbx 2013 format which is not supported by xna.
- The [official way](https://www.mixamo.com/faq/) to convert from fbx 2013 to fbx 2011 using the Autodesk FBX Converter produces an animation where the bones are awfully distorted when using the [skinned model sample](http://xbox.create.msdn.com/en-US/education/catalog/sample/skinned_model) (e.g. long and bended legs or worse).
- Another approach [using blender](http://community.mixamo.com/mixamo/topics/distorted_injured_walk_model_in_xna_4_0) works great with animations, but the textures will be unassigned unless you reassign them by hand. However, this is the approach that this project is based on.
- mixamo.com does not create a model file with multiple animations, so you would have to load the full model once for each animation, which is very inefficient.

##What you need:
- XNA 4
- Blender 2.65 (not later versions!)
- Autodesk FBX Converter 2013
- 7-Zip
- A character on mixamo.com

##What you get:
- A binary .fbx file with your character model in t-pose and embedded textures
- Multiple .anim files with one animation each

##How to import the character model:

- Export your character with the t-pose animation on mixamo.com as "include skin" and "Collada for blender (.dae zipped)"
- Drag the downloaded .zip file on [zip2fbx.bat](Scripts/zip2fbx.bat)
- You get a new .fbx file with the unanimated character in the same directory as the .zip file
- Add the .fbx file to your xna content project, process with [SkinnedModelProcessor](SkinningSample/SkinnedModelPipeline/SkinnedModelProcessor.cs)
- Load the Model from the .fbx file with ContentManager.Load<Model>(modelPath)

##How to import the animations:
- Export your character with the desired animation (only one at a time) on mixamo.com as "include skin" and "Collada for blender (.dae zipped)".
- Drag the downloaded .zip file on [zip2anim.bat](Scripts/zip2anim.bat)
- You get a new .anim file in the same directory as the .zip file
- Add the .anim files to your xna game project, activate "copy if newer" in the properties
- Load the [SkinningData](SkinningSample/SkinnedModel/SkinningData.cs) from the .anim files with [BaamStudios.Animation](BaamStudios.AnimationController/Animation.cs).LoadSkinningData(animFile) or use [BaamStudios.AnimationController.AnimationController](BaamStudios.AnimationController/AnimationController.cs).AddAnimation(name, file)

##Known bugs:
- The [zip2fbx.bat](Scripts/zip2fbx.bat) only works with one texture per mesh. Some models (e.g. [dude.fbx](SkinningSample/SkinningSample/Content/dude.fbx)) use one mesh with multiple textures.

##Notes:
- This project includes the [skinned model sample](http://xbox.create.msdn.com/en-US/education/catalog/sample/skinned_model). Some minor changes have been made, which are enclosed in C#-regions containing the word "BaamStudio".

##TODO: 
- change xna skinned model example to use a sample mixamo animation
- fix [zip2fbx.bat](Scripts/zip2fbx.bat) to work with models that have one mesh with multiple textures
