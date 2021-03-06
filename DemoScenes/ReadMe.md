# Notes
The scenes assume the following LaunchDarkly flags are in place:
* cube-rotation-axis
  * Determines the rotation of the cubes.
  * Should be a JSON object with the following format: {'x': 1.0, 'y': 0.0, 'z': 0.0}
  * A zero vector will result in no rotation.
* background-color
  * Establishes the background color in the scene.
  * Should be a JSON object with the following format: {'r': 1.0, 'g': 0.0, 'b': 0.0}
* show-options-menu
  * Determines if the "options" button in the main menu is visible.
  * Should return a boolean variation.

The following custom user attributes are used in the scenes (using the previously mentioned LD project) as follows:
* level-id
  * A numeric value of 0 or 1 to indicate which scene is being used.
* class-type
  * A string value with the following handled variations
    * Barbarian
    * Monk
    * Wizard
  * This is set using the "class" drop-down menu (top-right).

Each scene has a LdClient prefab in each scene will need to have its Mobile Key and User Key values defined in order to connect to your LaunchDarkly project.

Level changing using the menu button requires that both scenes are added to the build settings. The default behavior will also assume that SampleScene0 and SampleScene1 are given scene ID's 0 and 1 respectively.

The demo scenes assume that there is an input axis named 'Horizontal' and 'Vertical'. The values can be changed in the project's input manager or, if these inputs are present by another name, the EventSystem object in each scene can be updated accordingly.

As mentioned in the root directory's README.txt, the LaunchDarkly Client Unity Plugin assumes a .NET API Compatibility level of 4.x.
