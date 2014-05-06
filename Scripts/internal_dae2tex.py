import bpy
import sys
 
argv = sys.argv
argv = argv[argv.index("--") + 1:] # get all args after "--"
 
dae_in = argv[0]
fbx_out = argv[1]

bpy.ops.wm.collada_import(filepath=dae_in) 

scene = bpy.data.scenes['Scene']
for ob in scene.objects:
	if ob.type == 'MESH':
		scene.objects.active = ob
		bpy.ops.object.mode_set(mode = 'EDIT')
		bpy.ops.mesh.select_all()
		bpy.data.screens['UV Editing'].areas[1].spaces[0].image = ob.material_slots[0].material.texture_slots[0].texture.image
		bpy.ops.object.mode_set(mode = 'OBJECT')

bpy.ops.export_scene.fbx(filepath=fbx_out, axis_forward='-Z', axis_up='Y', mesh_smooth_type='OFF', use_rotate_workaround=True)
#, xna_validate=True