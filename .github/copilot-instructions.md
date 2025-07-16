Based on the summarized content provided, here's a detailed `.github/copilot-instructions.md` file for your RimWorld modding project. This file is intended to guide developers using GitHub Copilot to assist with their contributions to the mod.


# GitHub Copilot Instructions for Smooth Stone Walls Mod

## Mod Overview and Purpose

The "Smooth Stone Walls" mod is designed for RimWorld, a colony simulation game, with the aim of enhancing the aesthetic and utility options for wall surfaces within the game. This mod introduces new designators and job drivers that allow players to etch and smooth wall surfaces, adding both functional and decorative enhancements to their colonies.

## Key Features and Systems

- **Wall Etching and Decoration:** Introduces new `Designator` classes for etching walls (`Designator_EtchWall` and `Designator_EtchWallDecorative`), allowing players to create paths and artworks on their stone walls.
- **Job System Integration:** Incorporates the `JobDriver` classes (`JobDriver_EtchWall` and `JobDriver_EtchWallDecorative`) for executing the etching tasks through colonist works, with `DoEffect` methods handling the implementation of etching effects.
- **Work Givers:** The mod uses `WorkGiver_EtchWall` and `WorkGiver_EtchWallDecorative` to appropriately assign tasks to colonists based on their skills and priorities.
- **Wall Utility Enhancements:** Includes functionality via utility classes such as `SmoothableWallUtility_Notify_BuildingDestroying` and `SmoothableWallUtility_Notify_SmoothedByPawn` to manage wall state changes during gameplay.

## Coding Patterns and Conventions

- **Class Structure:** The mod uses both `public` and `internal` static classes, as well as derived classes from core RimWorld types such as `Designator`, `JobDriver`, and `WorkGiver_Scanner`.
- **Method Naming:** Methods are named using the PascalCase convention, and protected methods within job drivers aim for clear intent, such as `DoEffect`.
- **Static Classes:** Utilized for utility functions and default definition classes (`DesignationDefOf`, `JobDefOf`) which hold references to in-game XML definitions.

## XML Integration

- **Definitions and Modifications:** While the specific XML details are not included, the structure suggests integration with XML definitions for designators, job defs, and wall properties.
- **XPath for Parsing:** Consider using XPath for efficient reading and merging of XML assets related to added designators and job definitions.

## Harmony Patching

- **Introduction of Harmony Patches:** Harmony is used for method patching to extend or modify existing game behaviors without altering the original game code.
- **Patch Locations:** Potential patch locations include utility classes and initialization (`RSSW_Initializer`), enabling additional functionality upon game load or specific events like building destruction.

## Suggestions for Copilot

- **Functionality Expansion:** Suggest methods that add new interactions or effects related to desginators.
- **Code Refactoring:** Propose refactoring opportunities to improve code readability and maintainability, such as extracting repetitive logic into utility methods.
- **Debugging Assistance:** Offer hints and solutions for common modding errors such as XML parsing issues or incorrect Harmony patch applications.
- **Code Completion:** Assist in auto-completing RimWorld-specific classes and methods to streamline the development process.

By following these guidelines and utilizing GitHub Copilot, developers can efficiently contribute to the Smooth Stone Walls mod, enhancing its functionality and improving the player experience.


This Markdown document provides a comprehensive guide for developers using GitHub Copilot to work on your modding project, ensuring they understand the project’s structure, coding standards, and integration requirements.
