# GitHub Copilot Instructions for [RF] Etched Stone Walls (Continued) Mod

## Mod Overview and Purpose

The **[RF] Etched Stone Walls (Continued)** mod builds upon the natural stone wall smoothing feature in vanilla RimWorld, providing enhanced aesthetic customization options for your colony's interior design. It allows players to etch patterns onto smoothed stone walls, making them visually consistent with constructed walls. Additionally, pawns with artistic skills can add decorative etchings, incorporating random pictorial designs into the walls.

## Key Features and Systems

- **Etching Smoothed Stone Walls:** Transform the appearance of smoothed stone walls to match that of brick-patterned constructed walls.
  
- **Decorative Etching:** Artistic pawns can apply additional decorative patterns, introducing unique pictorial elements into walls.

- **Compatibility:** The mod does not alter existing game mechanics, ensuring broad compatibility with other mods, including those that add new stone types, given they adhere to vanilla naming conventions.

## Coding Patterns and Conventions

- **Class Definitions:** Classes are derived from core RimWorld types, ensuring seamless integration into existing mechanics. Notably:
  - `Designator_EtchWall` and `Designator_EtchWallDecorative` as derivatives of `Designator`.
  - `JobDriver_EtchWall` and `JobDriver_EtchWallDecorative` ensure task execution specifics.
  
- **Static Class Usage:** Used extensively for one-off configurations and utility functions, such as `SmoothableWallUtility_Notify_BuildingDestroying`.

- **Internal and Public Access Modifiers:** Public for general use outside the assembly, internal when intended for use strictly within the mod.

## XML Integration

- **Definitions:** Utilize XML files to define new items and properties, such as wall patterns and designators. Ensure alignment with existing game XML structures for maximum compatibility.

- **Patching:** XML Defs should accommodate added features like meditation stats via seamless integration with existing wall Defs.

## Harmony Patching

- **Utilization:** The mod uses Harmony to intercept and augment base game functionality without direct game code alteration, ensuring better compatibility and stability. Specific patches are found in key utility classes to alter behavior, like `SmoothableWallUtility_Notify_BuildingDestroying`.

- **Implementation:** Ensure patches are minimal and targeted, only affecting necessary methods to enable etching and decorative processes.

## Suggestions for Copilot

- **Code Structuring:** Encourage Copilot to structure generated methods and classes consistently with existing patterns in the mod, ensuring clarity and maintainability.

- **Commenting Standards:** Suggest generating meaningful comments that explain the purpose and function of core methodologies, particularly in complex Harmony patches and job implementations.

- **Integration Points:** Highlight the XML integration process and Harmony patching steps as critical areas where AI suggestions can enhance compatibility and application functionality.

Remember to test any changes in a controlled environment to maintain overall game balance and ensure that the mod works seamlessly alongside other community additions to RimWorld.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.
