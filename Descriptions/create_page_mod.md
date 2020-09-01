# 画面表示modをつくる

## はじめに
このページは, 主に「Visual Studioを使用してWPFアプリケーションの開発ができる」方向けに作成しています.  そのため, Page作成の具体的な方法は記載していません.

不安な方は, [「画面表示modのサンプルプロジェクトをつくる」](./make_pagemod_sample/readme.md)をご利用ください.

## 注意事項
- 基本的に, 画面表示modを作成する場合は, caMonソリューションへのプロジェクト追加という形ではなく, 各自でrepositoryを用意してそこに構築してください.
- 最低限必要になるライブラリは, nuget.orgにありますので, そちらをご利用ください.
- ライセンスには十分にお気を付けください.  仮に, 使用しているライブラリに「アプリケーション内で著作権関連の表示」が必要なライセンスである場合, 各modはその要求事項を満たす機能を実装する必要があります.  
  なお, 本体側のライセンスに関しては, 本体のダウンロードファイルに含まれるLICENSEファイルや, インストーラに表示されるライセンス文によって十分に要求事項を満たすため, 各modでの表示は必要ありません.  もちろんあっても構いませんが.


## プロジェクト設定
以下の内容は, すべてVisual Studio 2019 Community Version 16.6.5にて確認しております.  その他の環境では異なる可能性がございますので, ご注意ください.
1. .Net Core WPFのライブラリを作成する要領で, プロジェクトを作成します.
  - プロジェクトタイプは「WPF User Control Library (.Net Core)」が望ましいでしょう.
  - デフォルトで生成される「UserControl1.xaml」と「UserControl1.xaml.cs」は画面表示modとしては必要ありません.
2. nuget.orgより「caMon.IPages.dll」と, 必要に応じて「TR.BIDSSMemLib.rw.dll」や「BIDSData_toBind.dll」, およびそれらが依存するライブラリに参照を追加します.
3. System.Windows.Controls.Pageクラスを継承した任意のクラスを作成します.
4. caMon.IPagesインターフェイスを継承した任意のクラスを作成し, FrontPageプロパティに任意のページのインスタンスを設定します.


## 共有メモリへのアクセス方法
TR.BIDSSMemLib.rw.dllでは, 次回更新にて実装している各メソッドのstatic化を行う予定ですが, ひとまずそれは考えないこととします.

共有メモリの初期化や更新検知のためのAutoReadの開始などは, すべてcaMon本体側から行います.  mod側は, caMon.SharedFuncs.SMLに公開されているTR.BIDSSMemLib.SMemLibのインスタンスを手元にコピーするなり随時参照するなりして使用してください.


## デバッグ用設定
caMon v2.1.2より, 実行時引数による画面表示modの指定が可能になりました.  デバッグ設定にて実行ファイルへのパスを任意の場所に配置したcaMon.exeに指定し, アプリケーション引数に「$(TargetFileName)」など, modファイルへのパスを指定してください.  絶対パスでも, 作業ディレクトリからの相対パスでもどちらでも大丈夫です.

以下に, サンプルプロジェクトでの設定ファイルを示します.

### caMon.pages.sample/Properties/launchSettings.json
~~~
{
  "profiles": {
    "caMon.pages.sample": {
      "commandName": "Executable",
      "executablePath": "$(ProjectDir)..\\publish\\RuntimeNotIncludedPack\\caMon.exe",
      "commandLineArgs": "$(TargetFileName)"
    }
  }
}
~~~
なお, 上記の設定は, caMonリポジトリをcloneし, ソリューションを開き, caMonをRuntimeNotIncludedPack.pubxmlの設定に従ってビルドした場合に, sampleプロジェクトなどが使用できる設定です.  
caMon.slnから独立したソリューションにプロジェクトが所属する場合, "executablePath"は適当なパスに変更してください.


## その他
インターフェイスに含まれる変数の役割や各ライブラリのメソッド解説などは, 各ライブラリの個別解説ページをご覧ください.


## File Description
- [caMon.exe / caMon.dll](./file_desc/caMon.exe.md)
- [caMon.IPages.dll](./file_desc/caMon.IPages.dll.md)
- [caMon.ISelector.dll](./file_desc/caMon.ISelector.dll.md)

- [BIDSData_toBind.dll](./file_desc/BIDSData_toBind.dll.md)
- [IAtsPI.dll](./file_desc/IAtsPI.dll.md)
- [TR.BIDSSMemLib.rw.dll](./file_desc/TR.BIDSSMemLib.rw.dll.md)
- [TR.BIDSSMemLib.structs.dll](./file_desc/TR.BIDSSMemLib.structs.dll.md)
- [TR.SMemCtrler.dll](./file_desc/TR.SMemCtrler.dll.md)
- [TR.SMemIF.dll](./file_desc/TR.SMemIF.dll.md)

- [caMon.pages.e233sp.dll](./file_desc/caMon.pages.e233sp.dll.md)
- [caMon.pages.e235sp.dll](./file_desc/caMon.pages.e235sp.dll.md)
- [caMon.pages.sample.dll](./file_desc/caMon.pages.sample.dll.md)
- [caMon.selector.default.dll](./file_desc/caMon.selector.default.dll.md)

