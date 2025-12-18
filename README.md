# ATMApp: A Console-Based ATM Simulation

## Overview

**ATMApp** is a console application developed in C# that simulates the core functionalities of an Automated Teller Machine (ATM). The application is designed to demonstrate fundamental programming concepts, layered architecture (Business Logic, Data Layer, UI), and data handling using JSON for account storage.

## Features

The application provides a user-friendly console interface for performing typical ATM operations, including:

*   **User Authentication:** Secure login using a Card Number and PIN.
*   **Balance Inquiry:** View the current balance of the authenticated account.
*   **Withdrawal:** Withdraw funds with validation checks for sufficient balance and account status.
*   **Account Management:** The system handles account data, including a lock/unlock status.
*   **Data Persistence:** Account data is loaded from and saved to a local JSON file (`ATMjson22.json`).

## Technology Stack

The project is built using the following technologies:

| Technology | Version/Description | Purpose |
| :--- | :--- | :--- |
| **Language** | C# | Primary programming language. |
| **Framework** | .NET 8.0 | Target framework for the console application. |
| **Data Format** | JSON | Used for storing and retrieving account data (`ATMjson22.json`). |

## Dependencies

The following NuGet packages are used in the project:

*   **`Newtonsoft.Json` (v13.0.3):** Essential for serializing and deserializing the account data from the JSON file.
*   **`ConsoleTables` (v2.6.1):** Used to display data, such as account details or transaction history, in a clean, formatted table within the console.
*   **`System.Data.SqlClient` (v4.8.6):** Included for potential future integration with a SQL Server database.

## Project Structure

The application follows a layered architecture to ensure separation of concerns:

| Directory | Description | Layer |
| :--- | :--- | :--- |
| `myATMapp/App` | Application entry point and main program flow. | Presentation/UI |
| `myATMapp/Ui` | Handles user interaction and console output (e.g., `ClsUiHelper`). | Presentation/UI |
| `myATMapp/Bl` | Business Logic layer, containing the core logic for ATM operations (e.g., withdrawal, balance check). | Business Logic |
| `myATMapp/Dl` | Data Layer, responsible for reading and writing account data (e.g., from `ATMjson22.json`). | Data Access |
| `myATMapp/Domain` | Contains the data models (entities) used across all layers (e.g., the Account class). | Domain Model |

## Installation and Setup

To run this project locally, you will need the .NET 8.0 SDK installed.

1.  **Clone the repository:**
    ```bash
    git clone https://github.com/FaridFarid55/ATMApp.git
    cd ATMApp/myATMapp
    ```

2.  **Restore dependencies:**
    ```bash
    dotnet restore
    ```

3.  **Run the application:**
    ```bash
    dotnet run
    ```

## Sample Credentials

The application uses the data from `ATMjson22.json`. You can use the following sample credentials for testing:

| Full Name | Card Number | PIN | Account Balance | Status |
| :--- | :--- | :--- | :--- | :--- |
| Farid Farid | 560600 | 123123 | 50000 | Unlocked |
| Ali ahmed | 606000 | 124124 | 40000 | Unlocked |
| mohamed ail | 606000 | 789789 | 20000 | Locked |

***Note:*** *The third account is initially locked (`"IsLocked": "true"`), which can be used to test the account lock feature.*
