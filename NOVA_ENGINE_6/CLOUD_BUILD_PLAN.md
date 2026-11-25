# NOVA ENGINE 6 - Cloud build plan

## Selected service
Unity Build Automation (UBA), through the Unity Dashboard / Unity DevOps.

Why this service:
- Native cloud build service for Unity projects.
- Supports Git repositories hosted on GitHub.
- Supports Android builds.
- Current supported editor list includes Unity 6000.0 LTS.

## Repository source
- Repository: https://github.com/stevennpllk-crypto/UltraSpaceApp
- Working branch: `nova-engine-6-import`
- Do not modify `main`.

## Current blocker
The branch currently contains nine NOVA Unity package folders and 58 C# files, but it does not yet contain a complete Unity project root. In particular, `Assets/` and `ProjectSettings/` are absent. UBA cannot be treated as ready to build an APK until a valid Unity project structure is assembled and the packages are wired into its package manifest.

## Required preparation before the first cloud build
1. Review package source, assembly definitions, and inter-package dependencies.
2. Assemble a valid Unity 6 project root on this branch, including a correct `Packages/manifest.json` and Unity project settings.
3. Select a supported Unity 6000.0 LTS editor version in UBA that matches the project's actual editor version.
4. Connect UBA to this GitHub repository and select branch `nova-engine-6-import`.
5. Configure an Android build target (APK) and trigger a build.
6. Inspect the Unity build log and download the APK only if the build succeeds.

## Account and access requirements
- A Unity ID and access to Unity DevOps / Build Automation.
- GitHub repository read access granted to UBA. For a private repository, authorize it using the supported GitHub token or SSH-key flow.
- Check the current included build-minute/storage allowance and any pay-as-you-go charges in the Unity Dashboard before starting builds.
- Never put GitHub tokens, Unity passwords, license files, or signing keystores in this repository.

## Device constraint
This workflow is intended to be operated through the Unity Dashboard in a mobile browser; it does not require installing or running Unity Editor on the user's Android phone. Initial account authorization and dashboard setup still require the user to sign in and approve access.

## Verification boundary
Choosing UBA and documenting this plan does not mean the repository is connected to UBA, that Unity compilation has run, or that an APK exists. Those states must be verified separately from the UBA build history and build logs.
