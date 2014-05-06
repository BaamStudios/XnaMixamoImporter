import bpy
import sys
 
argv = sys.argv
argv = argv[argv.index("--") + 1:] # get all args after "--"
 
dae_in = argv[0]
fbx_out = argv[1]

bpy.ops.wm.collada_import(filepath=dae_in) 
bpy.ops.export_scene.fbx(filepath=fbx_out, object_types={'MESH', 'ARMATURE'}, mesh_smooth_type='OFF', use_anim=True, use_anim_action_all=True, use_default_take=False, use_anim_optimize=False, path_mode='STRIP', use_rotate_workaround=True, xna_validate=True)