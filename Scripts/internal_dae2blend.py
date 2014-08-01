import Blender
import sys
import blender_collada as bc
 
argv = sys.argv
argv = argv[argv.index("--") + 1:] # get all args after "--"
 
dae_in = argv[0]
blend_out = argv[1]

bc.load(dae_in, 30, 1.0, 0)

Blender.Save(blend_out)

Blender.Quit()