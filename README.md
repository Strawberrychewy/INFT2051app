# INFT2051app

Current Functionality:
1. Background Step Counter (App must still be running and not completely closed to count shakes)
2. FoodShop functional
3. FoodShop now shows how much currency the player has (default: 100)
3. Options popup works but does not affect anything yet
   - Mute button and slider both linked for a truly immersive experience when the user decides to play with the volume
   - Restart/Credits/ChangeName buttons are also in this popup but are now functional yet (Credits and Restart also have additional popups)
4. Feeding with Food bought from the store now fully functional for both devices with and without fingerprint sensors
   - An onscreen button will show up, prompting the user to smash that like button
5. Pet moves (Horizontally) to the point touched on the screen (excluding button regions), just a gimmick and does not really do anything    meaningful
   - When the player has not touched the touch panel for [5 seconds], the pet now moves on its own in its idle state, 
     it can [Jump low, Jump high, Jump high and do a flip, bounce and walk as if the player has touched the panel]

TODO:
1. Implement fingerprint novelty system [DONE]
2. Create/Read Databases (Food List, Base Pet List)
3. [SPLASH SCREEN OPENING] Add logo/name + "Press screen to enter"
4. Create Pet images/Sprite animation sheets
5. Create Food images and replace buttons to image buttons
6. Create pop-up menus for shop [DONE]
7. Link buttons to pop up menus [DONE]
8. Pet interactivity (Jump[DONE but only works in idle state], Feed[DONE], Status Check)
9. Include ways to increase Hygiene, Happiness, and Health parameters so it becomes harder for the pet to die
10. Include a way to start the game with an egg that can evolve etc
11. Include a way to save player data (Current pet with parameters, Time exitted, Time entered)

External Resources needed:
1. Art Assets
  - Food
  - Base Pets
  - Backgrounds [DONE]
  - UI Elements
2. Sound Assets
  - Base Music
  - Pet sounds
