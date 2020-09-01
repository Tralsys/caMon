# コマンドライン引数について
v2.1.2より, caMonはコマンドライン引数によるオプション設定の変更に対応しました.  
以下は, 設定できるオプションの一覧です.  なお, いずれのオプションも「最後に指定された, 有効な設定」が実際に反映されます.

## 目次
- [目次](#目次)
- [注意事項](#注意事項)
- [設定例](#設定例)
- [変更可能なオプション一覧](#変更可能なオプション一覧)
  - [セレクタmod](#セレクタmod)
  - [画面表示mod](#画面表示mod)
  - [WindowState](#windowstate)
  - [WindowStyle](#windowstyle)
  - [WindowStartupLocation](#windowstartuplocation)
  - [ResizeMode](#resizemode)
  - [Topmost](#topmost)
  - [ShowInTaskbar](#showintaskbar)
  - [F11Enabled](#f11enabled)
  - [F12Enabled](#f12enabled)
  - [CloseFunctionEnabled](#closefunctionenabled)
  - [BackFunctionEnabled](#backfunctionenabled)
  - [Height](#height)
  - [Width](#width)
  - [Left](#left)
  - [Top](#top)


## 注意事項
- 識別子の列挙において, 二重引用符(" ")が使用される場合があります.  
  これはあくまで「何がどこまで識別子なのか」がわかりやすいように使用しているだけであり, 実際の識別子入力で二重引用符を使用する必要はありません.
- 読みやすさ向上のために, 識別子などが大文字小文字混じった状態で記載されている場合があります.  
  特記がある場合を除き, 入力されたコマンドライン引数の解析において大文字小文字は区別されません.
- 「\#」で始まる文字列は

## 設定例
わかりやすように, 識別子とオプション設定用文字列は最大長のものを用います.  
「設定の意味」は, 上の列から下の列に向けて文のような構成をとっています.  なお, 実際に入力する際は異なる種類のオプション設定同士は順不同で, 同じ種類のオプション設定は「最後に入力された有効な設定」が採用されますので, ご注意ください.

|設定の意味|識別子|オプション設定用文字列|
|-|-|-|
|ウィンドウの初期表示位置をマニュアル設定に変更したうえで|/WindowStartupLocation|Manual|
|メインディスプレイ左端から50pxかつ|/Left|50|
|メインディスプレイ上端から100pxの位置に|/Top|100|
|800pxの幅と|/Width|800|
|600pxの高さを持つ|/Height|600|
|タイトルバーが存在せず|/WindowStyle|None|
|最大化/最小化されていない状態の|/WindowState|Normal|
|最小化を含むウィンドウサイズの変更が許可されていない|/ResizeMode|NoResize|
|常にどのウィンドウよりも上に表示される|/Topmost|True|
|タスクバーにアイコンが表示されないウィンドウに|/ShowInTaskbar|False|
|Dドライブ直下に配置された"caMon.pages.sample.dll"を表示させる||D:\caMon.pages.sample.dll|

上記の表の状態を実現するコマンドは, 次のような形になります.
~~~
caMon.exe /WindowStartupLocation Manual /Left 50 /Top 100 /Width 800 /Height 600 /WindowStyle None /WindowState Normal /ResizeMode NoResize /Topmost True /ShowInTaskbar False D:\caMon.pages.sample.dll
~~~
なお, 上記コマンドは次のように短縮できます.  オプション設定の並び順は, 上のコマンドと同じです.  読みやすいよう, 大文字にできる文字はすべて大文字で表記しています.
~~~
caMon.exe /WL MN /L 50 /T 100 /W 800 /H 600 /WY None /WA NM /RM NO /TM T /ST F D:\caMon.pages.sample.dll
~~~
短くしすぎてわかりづらいので, batファイルなどに設定を記述する際は, 長い方の識別子やオプション設定文字列を使用することをお勧めします.  
また, #から始まる文字列はコメント扱いとして無視されますので, 是非ご活用ください.  例えば, 次の二行は全く同じ動作をします.
~~~
caMon.exe #WhatElse-You-Write-This-Sentence-Will-Not-Be-Read /W 400 /H 300
caMon.exe /H 300 /W 400
~~~

## 変更可能なオプション設定一覧
### セレクタmod
modへのパスを指定することで, 任意のセレクタmodを指定することができます.  なお, ファイルパスは大文字小文字が区別されますので, ご注意ください.  
セレクタが指定された場合でも, 画面表示modが指定されていた場合, 画面表示modが優先して最初に表示されます.

指定がなかった場合, デフォルトのセレクタが使用されます.

### 画面表示mod
modへのパスを指定することで, 任意の画面表示modを指定で読み込むことができます.  なお, ファイルパスは大文字小文字が区別されますので, ご注意ください.  

指定がなかった場合, セレクタ画面が最初に表示されます.

### WindowState
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

### WindowStyle
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

### WindowStartupLocation
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

### ResizeMode
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

### Topmost
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

### ShowInTaskBar
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

### F11Enabled
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

### F12Enabled
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

### CloseFunctionEnabled
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

### BackFunctionEnabled
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

### Height
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

### Width
ウィンドウの幅を設定します.

以下の識別子のいずれかを用いて設定の変更を宣言します.

- "/w"
- "/wi"
- "/wid"
- "/widt"
- "/width"

上記の識別子にスペースを挟んで続き, 任意の整数値にて値を変更できます.

デフォルトの設定は「600」です.

### Left
ウィンドウの表示位置のうち, 左右方向について設定します.  設定値は, メインディスプレイの左端を基準にした相対座標です.

以下の識別子のいずれかを用いて設定の変更を宣言します.

- "/l"
- "/le"
- "/lef"
- "/left"

上記の識別子にスペースを挟んで続き, 任意の整数値にて値を変更できます.

デフォルトの設定は「20」です.

### Top
ウィンドウの表示位置のうち, 上下方向について設定します.  設定値は, メインディスプレイの上端を基準にした相対座標です.

以下の識別子のいずれかを用いて設定の変更を宣言します.

- "/t"
- "/to"
- "/top"

上記の識別子にスペースを挟んで続き, 任意の整数値にて値を変更できます.

デフォルトの設定は「40」です.

以上.
