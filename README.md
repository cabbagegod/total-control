# total-control
A Risk style RTS game.

#### Controls:
* Middle Mouse Button - Drag Camera
* Scroll Wheel - Zoom Camera

Click on a country to begin. If you want to create more countries, simply duplicate the Country gameobject and it *should* work fine. You might also have to regenerate the pathfinding. Countries should also work with any shape geometry.

### Here's a few things that *should* be different but I didn't have time for.

- "BuildingDatabase" should use addressable references instead of directly referencing objects, this decreases memory usage for dynamic assets that aren't currently being used
- UI should be controlled by a central "UIManager", this is just overall a way cleaner solution and much easier to manage.
- UI event listeners should be set through code, not through the Unity inspector. This makes code more readable and prevents issues from occuring if the UI element's references somehow get reset in the inspector.
- The Jet should use an angle based system for targeting. The current system isn't very accurate, however, I did not think to use angles until I had already sunk hours into this system so I did not have the time to fix it.
