# Remove Missing Scripts Tool Documentation

## Purpose
Unity Editor tool that automatically removes missing script references from GameObjects across all loaded scenes. Missing scripts occur when script files are deleted or moved but references remain on GameObjects, showing as "Missing (Mono Script)" components in the Inspector.

## Usage
**Menu Access:** `Tools > Cleanup > Remove Missing Scripts in Scene`

## Functionality
- Scans all loaded scenes in the current scene setup
- Iterates through every GameObject (including inactive ones)
- Identifies and removes missing MonoBehaviour script references
- Registers undo operations for each cleaned GameObject
- Marks scenes as dirty to ensure changes are saved
- Reports total missing scripts removed and GameObjects processed

## Implementation Requirements
1. Place script in `Editor` folder (required for `[MenuItem]` attribute)
2. Add missing using statement: `using System.Linq;`

## When to Use
- After deleting or renaming script files
- Before building to eliminate console warnings
- During project cleanup and maintenance
- When transferring projects between team members

## Output
Console message showing removal count and total GameObjects processed.

**Note:** The script has a missing `using System.Linq;` directive required for the `.Select()` method.
