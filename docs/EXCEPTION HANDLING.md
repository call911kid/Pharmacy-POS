# Exception Handling — Egyptian Pharmacy POS

## Overview

The system uses a unified, exception-based approach for handling both business rule violations and system failures. Errors are divided into two distinct categories:

- **Expected Business Rule Violations** — The BLL throws specific, custom domain exceptions (e.g., `ValidationException`, `InsufficientStockException`, `EntityNotFoundException`). The UI catches these *specific* exceptions locally behind button clicks and shows actionable, friendly warnings to the cashier.
- **Unexpected, Unrecoverable Failures** — System crashes (e.g., database goes offline, network failure, null references). These are intentionally *not* caught by local `try/catch` blocks. Instead, they bubble all the way up to a global handler in the UI, which safely logs them and prevents a hard crash to the desktop.

---

## Global Exception Handling — `Program.cs`

Three handlers are registered in `Program.cs` before `ApplicationConfiguration.Initialize()` and `Application.Run()`. They must be registered first to ensure no exception at startup escapes before the handlers are in place.

```csharp
Application.ThreadException += (sender, e) =>
    ExceptionHandler.Handle(e.Exception);

Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
    ExceptionHandler.Handle(e.ExceptionObject as Exception);
```

| Handler | Covers |
|---|---|
| `Application.ThreadException` | Unhandled exceptions on the main UI thread |
| `Application.SetUnhandledExceptionMode` | Forces `ThreadException` to fire consistently in both development and production, regardless of whether a debugger is attached |
| `AppDomain.CurrentDomain.UnhandledException` | Unhandled exceptions on background threads. Application cannot recover after this fires — log and notify only |

---

## `ExceptionHandler` — `UI/Exceptions/ExceptionHandler.cs`

All **unhandled** exceptions are routed to the static `ExceptionHandler.Handle()` method. It is responsible for two things only: logging the full exception detail and showing a clean, user-facing message. 

*(Note: If a business exception is correctly caught by the UI form, it will never reach this class).*

```csharp
// UI/Exceptions/ExceptionHandler.cs
internal static class ExceptionHandler
{
    public static void Handle(Exception ex)
    {
        Log(ex);

        switch (ex)
        {
            case EntityNotFoundException e:
                ShowError("The requested record was not found.");
                break;

            // additional custom exceptions added here if they slip past the UI forms

            default:
                ShowError("An unexpected system error occurred. Please contact support.");
                break;
        }
    }

    private static void Log(Exception ex)
    {
        // TODO: wire up Serilog or preferred logger
    }

    private static void ShowError(string message)
    {
        MessageBox.Show(message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
```

**Rules for this class:**
- Log before showing the message. If the logger throws, the user notification must still appear — wrap the log call separately if needed.
- Never pass `ex.Message` directly to `ShowError` for generic exceptions. Log the full detail, show a safe message.
- Never attempt recovery here. This handler is a last resort, not a control flow mechanism.

---

## Custom Exceptions — Current State

### `EntityNotFoundException` — `BLL/Exceptions/EntityNotFoundException.cs`

Thrown when a required record cannot be found. Lives in the BLL so the UI does not need a direct reference to the DAL to catch it.

```csharp
public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string message)
        : base(message) { }
}
```

**Usage:** Thrown by BLL services when a DAL lookup returns null for a record that must exist for the operation to proceed.

---

## Exception Ownership Rules

| Scenario | Approach |
|---|---|
| Anticipated business failure (e.g., Low Stock) | BLL throws a custom domain exception. UI Form explicitly catches it and shows the user a warning. |
| DAL infrastructure failure (e.g., DB Offline) | BLL ignores it. It bubbles directly to the `ExceptionHandler` in `Program.cs`. |
| General validation failure | BLL throws `ValidationException`. UI Form catches and prompts the user to fix their input. |
| Anything unhandled | Bubbles to `ExceptionHandler` in `Program.cs` for logging and a safe UI termination message. |