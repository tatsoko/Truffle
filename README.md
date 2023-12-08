# Truffle
The working scene is "TrufflePlane".
If you want to test the app, that's the scene you should build to your phone.
Don't forget to allow the app access to your GPS location, otherwise no mushroom will spawn.

The position of a mushroom can be added with the "SetPosition()" function from the "MushroomPosition" script.

Plane detection detects horizontal and vertical planes.
When a player is close to a mushroom location a ray is send from the camera to find a plane to spawn the mushroom on.
The "PlaneObjectSpawner" script has a variable "Distance" that determines how close a player needs to be to a mushroom location before the mushroom is spawned.
The "Distance" variable can be set in the Unity Inspector in the "XR Origin" GameObject.

When a ray hits a detected plane, a mushroom is spawned on that plane.
The mushroom will be spawned at the first plane the ray hits at the mushroom position.

Mulitple mushroom can be spawned by increasing the "Number of Mushroom" from the "PlaneObjectSpawner" script in the Unity Inspector in the "XR Origin" GameObject.
However, at the moment all these mushroom will spawn as fast as possible, meaning they all spawn close to the same position.
A function, to keep a minimum distance between the spawned mushrooms, needs to be added.

More mushroom models can be added to "Mushroom" in the Unity Inspector to spawn different kind of mushroom.
Which model spawns is chosen randomly at the moment.

mapboxk
pk.eyJ1IjoidGF0c29rb2xhIiwiYSI6ImNsb3NtN2lwYjAxdmUybW92ODRrb3Q5eWgifQ.1sLAKVAqj4TeZJylRQAM6Q 
