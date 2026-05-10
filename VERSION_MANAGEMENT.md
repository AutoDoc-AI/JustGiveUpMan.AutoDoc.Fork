# Automatic Version Management

## Summary
The version number is now automatically updated with every build:

- **Debug Build**: The patch number is incremented.
  - Example: `v1.0.0.1` → `v1.0.0.2`

- **Hotfix Build**: The hotfix number is incremented, and the patch number is reset to 0.
  - Example: `v1.0.0.5` → `v1.0.1.0`

- **Update Build**: The update number is incremented, and the hotfix and patch numbers are reset to 0.
  - Example: `v1.0.1.5` → `v1.1.0.0`

## How It Works

1. **UpdateVersion.ps1** script (`JGUM/Build/UpdateVersion.ps1`):
   - Reads the `SubModule.xml` file.
   - Parses the current version.
   - Updates the version based on the configuration (Debug, Hotfix, or Update).
   - Writes the updated version back to `SubModule.xml`.

2. **JGUM.csproj** and **JGUM.MCMBridge.csproj**:
   - The `UpdateVersion` target runs before the `PostBuildEvent`.
   - The script is executed with `$(Configuration)` and `$(SubModuleTemplatePath)` parameters.
   - After the script completes successfully, the normal post-build process continues.

## Manual Testing

To test with the Debug configuration:
```powershell
powershell -NoProfile -ExecutionPolicy Bypass -File "JGUM\Build\UpdateVersion.ps1" -SubModulePath "JGUM\SubModule.xml" -Configuration "Debug"
```

To test with the Hotfix configuration:
```powershell
powershell -NoProfile -ExecutionPolicy Bypass -File "JGUM\Build\UpdateVersion.ps1" -SubModulePath "JGUM\SubModule.xml" -Configuration "Hotfix"
```

To test with the Update configuration:
```powershell
powershell -NoProfile -ExecutionPolicy Bypass -File "JGUM\Build\UpdateVersion.ps1" -SubModulePath "JGUM\SubModule.xml" -Configuration "Update"
```

## Files

- **JGUM/Build/UpdateVersion.ps1**: PowerShell script (newly created).
- **JGUM/JGUM.csproj**: `UpdateVersion` target added.
- **JGUM.MCMBridge/JGUM.MCMBridge.csproj**: `UpdateVersion` target added.

## Notes

- Version format: `vMAJOR.UPDATE.HOTFIX.DEBUG` (e.g., `v1.0.1.5`).
- The first segment (`v1`) remains constant for the current release.
- The script returns exit code 1 on failure, which prevents the build from continuing.
- XML formatting is preserved (indentation, encoding).
