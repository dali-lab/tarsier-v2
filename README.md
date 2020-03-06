# tarsier
<a rel="license" href="http://creativecommons.org/licenses/by-nc-sa/4.0/"><img alt="Creative Commons License" style="border-width:0" src="https://i.creativecommons.org/l/by-nc-sa/4.0/88x31.png" /></a><br />This work is licensed under a <a rel="license" href="http://creativecommons.org/licenses/by-nc-sa/4.0/">Creative Commons Attribution-NonCommercial-ShareAlike 4.0 International License</a>.


# Tarsier Goggles
The Tarsier Goggles Project aims to let a user experience the world through the eyes of a tarsier. The experience focuses on different vision adaptions and the everyday life of these unique animals.

## Setup for development

### Prerequisistes

You will need a VR-ready Windows PC, with a copy of Unity 2019.3. It is also essential to have an Oculus Quest headset with all hardware installed and the software (including SteamVR) configured and updated.

### Download instructions

Fork or clone the project using git to begin development!

## Setup for play

### Prerequisites 

You will need a VR-ready Windows PC, with a copy of Unity 2019.3. It is also essential to have an Oculus Quest headset with all hardware installed and the software (including SteamVR) configured and updated.

### Download instructions

1. Download the [Tarsier.VR.build](https://github.com/dali-lab/tarsier-v2) to anywhere on your computer.
2. Extract the `Tarsier VR Build` folder from the zip file, and enter that directory. 
3. Ensure that the SteamVR compositor is running (that SteamVR has started up, sensed the hardware, and is currently in the SteamVR Home app)
4. Run the `Tarsier-Goggles` executable file.

__If the `Tarsier-Goggles` executable file and the `Tarsier-Goggles_Data` folder are in different directories, the app may not run.__

### Gameplay

The user will start in the Home Lobby scene, where there will be an optional tutorial. To transition to another scene the user will pull a pair of goggles to their face.

## Overview of Developement Implementation

### Vision
The toggled vision is created with components on the SteamVR Camera Eye Game object.

### External Assets


### Post Processing
We use the Post Processing Component on the Camera Eye object for the depth of field effect

## Tech Stack
- Unity3D
- Maya
