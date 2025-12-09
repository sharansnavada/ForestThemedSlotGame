# Forest Themed Slot Game

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

A small Unity-based slot-machine-style game with a forest theme. This project contains the Unity project files, art assets, and an optional Unity Package for distributing or reusing parts of the project.

## Overview

- Purpose: A forest-themed slot game prototype built with Unity and C#. The project demonstrates UI, simple game mechanics for a 3-reel slot machine, symbol assets, and basic reward/coin handling.
- Language: Primarily C# (Unity scripting).

## Repository structure

- .DS_Store
- Assests/  (art assets — note: directory name spelled "Assests" in the repo)
- UnityProject/  (the Unity project folder — open this with Unity Hub)
- Unity_Package/  (optional package(s) exported from the project)
- readme.md  (this file)
- LICENSE

## Requirements

- Unity: Recommended Unity 2020.3 LTS or later. Use the Unity Hub to open the project.
- Platform: Desktop (Windows/macOS) for development and testing; can be built to other targets via Unity Build Settings.

## How to open and run

# Forest Themed Slot Game - Setup Instructions

## 1. Clone the Repository

git clone https://github.com/sharansnavada/ForestThemedSlotGame.git


## 2. Create a New Unity Project

Open Unity Hub

Click New Project to create a new project

Delete the Scenes folder from the newly created project

Copy all 5 folders from the UnityProject directory inside the cloned repository

Paste them into your newly created Unity project folder

## 3. Open the Project in Unity

Open the Unity project

If Unity asks to upgrade the project, follow the prompts and take a backup if needed

You may be prompted to import TMP_Essentials — proceed with the import

## 4. Open the Main Scene

Go to the Scenes folder in the project

If you cannot find the main scene:

Look inside Assets/Scenes

Or search for SampleScene.unity

## 5. Run the Game

Press the Play button in the Unity Editor to test the slot game

---

## Unity Package

If you want to import or reuse systems from this project, the `Unity_Package` folder contains exported packages that can be imported into other Unity projects via `Assets -> Import Package -> Custom Package...`.

## Adding/Editing Assets

Artwork and audio are in the `Assests` folder. Note the folder name is misspelled; if you prefer the standard Unity `Assets` folder name, back up the files, rename the folder to `Assets`, and re-open the project in Unity.

## Building

- Go to File -> Build Settings.
- Add the main scene to the Scenes In Build list, choose target platform and build options, then click Build.

## Contributing

Contributions are welcome. To contribute:

1. Fork the repository.
2. Create a feature branch: `git checkout -b feature/my-change`.
3. Commit your changes and push them to your fork.
4. Open a pull request describing your changes.

Please open issues for bugs, feature requests, or general questions.

## License

This project is licensed under the MIT License — see the included LICENSE file for details.
