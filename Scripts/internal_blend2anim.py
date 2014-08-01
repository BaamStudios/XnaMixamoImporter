import bpy
import sys
 
argv = sys.argv
argv = argv[argv.index("--") + 1:] # get all args after "--"
 
blend_in = argv[0]
fbx_out = argv[1]

bpy.ops.wm.open_mainfile(filepath=blend_in, load_ui=False) 

main_obj = None
for ob in bpy.data.scenes['Scene'].objects:
	if ob.name.endswith('-lib') and ob.parent is not None:
		main_obj = ob.parent

bpy.ops.object.select_all(action='DESELECT')
for ob in bpy.data.scenes['Scene'].objects:
	if ob != main_obj and ob.parent != main_obj:
		ob.select = True
bpy.ops.object.delete()

scene = bpy.data.scenes['Scene']
for ob in scene.objects:
	if ob.type == 'MESH':
		scene.objects.active = ob
		bpy.ops.object.mode_set(mode = 'EDIT')
		
		for mat_slot_id, mat_slot in enumerate(ob.material_slots):
			ob.active_material_index = mat_slot_id
			bpy.ops.object.material_slot_select()			
			bpy.data.screens['UV Editing'].areas[1].spaces[0].image = mat_slot.material.texture_slots[0].texture.image			
			bpy.ops.mesh.select_all(action='DESELECT')
			
		bpy.ops.object.mode_set(mode = 'OBJECT')

bpy.ops.export_scene.fbx(filepath=fbx_out, axis_forward='-Z', axis_up='Y', mesh_smooth_type='OFF', path_mode='RELATIVE', use_rotate_workaround=True, use_anim=True, use_anim_action_all=True, use_anim_optimize=False, use_default_take=False, object_types={'MESH', 'ARMATURE'}, xna_validate=True)
