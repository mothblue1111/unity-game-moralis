Hi, thanks for downloading this Unity Asset.

UI Fade Script : Created by ScoredOne.

UI Fade Script is a script I made to allow simple fading for objects when operating UI elements.
One press of a button and the script will fade out the object without disabling it.

Scripts:
= UIFadeBase
= UIFadeScript
= UIFadeGetAllForFader
= UIFadeGraphic
= UIFadeGroupFader
= UIFadeSpriteRenderer

How to:
To use the script, add the script UIFadeGraphic to a GameObject that has a Component that inherits from Graphic (Image, Text ext.)
Attach the script to a button and use one of the functions ToggleFade(), FadeIn() or FadeOut().
UIFadeSpriteRenderer works exactly the same as UIFadeGraphic but only applies to SpriteRenderer Components.

You have 3 options to change.
The fade speed, the OnStart fade option (To fade in, to fade out or no action) and the events that Invoke once the fade is complete.

UIFadeGroupFader is a script made to store any number of fade scripts (UIFadeBase class) in the scene and run them at the same time.
Just like UIFadeGraphic, the script can be attached to buttons and used to perform the same actions on all fade scripts.

To quickly fill UIFadeGroupFader, UIFadeGetAllForFader is provided. This script will get all the fade scripts (UIFadeBase class) that 
are children underneath the current gameobject, simply add the script to the gameobject and press the button labeled "Get Children".

If you wish to interact with every fade script in the scene, then there is a solution for this.
All UIFadeBase classes register to the static UIFadeScript. From this class you can call the functions FadeAllIn() and FadeAllOut()
to make every script in the scene to perform this action, taking into account their own state.