# Missing Script Replacer

## Purpose

Finds and replaces missing script components in Unity scenes with placeholder components to prevent console errors and maintain scene stability.

## Problem Solved

Missing scripts in Unity scenes cause:
- Red "Missing (Mono Script)" entries in the Inspector
- Console errors during gameplay
- Potential runtime issues
- Difficulty identifying where scripts were originally attached

## Solution

The script automatically:
1. Scans all GameObjects in the active scene
2. Identifies components with missing script references
3. Removes the broken script entries
4. Adds `MissingScriptPlaceholder` components as markers
5. Preserves GameObject hierarchy and other components

## Usage

### Installation
1. Create an `Editor` folder in your project's `Assets` directory if it doesn't exist
2. Place `MissingScriptReplacer.cs` in the `Editor` folder

### Running the Tool
1. Open the tool: **Tools > Replace Missing Scripts**
2. Click **"Find and Replace Missing Scripts in Scene"**
3. Review console output for results

### Results
- Console shows count of replaced scripts
- Placeholder components appear in Inspector with note: "This component replaced a missing script"
- Scene is marked dirty for saving

## Features

- **Safe Operation**: Uses Unity's built-in utilities for reliable script removal
- **Error-Free**: Handles missing MonoScript references without console errors
- **Reversible**: Placeholder components can be manually removed if needed
- **Scene Preservation**: Maintains all other components and GameObject properties
- **Multiple Runs**: Can be run multiple times safely on the same scene

## Technical Details

- Uses `GameObjectUtility.GetMonoBehavioursWithMissingScriptCount()`
- Uses `GameObjectUtility.RemoveMonoBehavioursWithMissingScript()`
- Compatible with Unity 6+
- Editor-only tool (requires UnityEditor namespace)

## Limitations

- Only processes the currently active scene
- Does not restore original script functionality
- Placeholders must be manually assigned to replacement scripts if needed
