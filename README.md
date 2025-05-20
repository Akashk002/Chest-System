GamePlay Video -  https://www.loom.com/share/b5b7a9c565e64db79f64deef726ebb67?sid=b8d0e637-a320-4de9-8f72-a20f587ece6d

Playable Link - https://outscal.com/akashkatailiha120/game/play-chest-system-game-4

Game Description 

This game is a dynamic, slot-and-chest-based casual title designed to engage players with its rewarding blend of chance and strategy. Players scroll slots to trigger chest unlocks, earn in-game currency (coins and gems), and experience fluid, event-driven gameplay

Chest & Slot System Architecture Overview


Service Locator Pattern :-
The system uses a central GameService class to act as the gateway for accessing key game services. Implemented as a Singleton via GenericMonoSingleton<T>, this design ensures there is only one service locator instance throughout the game session.

Key Services Registered:

- SlotService: Oversees slot machine operations including spins, chest triggers, and slot-to-chest mappings.

- ChestService: Manages chest states and transitions, including unlock timers and reward distributions.

- EventService: Facilitates a flexible, observer-based system for game-wide events.

 - CurrencyHandler: Handles all aspects of in-game currency, ensuring accurate coin and gem updates with reliable persistence.

MVC (Model-View-Controller) Pattern :-
Slot System:

- Model (SlotModel):

Manages slot data, runtime states, and timing logic.

Contains business rules for slot spins and chest generation.

- View (SlotView):

Handles user interactions, pointer events (enter, exit, click), and visual feedback.

Renders UI components for the slot system.

- Controller (SlotController):

Orchestrates interactions between SlotModel and SlotView.

Manages slot operations, state transitions, and communicates chest-related events.


Chest System:

- Model (ChestModel):

Stores chest data, state, rewards, and unlock timers.

Implements logic for unlock progression and reward determination.

- View (ChestView):

Manages chest animations, visual states, and user interface elements.

Updates display dynamically based on the chest’s state.

- Controller (ChestController):

Coordinates chest state transitions and triggers animations.

Distributes rewards and manages interactions with the slot system.

Observer Pattern :-
Implemented via the EventService and a generic EventController<T>, this pattern decouples event generation from event handling. It allows various components to react to game events through C# events and delegates.

Key Events:

- OnSlotSelect: Fired when a slot is selected, prompting UI updates and chest state evaluations.

- OnFailedString: Triggered to handle failure notifications (e.g., when a player lacks sufficient gems).

Subscribers:

- DisplayOverlayTextHandler: Listens for failure events to provide immediate on-screen feedback.

- UnlockedChest: Responds to slot selection events to update chest visuals and unlock behavior.

Saving System
The saving system combines Unity’s PlayerPrefs for basic value persistence with JSON serialization for more structured, complex data storage.

Currency Saving:

- Managed by CurrencyHandler.

- Persists player coin and gem amounts using PlayerPrefs.

- Automatically reloads saved currency values upon game initialization.

Chest State Saving:

- Handled by ChestService.

- Stores details such as:

Chest index

Chest state (Locked, Unlocking, Opened)

Chest type

Unlocking start time

- Uses a structured class (ChestSavedData) and JSON serialization to accurately save and restore chest states on game restart.

Key Features
- Automatic State Recovery:

Chest unlocking timers and overall progress are preserved between sessions.

The system reliably restores currency amounts and game states after a restart.

- Robust Data Persistence:

Utilizes both simple key-value storage and structured JSON serialization for handling varied data.

- Error Handling & Feedback:

Implements clear error notifications and overlay text to inform players of issues like insufficient currency.

- Modular and Extensible Design:

Adopts design patterns (Service Locator, MVC, Observer) to ensure a clean separation of concerns.

Facilitates the addition of new features, chest types, or enhancements with minimal impact on existing code.
