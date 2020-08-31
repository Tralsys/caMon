# コマンドライン引数について
v2.1.2より, caMonはコマンドライン引数によるオプション設定の変更に対応しました.  
以下は, 設定できるオプションの一覧です.  なお, いずれのオプションも「最後に指定された, 有効な設定」が実際に反映されます.

識別子等を解りやすくするため, 二重引用符("")が使用される場合がありますが, これはあってもなくても大丈夫です.
読みやすさ向上のために識別子などが大文字小文字混じった状態で記載されている場合がありますが, 引数解析において, ファイルパスを除き大文字小文字は区別されません.  

## セレクタmod
modへのパスを指定することで, 任意のセレクタmodを指定することができます.  
セレクタを指定しても, 画面表示modが指定されていた場合, そちらが優先して最初に表示されます.

指定がなかった場合, デフォルトのセレクタが使用されます.

## 画面表示mod
modへのパスを指定することで, 任意の画面表示modを指定で読み込むことができます.

指定がなかった場合, セレクタ画面が最初に表示されます.

## WindowState
Windowの初期表示状態を指定します.

以下の識別子のいずれかを用いて設定の変更を宣言します.

- "/wa"
- "/WSta"
- "/WStat"
- "/WState"

上記の識別子にスペースを挟んで続き, 以下の文字列を用いて使用するオプションを決定します.

|標準|最大化状態|最小化状態|
|-|-|-|
|nm|mx|mn|
|Norm|Maxm|Minm|
|Normal|Maximized|Minimized|

デフォルトの設定は「標準(Normal)」です.

