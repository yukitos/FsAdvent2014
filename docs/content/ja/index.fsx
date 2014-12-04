(*** hide ***)
// This block of code is omitted in the generated HTML documentation. Use 
// it to define helpers that you do not want to show in the documentation.
#I "../../bin"

(**
# F# Project Scaffoldを使ってプロジェクトを作成する

この記事は[F# Advent Calendar 2014][fsadvent2014]の4日目です。

前回はotfさんによる[ApiaryProviderで大マッシュアップ時代を生き抜く][otf]でした。

さてここではタイトルの通り、[F# Project Scaffold][fsscaffold]を使って
プロジェクトをセットアップしてみましたという話をしたいと思います。

## [F# Project Scaffold][fsscaffold] を入手する

[F# Project Scaffold][fsscaffold]のページによると、
まずはプロジェクトを複製するか、ProjectScaffoldレポジトリを
各々のレポジトリにコピーしなさいと書かれています。
しかし今回は手っ取り早くGitHubのページから
[ZIPファイルをダウンロード][download]して、
それを展開して使います。

ちなみに想定する環境はWindowsです。
Monoな環境はわからないので誰か他の人にお任せします。

展開先ですが、***ディレクトリの浅い位置*** に展開してください。
たとえば`C:\gh`に`MyProject`というフォルダ名で展開します。
`C:\gh\MyProject\build.cmd`といった具合にファイルが配置されることとします。

もしディレクトリの深い位置に展開してしまうとプロジェクトのビルドに失敗します。
具体的にはNuGetのパッケージを取得後に展開する段階で

> パスの一部が見つかりません

という何を言っているのかわからないエラーに遭遇することになります。
最近のNuGetパッケージはそういう作りなのか、
何故かパッケージ(nupkgでしたっけ？実際は単なるzipファイル)の中に
URLエンコードされたらしいやたらと長いフォルダ名があるのが原因に見えますが果たして。

## プロジェクトの初期設定

展開したファイルを使ってプロジェクトの初期設定をします。
既にこの時点で展開先のフォルダをエクスプローラーで開いていると思うので、
アドレスバーに`cmd`と入力してエンターを押してコマンドプロンプトを立ち上げます。
エクスプローラーが既に居なくなっている場合には
普通にコマンドプロンプトを立ち上げて展開先のフォルダに移動します。

そしておもむろに以下のコマンドを実行します：

    build.cmd

そうするとこれから作成するプロジェクトに関していくつか質問されるので、
それに答えて初期設定をします。
質問内容と対訳、入力例は以下の通りです：

| 質問(原文) | 質問(対訳) | 入力例 |
|:-----------|:-----------|:-------|
| Project Name (used for solution/project files) | プロジェクト名(ソリューションとプロジェクトファイル名に使用されます) | MyProj |
| Summary (a short description) | 要約(短い説明文) | My first project. |
| Description (longer description used by NuGet) | 説明(NuGetで使われる長めの説明文) | My first project created with using F# Project Scaffold. |
| Author | 作者 | Anonymous |
| Tags (separated by spaces) | タグ(半角スペース区切り) | Sample F# Scaffold |
| Github User or Organization | GitHubのユーザ名または組織名 | anonymous |
| Github Project Name (leave blank to use Project Name) | GitHubのプロジェクト名(空の場合はプロジェクト名)| (空のまま) |

GitHubのプロジェクト名まで入力が終わるとそのままプロジェクトのビルドが走ります。
あとはこのままソリューションファイルを開いてコードを作成すればいいんですが、
今回はさらに英語と日本語のドキュメントを用意するところまで頑張ってみたいと思います。

## 日本語ドキュメントの追加

なにはともあれ、日本語ドキュメントの対象になるfsxファイルを用意します。

今更の説明ですが、[F# Project Scaffold][fsscaffold] は
標準でドキュメントの作成機能が備わっていて、
`docs/content` フォルダ以下に`.fsx`または`.md`ファイルを置いておくと
[F# Formatting][fsformatting] の力を借りてこれらのファイルを
htmlファイルに変換してくれます。
変換後のファイルは`docs/output`フォルダ以下に出力されます。
さらに、プロジェクト内のコードにXMLドキュメントコメントを追加しておけば、
作成したプロジェクト内のAPIリファレンスドキュメントも自動的に生成してくれます。

そういうわけなので、`docs/content/index.fsx` を
`docs/content/ja/index.fsx` にコピーして、
中身もそれとわかるように少し書き換えておきます。

### 日本語ファイル用テンプレートの追加

次に日本語ファイル用のテンプレートが必要です。
既にあるものをそのまま使っても問題ないのですが、
昨今は多言語対応が求められる時代ですので、
元のテンプレートは英語用にして、
日本語用には別にきちんと用意しておくにこしたことはないでしょう。

`docs/tools/templates/template.cshtml` を
`docs/tools/templates/ja/template.cshtml` にコピーします。

ここでもやはりそれとわかるようにファイルを編集しておくとよいです。

### ドキュメント生成スクリプトの編集

テンプレートファイルを用意しただけでは誰もそれを使ってくれないので、
ドキュメント生成スクリプトを編集して、
日本語ファイルには日本語テンプレートが使われるようにします。

`docs/tools/generate.fsx` を以下のように書き換えます：

    (前略)

    let docTemplate = formatting @@ "templates/docpage.cshtml"

    // Where to look for *.csproj templates (in this order)
    // Note that the key will be the directory name of language specific contents
    // except "en" which is used as the default language.

    // TODO: Add new entries below when you create more language specific documents.

    let layoutRootsAll =
      dict [ ("en", [templates; formatting @@ "templates"
                     formatting @@ "templates/reference"])
             ("ja", [templates @@ "ja"; formatting @@ "templates"
                     formatting @@ "templates/reference"]) ]

    // Copy static files and CSS + JS from F# Formatting
    let copyFiles () =

    (中略)

    // Build API reference from XML comments
    let buildReference () =
      CleanDir (output @@ "reference")
      let binaries =
        referenceBinaries
        |> List.map (fun lib-> bin @@ lib)
      // NOTE: Currently the API reference is available in English only.
      MetadataFormat.Generate
        ( binaries, output @@ "reference", layoutRootsAll.["en"], 
          parameters = ("root", root)::info,
          sourceRepo = githubLink @@ "tree/master",
          sourceFolder = __SOURCE_DIRECTORY__ @@ ".." @@ "..",
          publicOnly = true, libDirs = [bin] )

    // Build documentation from `fsx` and `md` files in `docs/content`
    let buildDocumentation () =
      let subdirs = Directory.EnumerateDirectories(content, "*", SearchOption.AllDirectories)
      for dir in Seq.append [content] subdirs do
        let sub = if dir.Length > content.Length then dir.Substring(content.Length + 1) else "."
        let langSpecificPath(lang, path:string) = path.Split('/', '\\') |> Array.exists(fun i -> i = lang)
        let layoutRoots =
            let key = layoutRootsAll.Keys |> Seq.tryFind (fun i -> langSpecificPath(i, dir))
            match key with
            | Some x -> layoutRootsAll.[x]
            | None -> layoutRootsAll.["en"] // "en" is the default language
        Literate.ProcessDirectory
          ( dir, docTemplate, output @@ sub, replacements = ("root", root)::info,
            layoutRoots = layoutRoots )

    (後略)

元のファイルとの差分は主にL11-L15で定義されている`layoutRootsAll`の追加です。
これはキーに言語名、値にテンプレートファイルを探す複数の場所を保持しています。
特に`ja`の方では1つめの値が`templates @@ "ja"`となっている点に注意してください。
このようにすることで、先ほど追加した日本語用のテンプレートファイルが
使われるようになります。

また、APIリファレンスは今のところ多言語出力出来ないので英語決め打ちにします(L30)。

L41のヘルパー関数はパスを区切り文字で分割した後、
その中に言語名が含まれている場合には`Some`を返します。
L43で`layoutRootsAll.Keys`のいずれかが
現在処理中のファイルパス`dir`の一部になっているかどうか調べて、
一部になっている場合はその言語用のテンプレートを、
そうでなければ英語用のテンプレートを適用されるようにしています(L45-46)。

> ちなみにF# Dataの`generate.fsx`では`dir.Contains("ja")`ならば
> 日本語テンプレートを使うようになっているのですが、
> これだとたまたまフォルダ名に`ja`が入っているだけで日本語テンプレートが
> 当たってしまうので後々まずいことになりそうな気がします :)

### 言語ページ用のリンクをメニューに追加する

さてここまでで英語と日本語のページが作成できるようになったわけですが、
せっかく用意した日本語ページへのリンクも用意したいところです。

そこで言語毎のページへのリンクとして表示する国旗画像を追加して、
メニューバーにもリンクを追加します。

国旗画像はとりあえず[F# Data][fsdata]のものを<del>ぱちって</del>コピーして

 * docs/files/img/en.png
 * docs/files/img/ja.png

に置きます。

そして各テンプレートファイルを変更します。

`docs/tools/templates/template.cshtml` と
`docs/tools/templates/ja/template.cshtml`
にある`ul#menu`以下にリンクを追加します：

    <li class="nav-header">
      <a href="@Root/ja/index.html" class="nflag"><img src="@Root/img/ja.png" /></a>
      <a href="@Root/index.html" class="nflag nflag2"><img src="@Root/img/en.png" /></a>
      @Properties["project-name"]
    </li>

    <li class="divider"></li>

これだけだと縦並びになってしまったりと都合が悪いので、
プロジェクト固有のCSSを追加して体裁を整えます。

### プロジェクト固有のCSSファイルの追加

`docs/files/content`以下にプロジェクト固有のCSSファイルを追加します。

たとえば`project.css`ファイルを以下の内容で追加して、
言語毎のページリンクを体裁よく表示されるようにします。

    .nav-list > li > a.nflag {
      float:right;
      padding:0px;
    }
    .nav-list > li > a.nflag2 {
      margin-right:18px;
    }

あわせてテンプレートファイル`docs/tools/templates/template.cshtml`と
`docs/tools/templates/ja/template.cshtml`の`head`タグ内に
`link`タグを追加します。

    <link type="text/css" rel="stylesheet" href="@Root/content/project.css" />

## ビルドスクリプトの変更

以上で日英ドキュメントを作成する環境が整備できましたが、
このままビルドしてみて、`docs/output` 以下に出力されるドキュメントを開いてみても
GitHub Pages用のURLで各リンクが生成されるため、CSSなども読み込まれずひどいことになります。

そこでローカル環境でドキュメントをチェックできるように
プロジェクトのフォルダ直下にある`build.fsx`を以下の通り編集します。

    let generateHelp' fail debug =
        let args = if debug then ["--define:HELP"]
                   else ["--define:RELEASE"; "--define:HELP"]
        if executeFSIWithArgs "docs/tools" "generate.fsx" args [] then
            traceImportant "Help generated"
        else
            if fail then
                failwith "generating help documentation failed"
            else
                traceImportant "generating help documentation failed"

    let generateHelp fail =
        generateHelp' fail false

    Target "GenerateHelp" (fun _ ->
        DeleteFile "docs/content/release-notes.md"    
        CopyFile "docs/content/" "RELEASE_NOTES.md"
        Rename "docs/content/release-notes.md" "docs/content/RELEASE_NOTES.md"

        DeleteFile "docs/content/license.md"
        CopyFile "docs/content/" "LICENSE.txt"
        Rename "docs/content/license.md" "docs/content/LICENSE.txt"

        generateHelp true
    )

    Target "GenerateHelpDebug" (fun _ ->
        DeleteFile "docs/content/release-notes.md"    
        CopyFile "docs/content/" "RELEASE_NOTES.md"
        Rename "docs/content/release-notes.md" "docs/content/RELEASE_NOTES.md"

        DeleteFile "docs/content/license.md"
        CopyFile "docs/content/" "LICENSE.txt"
        Rename "docs/content/license.md" "docs/content/LICENSE.txt"

        generateHelp' true true
    )

    "CleanDocs"
      ==> "GenerateHelpDebug"

変更箇所は以下の通りです：

  * 元の`generateHelp`関数の名前を`generateHelp'`に変更。
    あわせてデバッグビルドかどうかを引数に渡せるようにした。
  * ビルドターゲット`GenerateHelpDebug`を追加。
  * `GenerateHelpDebug`を実行する場合には必ず`CleanDocs`を実行して、
    一旦ドキュメントを全消去するようビルドチェインを作成

見ての通り、デバッグ時には`generate.fsx`に`--define:RELEASE`を指定せずに実行するようになりました。

そしてローカル用にドキュメントをビルドする場合、以下のようにターゲットを指定して`build.cmd`を実行します：

    build.cmd GenerateHelpDebug

## 新しい言語用のドキュメントを追加する

英語と日本語以外のドキュメントを追加するために必要な手順は以下の通りです：

1. `docs/content/<lang>` フォルダにファイルを用意する
2. `docs/tools/templates/<lang>` フォルダに言語固有のテンプレートファイルを用意する
3. `docs/tools/generate.fsx` にある `layoutRootsAll` の値を編集する

`layoutRootsAll` の値は

    ("<lang>", [templates @@ "<lang>"; formatting @@ "templates"
                formatting @@ "templates/reference"])

という形式になります。

## ドキュメントをGitHub Pagesとして公開する

GitHub Pagesとしてドキュメントを公開するには、単に`gh-pages`というブランチを作成して、
そのブランチへファイルをコミット＆プッシュするだけです。

既にgitがインストールしてあって、git.exeがパスの通った場所に見つかる場合には

    build.cmd ReleaseDocs

とするだけで`gh-pages`ブランチに`docs/output`以下のファイルをコミットしてくれます。
ひょっとすると`gh-pages`ブランチも作成してくれるかもしれませんが試していません。

手元の環境ではgitがインストールされていなくて、
[SourceTree][sourcetree] しかなかったので、
この場合はターミナルを起動して

    ./build.sh ReleaseDocs

とすればGitHub Pagesにドキュメントをアップロードできます。お手軽です。

## 終わりに

お気づきかとは思いますが、以上のようにして作成したのがまさにこのページです。

[F# Project Scaffold][fsscaffold]を利用すれば
プロジェクトのビルドからドキュメントの生成まで一揃いの環境が手軽に用意できます。
F#のプロジェクトを作成する場合には、GitHub上で
プロジェクトを管理するかどうかに関わらず利用できますので
是非試してみてはいかがでしょうか。

あと上記で変更した多言語対応`generate.fsx`はたぶん便利なのではないかなあと思うので、
後ほどPRを投げてみようかなと目論んでいたりします。

次回はbleis-tiftさんよろしくお願いします。

  [otf]: http://otf.hateblo.jp/entry/2014/12/02/221037
  [fsadvent2014]: http://connpass.com/event/9758/
  [fsscaffold]: https://github.com/fsprojects/ProjectScaffold
  [download]: https://github.com/fsprojects/ProjectScaffold/archive/master.zip
  [fsformatting]: https://github.com/tpetricek/FSharp.Formatting
  [fsdata]: https://github.com/fsharp/FSharp.Data
  [sourcetree]: https://www.atlassian.com/ja/software/sourcetree

*)
