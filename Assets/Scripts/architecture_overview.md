# System Architecture Overview

This document provides a detailed breakdown of the major systems in the Unity project, explaining their architecture, design patterns, and key implementation details.

## 1. State Management System

The state management is built on a generic and reusable base, which is then specialized for both game-wide states and character-specific states.

-   **Architecture**: The core is the abstract `BaseStatesManager<TStateEnum>` class, which manages a collection of states that implement the `IBaseState<TStateEnum>` interface. This manager handles the logic for transitioning between states, ensuring that `ExitState()` is called on the old state and `EnterState()` is called on the new one. States are identified by an enum (`GameStatesEnum`, `CharacterStateEnum`), making the system type-safe and easy to read.

-   **Implementation**:
    -   `GameStatesManager`: Manages the main game flow (e.g., `Playing`, `Inventory`).
    -   `CharacterStatesManager`: Manages the player's state (e.g., `Move`, `Attack`, `Interact`).
    -   A `TODO` in `PlayingGameState.cs` notes that the dependency on the `GameManager` should ideally be handled via a **Dependency Injection (DI)** framework rather than direct access, which would make the states more modular and easier to test.

## 2. Input System

The project uses Unity's new **Input System package**, providing a robust and flexible way to handle user input.

-   **Architecture**: The `PlayerCharacterInputManager` acts as a centralized hub for all player-related input. It encapsulates the `InputSystem_Actions` object and exposes input data through C# events (e.g., `MoveInputAction`, `AttackPressedAction`).

-   **Implementation**: This approach decouples the rest of the application from the specific input provider. Other systems subscribe to these events instead of checking for input directly. The manager also handles toggling between different action maps (`Player` and `UI`), which is essential for switching between gameplay and menu navigation (like in the inventory screen).

## 3. Interaction System

This system allows the player to interact with objects in the world in a flexible and distance-based manner.

-   **Architecture**: The `PlayerInteractor` component on the player uses a `Collider2D` (as a trigger) to detect nearby interactable objects. Any object that can be interacted with must implement the `IInteractable` interface.

-   **Implementation**: The `PlayerInteractor` maintains a list of all `IInteractable` objects currently within its trigger zone. In its `RecalculateCurrent` method, it determines the closest valid (i.e., not busy) interactable object. If the target interactable changes, it fires an `InteractableChanged` event, allowing other systems (like the UI) to react and display the correct prompt.

## 4. Inventory System (MVP)

The inventory is built using the **Model-View-Presenter (MVP)** pattern, which ensures a clean separation of concerns between data, UI, and logic.

-   **Model**: `InventoryModel` contains the raw data—a list of `InventorySlot` objects. It holds all the logic for adding, swapping, and using items. It is completely independent of the UI and exposes an `OnChanged` event to notify subscribers of any data modifications.

-   **View**: `InventoryView` is a `MonoBehaviour` responsible for rendering the inventory. It listens to user input (clicks, drags) and forwards these actions to the Presenter via C# events.
    -   Two `TODO`s highlight areas for future improvement: implementing **object pooling** for the slot UI to enhance performance and considering a more efficient event system if the current delegate-based one becomes a bottleneck.

-   **Presenter**: `InventoryPresenter` acts as the bridge. It listens to events from the `View` (e.g., `SlotClicked`), processes them by calling methods on the `Model`, and subscribes to the `Model`'s `OnChanged` event to update the `View` whenever the data changes.

-   **Data**: The actual item data is stored in `ItemData` **ScriptableObjects**. A `TODO` here mentions that the `ItemId` uniqueness is currently handled in a simple way, suggesting a more robust system (like a GUID generator or a centralized registry) could be implemented later.

## 5. Sound System

The sound system is managed by a singleton that handles playing sound effects and music.

-   **Architecture**: The `SoundManager` is a `MonoBehaviour` with a static `Instance` property for global access. A `TODO` wisely points out that the current singleton implementation could be made more robust to better handle scene transitions and potential future needs.

-   **Implementation**: It uses a `SoundLibrary` ScriptableObject to map a `SoundsEnum` to specific `SoundData` (another ScriptableObject containing the `AudioClip` and settings). This makes adding or changing sounds a data-driven process that doesn't require code changes. The manager uses two separate `AudioSource` components to handle SFX and Music independently.

## 6. Save/Load System

The system handles the persistence of game data, currently focused on the inventory.

-   **Architecture**: A static `SaveManager` class provides simple `SaveInventory` and `LoadInventory` methods. It does not need to be a `MonoBehaviour`.

-   **Implementation**: The `InventoryModel` is converted to a plain C# object, `InventorySaveData` (a Data Transfer Object or DTO), for serialization. Unity's built-in `JsonUtility` is then used to serialize the DTO to a JSON file, which is stored in `Application.persistentDataPath`. This keeps the save data clean and separate from the game's runtime logic.
