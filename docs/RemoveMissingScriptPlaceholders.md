# Remove Missing Script Placeholders

## Purpose
Cleanup tool that removes all `MissingScriptPlaceholder` components from scenes after missing script issues have been resolved.

## Companion Tool
Works with **Missing Script Replacer** (Tools > Replace Missing Scripts) which creates these placeholder components when removing broken script references.

## Remove Missing Script Placeholders

### Purpose
Cleanup tool that removes all `MissingScriptPlaceholder` components from the scene, typically used after fixing missing script issues.

### Usage
1. **Tools > Cleanup > Remove Missing Script Placeholders**
2. Tool automatically finds and removes all placeholder components

### Features
- Uses `Undo.DestroyObjectImmediate()` for undo support
- Console reports removal count with 🧼 emoji
- Processes all GameObjects in scene

## Workflow

### Typical Usage Pattern
1. **Initial Cleanup**: Run Missing Script Replacer to eliminate console errors
2. **Development**: Work with placeholders visible in Inspector
3. **Script Restoration**: Manually assign replacement scripts to GameObjects
4. **Final Cleanup**: Run Remove Missing Script Placeholders to clean up

### Multiple Scene Support
Both tools process only the currently active scene. For multiple scenes:
- Open each scene individually
- Run tools as needed
- Save scenes after processing

## Technical Details

- **Unity Version**: 6+
- **Location**: Place both scripts in `Assets/Editor/` folder
- **Dependencies**: Both tools reference `MissingScriptPlaceholder` component
- **Undo Support**: Cleanup tool supports Unity's undo system

## Benefits

- **Error Elimination**: Removes console spam from missing scripts
- **Visual Markers**: Placeholders clearly show where scripts were missing
- **Safe Operation**: Uses Unity's built-in utilities and undo system
- **Workflow Flexibility**: Can be run multiple times safely
