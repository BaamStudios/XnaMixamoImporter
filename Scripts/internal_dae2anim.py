import bpy
import sys
 
argv = sys.argv
argv = argv[argv.index("--") + 1:] # get all args after "--"
 
dae_in = argv[0]
fbx_out = argv[1]

bpy.ops.wm.collada_import(filepath=dae_in) 

min_time = 9999
max_time = -9999

for armature in bpy.context.scene.objects:
	if armature.type == 'ARMATURE':
		for fcurve in armature.animation_data.action.fcurves:
			for keyframe in fcurve.keyframe_points:
				cur_time = keyframe.co.x
				min_time = min(min_time, cur_time)
				max_time = max(max_time, cur_time)

for armature in bpy.context.scene.objects:
	if armature.type == 'ARMATURE':
		for fcurve in armature.animation_data.action.fcurves:
			for keyframe in list(reversed(fcurve.keyframe_points)):
				keyframe.co.x = round(keyframe.co.x * 30.0/24.0 - min_time)

bpy.ops.export_scene.fbx(filepath=fbx_out, object_types={'MESH', 'ARMATURE'}, mesh_smooth_type='OFF', use_anim=True, use_anim_action_all=True, use_default_take=False, use_anim_optimize=False, path_mode='STRIP', use_rotate_workaround=True, xna_validate=True)