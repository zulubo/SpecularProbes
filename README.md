# SpecularProbes
Bake specular highlights into Unity Reflection Probes, allowing baked lights to cast sharp specular highlights for free.

![Example Reflection Probe](/images/probe.png)

## Why is this cool?
Realtime lights are expensive, and especially in VR must be used sparingly. One downside of baked lights is that by default they will not appear in reflections or as specular highlights. I had the idea of baking specular highlights into reflection probes as small, very bright clusters of pixels. Turns out this works supremely well, and creates convincing specular highlights when combined with PBR. This package makes this easy and convenient for artists.

Check out the difference, here's a scene with some vanilla baked lights.

![Scene Without Specular](/images/scene_nospecular.PNG) 

And then with baked specular lights.

![Scene With Specular](/images/scene_specular.PNG)


## How do I do it?

To mark a light to be included in reflection probes, simply add the `SpecularProbeLight` component to it and configure radius and intensity. To mark a reflection probe to render these specular highlights, add the `SpecularProbeRenderer` component to it. The radius option tells the probe which lights to render, all lights within the radius will be included.

**If using Unity 2019.2 or newer**, Specular Probes will automatically be rendered when baking lighting for a scene. **Otherwise**,To bake manually, use the context menu buttons to either bake the currently selected probe, or all specular probes in the scene. **These buttons may not appear on the component**, and you can also access them through the gear button.

![Probe Renderer Component](/images/component_renderer.PNG)

And that's all you need to know! These components are automatically cleaned up when playing, and there is no overhead or cost in builds of course. I've been using this effect extensively in Vertigo 2 to avoid realtime lights, and it works great. Here are some more screenshots if you need convincing.

![Example Screenshot 3](/images/example_03.PNG)

![Example Screenshot 1](/images/example_01.PNG)

![Example Screenshot 2](/images/example_02.PNG)

## Wait, how does this work?

Good question! It's a really simple trick. When about to render reflection probes, I spawn small brightly colored spheres around lights. That way, they get rendered into the probe as, like I mentioned before, small clusters of ultra bright pixels. I didn't expect this to work as well as it does, but it's almost indistinguishable from real specular highlights!
