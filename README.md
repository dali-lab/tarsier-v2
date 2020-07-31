# Anivision
<a rel="license" href="http://creativecommons.org/licenses/by-nc-sa/4.0/"><img alt="Creative Commons License" style="border-width:0" src="https://i.creativecommons.org/l/by-nc-sa/4.0/88x31.png" /></a><br />This work is licensed under a <a rel="license" href="http://creativecommons.org/licenses/by-nc-sa/4.0/">Creative Commons Attribution-NonCommercial-ShareAlike 4.0 International License</a>.


# Anivision
The Anivision Project aims to let a user experience the world through the eyes of various animals. The experience focuses on different vision adaptions and the everyday experience of being another animal.

![Screenshot of Bee Vision scene](https://i.imgur.com/nKeaC5o.png)

# Architecture

## Tech Stack 🥞
- Unity3D (2019.4.1f1)
- Maya

## Setup steps

### Prerequisistes

You will need Unity Hub installed, as well as Android Development Studio. Follow [these instructions](https://circuitstream.com/blog/oculus-quest-unity-setup/) as necessary to configure the Quest (if not already configured), Android Dev Studio, and Unity Hub. However, the article is a bit outdated so only follow the instructions up to "Install and run Unity". For configuring the build, player, and XR settings, refer to [this](https://developer.oculus.com/documentation/unity/unity-conf-settings/) instead.

### Development Setup instructions

1. Fork or clone the project using git to a local space.
2. Open the project in Unity Hub and, if prompted, install the correct version of Unity (2019.4.1f1) to use for development.
3. In `File/Build Settings`, change development platform to Android. Follow [these instructions](https://developer.oculus.com/documentation/unity/unity-conf-settings/) to configure your build settings.

The Oculus plugin is already in the repository, and therefore do not need to be specially installed.

**For tips and information on how to start contributing, check out our [wiki](https://github.com/dali-lab/tarsier-v2/wiki).**

### Building and Running

To build, connect the Oculus Quest to the computer via USB.

If ADB (Android Development Bridge, included in Android Studio) has been properly installed, you will be able to select the device in `File/Build Settings`. From there, you may Build and Run.

If you choose just the Build option, this will create an apk file that will be saved onto your computer. Using the command line, navigate to the location where the file has been saved and type `adb install -r [yourFilename].apk`. This will load the project onto your Quest, which can be found under Unknown Sources part of your library.


### Gameplay

The user will start in the Home Lobby scene, where there will be an optional tutorial. To transition to another scene the user will pull a pair of goggles to their face.

## Authors
* Cathy Wu 2021, developer
* Amon Ferri 2023, developer
* Bill Tang 2020, developer
* Dorothy Qu 2019, designer
* Kristie Chow 2020, PM
* Jasmine Mai 2020, Mentor

## Acknowledgments 🤝
We would like to thank our advisor, Sam Gochman, for this time and expertise. We also would like to thank DALI Staff and Professor Mahoney for their advice and continued support.

---
Designed and developed by [@DALI Lab](https://github.com/dali-lab)
