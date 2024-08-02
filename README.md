# Unity AutoKeystore
Allows store android keystore between sessions, without manual input everytime.

[![NPM Package](https://img.shields.io/npm/v/com.dreamcode.mobile.android-keystore)](https://www.npmjs.com/package/com.dreamcode.mobile.android-keystore)
[![openupm](https://img.shields.io/npm/v/com.dreamcode.mobile.android-keystore?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.dreamcode.mobile.android-keystore/)
[![Licence](https://img.shields.io/npm/l/com.dreamcode.mobile.android-keystore)](https://github.com/dreamcodestudio/com.dreamcode.mobile.android-keystore/blob/main/LICENSE)
![NPM Downloads](https://img.shields.io/npm/dt/com.dreamcode.mobile.android-keystore)

# How do I install AutoKeystore?

<details>
<summary>Install via OpenUPM cli</summary>

```
openupm add com.dreamcode.mobile.android-keystore
```
</details>

<details>
<summary>Install from git URL via Package Manager</summary>

1. Open the menu item `Window > Package Manager`
1. Click `+` button and select `Add package from git URL...`
1. Enter the following URL and click `Add` button

```
https://github.com/dreamcodestudio/com.dreamcode.mobile.android-keystore.git
```
</details>

<details>
<summary>Install via npm</summary>

* Navigate to the `Packages` directory of your project.
* Adjust the [project manifest file](https://docs.unity3d.com/Manual/upm-manifestPrj.html) `manifest.json` in a text editor and add `com.dreamcode` is part of scopes.
```json
  {
    "scopedRegistries": [
      {
        "name": "npmjs",
        "url": "https://registry.npmjs.org/",
        "scopes": [
          "com.dreamcode"
        ]
      }
    ],
    "dependencies": {
      ...
    }
  }
  ```

* Open `Package Manager` and press `Install` button.
<img src="https://user-images.githubusercontent.com/7010398/187045087-76c3bf90-f023-46d5-a794-9657d9398548.png" width="730">

</details>

<details>
<summary>Install via Asset Store</summary>
https://assetstore.unity.com/packages/slug/232044
</details>

# How do I use AutoKeystore?

* Open the setup dialog `Tools > DreamCode > Android > AutoKeystore`

![preview_autokeystore](https://user-images.githubusercontent.com/7010398/188006008-336628ff-39b7-42b9-820b-e2f21a5513da.png)

### Well done 🤝
Now keystore data will be autocomplete on build stage.
