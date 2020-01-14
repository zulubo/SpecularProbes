# SpecularProbes
Bake specular highlights into Unity Reflection Probes, allowing baked lights to cast sharp specular highlights for free.

## Why is this cool?
Realtime lights are expensive, and especially in VR must be used sparingly. One downside of baked lights is that by default they will not appear in reflections or as specular highlights. I had the idea of baking specular highlights into reflection probes as small, very bright clusters of pixels. Turns out this works supremely well, and creates convincing specular highlights when combined with PBR. This package makes this easy and convenient for artists. 

Check out the difference, here's a scene with some vanilla baked lights.

![Scene Without Specular](/images/scene_nospecular.PNG) 

And then with baked specular lights.

![Scene With Specular](/images/scene_specular.PNG)


## How do I do it?

To mark a light to be included in reflection probes, simply add the `<SpecularProbeLight>` component to it and configure radius and intensity. To mark a reflection probe to render these specular highlights, add the `<SpecularProbeRenderer>` component to it. The radius option tells the probe which lights to render, all lights within the radius will be included.

Specular Probes will automatically be rendered when baking lighting for a scene. To bake manually, use the context menu buttons to either bake the currently selected probe, or all specular probes in the scene. **These buttons may not appear on the component**, and you can also access them through the gear button.



