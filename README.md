# unity-version

GitHub Action that returns unity version in specific folder.

## Inputs

### `project-path`

Path to Unity project. Used to find Unity version. Default `${{ github.workspace }}`.

## Outpust

### `unity-version`

Unity version. Also setted env UNITY_VERSION

### `unity-version-changeset`

Unity version changeset. Also setted env UNITY_VERSION_CHANGESET

## Example usage

```yaml
- name: Checkout project
  uses: actions/checkout@v2

- name: Get Unity version
  id: unity-version
  uses: appegy/unity-version-action@v1   

- name: Prepare build matrix
  run: |
    echo "[Env] Version = $UNITY_VERSION"
    echo "[Env] Changeset = $UNITY_VERSION_CHANGESET"
    echo "[Env] Version = ${{ steps.unity-version.outputs.unity-version }}"
    echo "[Env] Changeset = ${{ steps.unity-version.outputs.unity-version-changeset }}"
```
