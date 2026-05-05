# Location Guesser
## Requirements
- Running the game in a game loop
- Displaying an image with an assigned location
- Opening an interactive map via a button
- Placing a pin on the map
- Displaying the distance between the pin and the actual location of the image
- Selecting an image set before starting the game
- Users can create and log in into an account
- When selecting a set, users have premade sets available
- Each premade set has a leaderboard of player scores
## Technologies and packages used
- CommunityToolkit.Mvvm
- Mapsui.Maui
- SQLite
- Entity Framework
## AI instructions
Act as an expert software architect and developer specializing in the MVVM (Model-View-ViewModel) pattern. Your task is to write high-quality, production-ready, and highly scalable code.

Strictly adhere to the following principles:

1. Strict Separation of Concerns:
   - View: Define the UI declaratively. The code-behind MUST remain empty except for component initialization.
   - ViewModel: Handle presentation logic and state. NEVER reference UI controls, namespaces, or specific UI elements here.
   - Model: Contain pure business logic, data structures, and data access.

2. Binding and Commands:
   - Use Data Binding for all UI state and properties.
   - Handle all user interactions (clicks, gestures) via Commands (e.g., `ICommand` or `RelayCommand`), not event handlers.
   - Implement `INotifyPropertyChanged` efficiently (prefer utilizing modern libraries like `CommunityToolkit.Mvvm` with `[ObservableProperty]` and `[RelayCommand]` attributes if applicable).

3. Architecture & Testing:
   - Use Dependency Injection (DI) to pass services and models into ViewModels.
   - Program against interfaces (e.g., `IDataService`) rather than concrete implementations to ensure the ViewModel is 100% unit-testable.

4. Output Requirements:
   - Follow standard naming conventions (`*View`, `*ViewModel`, `*Model`).
   - Group the code clearly by layer when providing your response.
   - Keep explanations extremely brief; focus on delivering clean, well-structured code.
