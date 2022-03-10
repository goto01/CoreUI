# CoreUI 0.2.0
Simple Pixel Perfect UI system for the Unity. Asset rsender UI elements according to the size of the pixel.

## Preview
<p align="left">
  <img src="./Preview/Adventur.gif" Height="350"/> 
  <img src="./Preview/Result.png" Height="350"/>   
  <img src="./Preview/ResultGif.gif" Height="350"/>
  <details>
    <summary>Generated meshes</summary>
    <img src="./Preview/Mesh.gif" Height="350"/>
    <img src="./Preview/Raw.png" Height="350"/>
  </details>    
</p>

## Implemented UI elements:
<details>
  <summary>Window</summary>
  <img src="./Preview/Elements/Window.gif" Height="350"/>
</details>  
<details>
  <summary>Image</summary>
</details> 
<details>
  <summary>Flexible Image (Progress bar)</summary>
  <img src="./Preview/Elements/FlexibleImage.gif"/>
</details> 
<details>
  <summary>Slider(Vertical and horizontal)</summary>
  <img src="./Preview/Elements/ProgressBar.gif" Height="350"/>
</details> 
<details>
  <summary>Button</summary>
  <img src="./Preview/Elements/Button.gif" Height="350"/>
</details> 
<details>
  <summary>Scroll</summary>
  <img src="./Preview/Elements/Scroll.gif" Height="350"/>
</details> 
<details>
  <summary>Label</summary>
  <img src="./Preview/Elements/Label.gif" Height="350"/>
</details> 
<details>
  <summary>Toggle</summary>
  <img src="./Preview/Elements/Toggle.gif" Height="350"/>
</details> 
<details>
  <summary>Text Field</summary>
  <img src="./Preview/Elements/TextField.gif" Height="350"/>
</details> 

## Text
The UI text element and the CoreUITextMesh(MonoBehaviour) are based on custom text rendering. Generation and editing are implemented as a custom editor.

Text effects:
- Sin effect
- Shake effect
<p align="left">
  <img src="./Preview/TextEffect.gif" Height="200"/>
  <img src="./Preview/TextMeshEditor.gif" Height="200"/>
  <img src="./Preview/FontEditor.png" Height="200"/>
</p>

## Changelogs
<details>
  <summary>0.1.0</summary>
  
  - Fix bug when start editor new font
  - Add rect visualisation for all chars in font editor window
  - Add symbols grid for font editor
  - Add main screen points achors to CameraHandler
  - Add Text property to CoreUIText
  - Show some symbols of text
  - CoreUITextMesh memory optimisation
  - Change GenerateMeshData in the TextMesh(text and color; only text)
  - Add updating of color in the presentation element
  - Add checking for duplication when add symbol
  - Exception when create label with empty string
  - Clear mesh in the UpdateMeshInfo
  - Notificate when scene doesn't have any camera with 'MainCamera' tag
  - Setting of execution order
  - Add Id property to button
  - Optimize text (TextMesh:32)
  - Super Space Beaver CoreUIContainer:150
  - Change Button trigger mechanic
  - Add resizing of FlexibleImage
  - Эффекты не работаю в UI, а только в textmesh
  - When change font content doesn't change
  - Autogeneration of font material
  - Connection material to font
  - Fix singleton
  - Problems when switch scene
  - Remove warnings
  - Fix active and enable (CoreUISimplePresentation 68)
  - Add info window of CoreUI(Version)
  - Bug if repository has none style
  - Create coreUI prefab
  - Check multiline text in scroll
  - Add automatic scroll
  - Font editor window exception when font not selected
</details> 
<details>
  <summary>0.2.0</summary>
  
  - Autoadding styles to repository
  - Styles Repository editor
  - Toggle
  - Update Unity
  - Add a event click releasing when use a click event in a control (prevent a clicking on two buttons on a same position)
  - Textbox
  - Focus
  - Keys navigation
  - Symbol entering
  - Cursor
  - 'Backspace' key
  - 'Delete' key
  - Mouse navigation
  - 'Enter' key
  - Background for touching (the area hides when you delete all symbols) 
  - Set slider start value to start position depends on orientation
  - Bugs
  - Scroll visibility error when in parent scroll
</details> 

## Development progress
[workflowy](https://workflowy.com/s/HwM7.cApHYq98eb)

[Tileset](http://pixeljoint.com/pixelart/73768.htm)
[Tileset](https://www.reddit.com/r/IndieGaming/comments/edxbgy/pixel_art_new_crafting_ui/)