## WindowStyle
Windowのスタイルを指定します.  Windowのスタイルについての詳細は, [Microsoft Docs "Window.WindowStyle プロパティ"](https://docs.microsoft.com/ja-jp/dotnet/api/system.windows.window.windowstyle?view=netcore-3.1)をご覧ください.

以下の識別子のいずれかを用いて設定の変更を宣言します.

- "/wy"
- "/WSty"
- "/WStyl"
- "/WStyle"

上記の識別子にスペースを挟んで続き, 以下の文字列を用いて使用するオプションを決定します.

|None|SingleBorderWindow|ToolWindow|ThreeDBorderWindow|
|-|-|-|-|
|no|sb|tl|tb|
|None|SingleBorder|Tool|ThreeDBorder|
||SingleBorderWindow|ToolWindow|ThreeDBorderWindow|

デフォルトの設定は「SingleBorderWindow」です.

## WindowStartupLocation
Windowの初期表示位置設定方法を設定します.

以下の識別子のいずれかを用いて設定の変更を宣言します.

- "/wl"
- "/wstl"
- "/wstlo"
- "/wstloc"
- "/WindowStartupLocation"

上記の識別子にスペースを挟んで続き, 以下の文字列を用いて使用するオプションを決定します.

|CenterOwner|CenterScreen|Manual|
|-|-|-|
|co|cs|mn|
|CenterOwner|CenterScreen|Manual|

デフォルトの設定は「Manual」です.

## ResizeMode
Windowのリサイズに関連する設定を設定します.  各オプション値について, 詳しくは[Microsoft Docs "Window.ResizeMode プロパティ"](https://docs.microsoft.com/ja-jp/dotnet/api/system.windows.window.resizemode?view=netcore-3.1)をご覧ください.

以下の識別子のいずれかを用いて設定の変更を宣言します.

- "/rm"
- "/ResizeMode"

上記の識別子にスペースを挟んで続き, 以下の文字列を用いて使用するオプションを決定します.

|NoResize|CanMinimize|CanResize|CanResizeWithGrip|
|-|-|-|-|
|no|mn|rs|rg|
|NoResize|CanMinimize|CanResize|CanResizeWithGrip|

デフォルトの設定は「CanResize」です.

## Topmost
常にすべてのウィンドウよりも上に表示するかどうかの設定です.

以下の識別子のいずれかを用いて設定の変更を宣言します.

- "/tm"
- "/Topmost"

上記の識別子にスペースを挟んで続き, 以下の文字列を用いて使用するオプションを決定します.
|True|False|
|-|-|
|T|F|
|Tr|Fa|
|Tru|Fal|
|True|Fals|
||False|

デフォルトの設定は「False」です.

## ShowInTaskBar
タスクバーにアイコンを表示させるかどうかの設定です.

以下の識別子のいずれかを用いて設定の変更を宣言します.

- "/st"
- "/ShowInTaskbar"

上記の識別子にスペースを挟んで続き, 以下の文字列を用いて使用するオプションを決定します.
|True|False|
|-|-|
|T|F|
|Tr|Fa|
|Tru|Fal|
|True|Fals|
||False|

デフォルトの設定は「true」です.

## F11Enabled
F11キーによる全画面化機能を有効にするかどうかの設定です.

以下の識別子のいずれかを用いて設定の変更を宣言します.

- "/f11e"
- "/F11Enabled"

上記の識別子にスペースを挟んで続き, 以下の文字列を用いて使用するオプションを決定します.
|True|False|
|-|-|
|T|F|
|Tr|Fa|
|Tru|Fal|
|True|Fals|
||False|

デフォルトの設定は「true」です.

## F12Enabled
F12キーによるWindowStyle変更機能を有効にするかどうかの設定です.

以下の識別子のいずれかを用いて設定の変更を宣言します.

- "/f12e"
- "/F12Enabled"

上記の識別子にスペースを挟んで続き, 以下の文字列を用いて使用するオプションを決定します.
|True|False|
|-|-|
|T|F|
|Tr|Fa|
|Tru|Fal|
|True|Fals|
||False|

デフォルトの設定は「true」です.

## CloseFunctionEnabled
各画面表示modやセレクタmodに実装された, caMon本体にアプリケーション終了を要求する「Close」ボタン機能を有効にするかどうかの設定です.  
あくまで「各modのCloseAppイベントによる終了要求を受けるかどうか」の設定になるので, 各modで独自に終了手段を実装していた場合, それには影響を及ぼしません.

以下の識別子のいずれかを用いて設定の変更を宣言します.

- "/cfe"
- "/CloseFunc"
- "/CloseFunction"
- "/CloseFunctionEnabled"

上記の識別子にスペースを挟んで続き, 以下の文字列を用いて使用するオプションを決定します.

|True|False|
|-|-|
|T|F|
|Tr|Fa|
|Tru|Fal|
|True|Fals|
||False|

デフォルトの設定は「true」です.

## BackFunctionEnabled
各画面表示modやセレクタmodに実装された, caMon本体にセレクタ画面の表示を要求する「BackToHome」ボタン機能を有効にするかどうかの設定です.  

以下の識別子のいずれかを用いて設定の変更を宣言します.

- "/bfe"
- "/BackFunc"
- "/BackFunction"
- "/BackFunctionEnabled"

上記の識別子にスペースを挟んで続き, 以下の文字列を用いて使用するオプションを決定します.

|True|False|
|-|-|
|T|F|
|Tr|Fa|
|Tru|Fal|
|True|Fals|
||False|

デフォルトの設定は「true」です.

## Height
ウィンドウの高さを設定します.

以下の識別子のいずれかを用いて設定の変更を宣言します.

- "/h"
- "/he"
- "/hei"
- "/heig"
- "/heigh"
- "/height"

上記の識別子にスペースを挟んで続き, 任意の整数値にて値を変更できます.

デフォルトの設定は「400」です.

## Width
ウィンドウの幅を設定します.

以下の識別子のいずれかを用いて設定の変更を宣言します.

- "/w"
- "/wi"
- "/wid"
- "/widt"
- "/width"

上記の識別子にスペースを挟んで続き, 任意の整数値にて値を変更できます.

デフォルトの設定は「600」です.

## Left
ウィンドウの表示位置のうち, 左右方向について設定します.  設定値は, メインディスプレイの左端を基準にした相対座標です.

以下の識別子のいずれかを用いて設定の変更を宣言します.

- "/l"
- "/le"
- "/lef"
- "/left"

上記の識別子にスペースを挟んで続き, 任意の整数値にて値を変更できます.

デフォルトの設定は「20」です.

## Top
ウィンドウの表示位置のうち, 上下方向について設定します.  設定値は, メインディスプレイの上端を基準にした相対座標です.

以下の識別子のいずれかを用いて設定の変更を宣言します.

- "/t"
- "/to"
- "/top"

上記の識別子にスペースを挟んで続き, 任意の整数値にて値を変更できます.

デフォルトの設定は「40」です.

以上.