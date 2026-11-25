# NOVA ENGINE 6 - Cloud build plan

## Selected service
Unity Build Automation (UBA), through the Unity Dashboard / Unity DevOps.

Why this service:
- Native cloud build service for Unity projects.
- Supports Git repositories hosted on GitHub.
- Supports Android builds.
- Supports the Unity 6000.0 LTS release line.

## Repository source
- Repository: https://github.com/stevennpllk-crypto/UltraSpaceApp
- Working branch: `nova-engine-6-import`
- Do not modify `main`.

## Unity editor version
The project bootstrap pins Unity `6000.0.85f2`, a patched Unity 6.0 LTS editor release. UBA must be configured to use this exact editor version if it is available in the builder selection.

## Project bootstrap added
- `Packages/manifest.json` with an empty dependency map; the nine NOVA packages are embedded under `Packages/`.
- `ProjectSettings/ProjectVersion.txt` pinned to Unity `6000.0.85f2`.
- `Assets/NOVA/README.md` documenting the asset root and missing authored game content.

This is only a minimal project bootstrap. It has not been opened by Unity Editor and has not been validated by Unity Package Manager.

## Remaining blockers before an APK build
1. Inspect package source, assembly definitions, and inter-package dependencies.
2. Run Unity import/compilation and fix any C# or package-resolution errors.
3. Add and validate a startup scene; the source archive did not include game scenes, prefabs, textures, audio, or other authored game content.
4. Configure Android player settings and build target.
5. Connect UBA to this GitHub repository and select branch `nova-engine-6-import`.
6. Trigger the Android build, inspect the full log, and download the APK only if the build succeeds.

## Account and access requirements
- A Unity ID and access to Unity DevOps / Build Automation.
- GitHub repository read access granted to UBA. For a private repository, authorize it using the supported GitHub token or SSH-key flow.
- Check the current included build-minute/storage allowance and any pay-as-you-go charges in the Unity Dashboard before starting builds.
- Never put GitHub tokens, Unity passwords, license files, or signing keystores in this repository.

## Device constraint
This workflow is intended to be operated through the Unity Dashboard in a mobile browser; it does not require installing or running Unity Editor on the user's Android phone. Initial account authorization and dashboard setup still require the user to sign in and approve access.

## Verification boundary
Choosing UBA, adding a minimal project bootstrap, and documenting this plan do not mean the repository is connected to UBA, that Unity compilation has run, or that an APK exists. Those states must be verified separately from the UBA build history and build logs.
