XnaMixamoImporter
==========

Content pipeline tools for importing animated models from [mixamo.com](https://www.mixamo.com) to XNA

##Motivation
- Fbx files from [mixamo.com](https://www.mixamo.com) are using the fbx 2013 format which is not supported by xna.
- The [official way](https://www.mixamo.com/faq/) to convert from fbx 2013 to fbx 2011 (using the Autodesk FBX Converter) produces an animation where the bones are awfully distorted when using the [skinned model sample](http://xbox.create.msdn.com/en-US/education/catalog/sample/skinned_model) (e.g. long and bent legs or worse).
- Another approach [using blender](http://community.mixamo.com/mixamo/topics/distorted_injured_walk_model_in_xna_4_0) works great with animations, but the textures will be unassigned unless you reassign them by hand. However, this is the approach that this project is based on.
- [mixamo.com](https://www.mixamo.com) does not create a model file with multiple animations, so you would have to load the full model once for each animation, which is very inefficient.

##What you need
- [XNA 4](http://www.microsoft.com/en-us/download/details.aspx?id=23714)
- [Blender 2.65](http://download.blender.org/release/Blender2.65/) (not later versions!)
- [Autodesk FBX Converter 2013](http://usa.autodesk.com/adsk/servlet/pc/item?siteID=123112&id=10775920) (or maybe later versions)
- [7-Zip](http://www.7-zip.org/)
- A character on [mixamo.com](https://www.mixamo.com)

##What you get
- A binary .fbx file with your character model in t-pose and embedded textures
- Multiple .anim files with one animation each

##How to import the character model
- Export your character with the t-pose animation on [mixamo.com](https://www.mixamo.com) as "include skin" and "Collada for Blender 2.49 (.dae zipped)"
- Drag the downloaded .zip file on [zip2fbx.bat](Scripts/zip2fbx.bat)
- You get a new .fbx file with the unanimated character in the same directory as the .zip file
- Add the .fbx file to your xna content project, process with [SkinnedModelProcessor](SkinningSample/SkinnedModelPipeline/SkinnedModelProcessor.cs)
- Load the Model from the .fbx file with ContentManager.Load&lt;Model&gt;(modelPath)

##How to import the animations
- Export your character with the desired animation (only one at a time) on [mixamo.com](https://www.mixamo.com) as "include skin" and "Collada for Blender 2.49 (.dae zipped)".
- Drag the downloaded .zip file on [zip2anim.bat](Scripts/zip2anim.bat)
- You get a new .anim file in the same directory as the .zip file
- Add the .anim files to your xna game project, activate "copy if newer" in the properties
- Load the [SkinningData](SkinningSample/SkinnedModel/SkinningData.cs) from the .anim files with [BaamStudios.Animation](BaamStudios.AnimationController/Animation.cs).LoadSkinningData(animFile) or use [BaamStudios.AnimationController.AnimationController](BaamStudios.AnimationController/AnimationController.cs).AddAnimation(name, file)

##Notes
- This project includes the [skinned model sample](http://xbox.create.msdn.com/en-US/education/catalog/sample/skinned_model). Some minor changes have been made, which are enclosed in C#-regions containing the word "BaamStudio".
- The .anim files are just binary serialized [SkinningData](SkinningSample/SkinnedModel/SkinningData.cs). You could use other formats (e.g. json) if you like. There is some commented code prepared in [Animation](BaamStudios.AnimationController/Animation.cs) and [AnimationExtractorModelProcessor](BaamStudios.AnimationExtractorPipeline/AnimationExtractorModelProcessor.cs) to get you started on json format using [ServiceStack.Text](https://servicestack.net/text).
- This project was built around a character made with [Fuse](https://www.mixamo.com/fuse). Other character generators may not be supported yet. Please contact us if you would like to contribute your changes to support other model sources.
- The .anim files will probably only work with the character that was used to export them from [mixamo.com](https://www.mixamo.com) because other characters ususally have different t-pose bone positions. However, this has not been confirmed yet and there is a chance that different characters may work with the same .anim files if the skeletons contain the same bones and the two characters have similar proportions.
