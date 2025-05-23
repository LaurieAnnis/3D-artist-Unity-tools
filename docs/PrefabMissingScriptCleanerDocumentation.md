# PrefabMissingScriptCleaner Documentation

## Purpose
Removes missing script components from prefabs in Unity 6.1+. Solves the common issue where deleted or moved scripts leave "Missing (Mono Script)" references that prevent prefab saving and cause console errors.

## Features
- Batch processes all prefabs in selected folder
- Handles nested prefab hierarchies and variants
- Uses Unity's native removal methods for safe operation
- Provides progress feedback and result statistics
- Includes Undo support for operation reversal

## Installation
1. Create `Editor` folder in your project (if it doesn't exist)
2. Place `PrefabMissingScriptCleaner.cs` script in the `Editor` folder
3. Script compiles automatically

## Usage
1. Open **Tools > Clean Missing Scripts from Prefabs**
2. Select target folder containing prefabs in **Select Folder** field
3. Click **Clean Missing Scripts** button
4. Monitor progress bar during processing
5. Review results showing processed prefabs and removed components

## Technical Details
- Uses `GameObjectUtility.RemoveMonoBehavioursWithMissingScript()` for safe removal
- Recursively processes prefab sources and variants to prevent component reappearance
- Registers operations with Undo system for reversibility
- Batches asset operations for optimal performance

## When to Use
- After deleting or moving script files
- Before building to eliminate console errors
- When refactoring codebase with script reorganization
- During project cleanup or migration

## Safety Notes
- Always backup project before running on large datasets
- Use Undo (Ctrl+Z) to reverse operations if needed
- Test on small folder first to verify expected behavior
