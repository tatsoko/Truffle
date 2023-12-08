# Truffle
In the scenes folder, there are three scenes where I tested the different methods Unity offers for tracking surfaces.
Ultimately, I've chosen to go with plane detection.
Therefore, the working scene is "TrufflePlane".
If you want to test the app, that's the scene you should build to your phone.

Plane detection detects horizontal and vertical planes.
When a ray hits a detected plane, a mushroom is spawned on that plane.
At the moment the spawning function doesn't take GPS locations into account.
That means, the mushroom will be spawned at the first plane the ray hits.

However, when you allow the app to access your GPS, the app already displays the user's location.

Mulitple mushroom can be spawned by increasing the "Number of Mushroom" in the Unity Inspector.
However, at the moment all these mushroom will spawn as fast as possible, meaning they all spawn close to the same position.
A function, to keep a minimum distance between the spawned mushrooms, needs to be added.

More mushroom models can be added to "Mushroom" in the Unity Inspector to spawn different kind of mushroom.
