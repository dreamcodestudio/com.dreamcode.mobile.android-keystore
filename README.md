# Unity AutoKeystore
Allows store android keystore between sessions, without manual input everytime.

[![NPM Package](https://img.shields.io/npm/v/com.dreamcode.mobile.android-keystore)](https://www.npmjs.com/package/com.dreamcode.mobile.android-keystore)
[![Licence](https://img.shields.io/npm/l/com.dreamcode.mobile.android-keystore)](https://github.com/dreamcodestudio/com.dreamcode.mobile.android-keystore/blob/main/LICENSE)

### Install from npm 🤖
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

* Open the setup dialog `DreamCode > Android > AutoKeystore`

![preview_autokeystore](https://user-images.githubusercontent.com/7010398/188006008-336628ff-39b7-42b9-820b-e2f21a5513da.png)

### Well done 🤝
Now keystore data will be autocomplete on build stage.
