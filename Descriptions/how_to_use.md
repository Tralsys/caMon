# caMonの使い方 基本編

## 動作環境
### スタンドアロン(portable)版
少なくとも, Windows 10 Home x64 Build 19041.388での動作は確認できています.  ランタイムに依存しないため, Windows7を始めとする過去のOS, あるいは将来リリースされるWindows OSにおいて正常に動作するかどうかはわかりません.

x86オペレーティングシステムの場合はx86版が, x64オペレーティングシステムの場合はx86版とx64版の両方を使用できます.  Arm環境は知りません.

ファイルサイズが大きいため, 圧縮ファイルの展開に少なくとも300MB以上の空き容量が必要になると思われます.

### ランタイム依存(RuntimeNotIncludedPack)版
.Net Core Desktop Runtimeがインストールされている環境で動作します.  [.Net Core Desktop Runtimeのダウンロードはこちら](https://dotnet.microsoft.com/download/dotnet-core/current/runtime)  なお, ランタイムのx86/x64の選択は, 各自のOSと一致させてください(x86 OSの場合はx86版を, x64 OSの場合はx64版をダウンロード/インストールしてください).


## インストール方法例
1. 本体の圧縮ファイルをダウンロードする
2. ダウンロードした圧縮ファイルを, 任意の場所に展開する
3. 展開先の, caMon.exeが存在するフォルダ内に「mods」フォルダを作成する
4. 任意の画面表示modをダウンロードし, 先ほど作成した「mods」フォルダ内に配置する

ランタイムがインストールされていない場合, この状態でRuntimeNotIncludedPackのcaMon.exeを起動した場合, ランタイムがインストールされていない旨表示されます.  その場合は, ランタイムをインストールしたうえで, 再度実行してください.

上記作業を完了させた状態で, 「caMon.exe」をダブルクリックにて実行すると, セレクタのmodリストに, ダウンロードしてきたmodが表示されているはずです.


## BIDSのインストール
[BVE5やBVE6でBIDS Shared Memory Libraryを使用したい場合は, こちらのページをご確認ください.](https://github.com/TetsuOtter/BIDSSMemLib/wiki/bve5)  
[openBVEでBIDS Shared Memory Libraryを使用したい場合は, こちらのページをご確認ください.](https://github.com/TetsuOtter/BIDSSMemLib/wiki/obve)


## 操作方法
オプション設定の変更は, コマンドライン引数(実行時引数)によって行います.  [詳細は, こちらをご確認ください.](./CmdLArgs.md)

各modの操作方法は, 各modの解説ページをご確認ください.

- [caMon.pages.e233sp.dll](./file_desc/caMon.pages.e233sp.dll.md)
- [caMon.pages.e235sp.dll](./file_desc/caMon.pages.e235sp.dll.md)
- [caMon.pages.sample.dll](./file_desc/caMon.pages.sample.dll.md)
- [caMon.selector.default.dll](./file_desc/caMon.selector.default.dll.md)
