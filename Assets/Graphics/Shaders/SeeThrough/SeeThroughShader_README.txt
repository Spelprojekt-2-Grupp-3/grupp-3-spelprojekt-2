README FOR USING SEETHROUGH SHADER - By Max Larsson

The package should take care of MOST things by itself but there are a couple manual things that need to be done. 

INSTRUCTIONS
1. Create a "SeeThrough" layer. Name is optional, just make it comprehensible
2. Assign the script PlayerSTCircleSync to the player gameobject
3. Assign your material/the example material to the Mat variable on the script. 
4. Assign the main camera to the script. If no camera is attached, the script will try to find "MainCamera". 
5. Set the Mask variable on the script to the layer you created. 
6. Set any object that should be able to be see-through to use the layer you created. 
    Note that these objects can't have another layer as far as I'm aware, if you need to categorize them consider tags. 

The script will draw a ray from the player towards the main camera. Any object intersecting that ray with the appropriate tag will make the object see through.

You can adjust the smoothness and opacity through the materials tab. Size should be controlled by the slider on the GAMEOBJECT HOLDING THE SCRIPT NOT ON THE MATERIAL. 
    This is because the circle activation is done by changing the size of the circle, and since materials are static that change is permanent and global
    So to be able to change the size freely, another variable is needed

For the circle to be perfectly centered on your character, I advice putting it on an object with its origin in the middle of the mesh (This can be an otherwise empy child)

If you're reading this, you're likely part of my group so just ask me if you run into any issues and I'll do my best to look into it! 
    If you have a friend who would like to use the shader, you may share it with them, just be mindful that I can't help too many people if it starts showing problems

KNOWN ISSUES
I would like to add noise to the circle but have so far been unable to get it to render properly. If I can get it to render properly I should be able to animate it by multiplying with the current time
There MAY be issues with non-manifold geometry looking weird. Ask your 3D artist to help patch the model if you're unable. Completely manifold objects shouldn't have any problems rendering