(*** hide ***)
// This block of code is omitted in the generated HTML documentation. Use 
// it to define helpers that you do not want to show in the documentation.
#I "../../bin"

(**

# Create a project with using F# Project Scaffold

This article is the 4th in the [F# Advent Calendar in Japanese][fsadvent] series.

The 3rd is [ApiaryProviderで大マッシュアップ時代を生き抜く][otf]
(How to survive these widely mashed-up days with ApiaryProvider) written by otf.

Here, as titled, I'm trying to create a new project with using [F# Project Scaffold][fsscaffold].

## Getting started

[F# Project Scaffold][fsscaffold]'s [Getting started][gettingstarted] page says:

> This first thing to do is to clone or copy the ProjectScaffold repository to your own workspace.

To make things easier, I [download a ZIP file][download] and use it.

By the way, I'm writing for Windows environment and I don't know how to do this on other environments (help! :)

Extract the file under `C:\gh\MyProject` directory, so now I have `C:\gh\MyProject\build.cmd` etc.

Note that we shouldn't extract it into some deep directory path.
When I extracted the file under `deep\deep\deep\directory\path\on\your\machine`,
I had got a cryptic `Could not find a part of the path` error during building my project.
I suspect that this error has been caused by some NuGet packages which have too long path inside it.

## Initialize my project

To initialize my project, start command prompt via Windows Explorer
(input `cmd` in the address bar and then press enter) or start from Start Menu and move to the project folder.
Then run the following command:

    build.cmd

This command asks me about my project (listed below):

| Question | Example answer |
|:---------|:---------------|
| Project Name (used for solution/project files) | MyProj |
| Summary (a short description) | My first project. |
| Description (longer description used by NuGet) | My first project created with using F# Project Scaffold. |
| Author | Anonymous |
| Tags (separated by spaces) | Sample F# Scaffold |
| Github User or Organization | anonymous |
| Github Project Name (leave blank to use Project Name) | (leave blank) |

After the last question, the build command continues to build my project automatically.
At this point, the newly created solution file and related projects are available.

This time, however, I'm trying to add Japanese documents rather than implementing my project :)

## Add Japanese documents

By default, [F# Project Scaffold][fsscaffold] has an ability to generate project's documents.
All `.fsx` and/or `.md` files under `docs/content` directory are parsed and
then these files will be translated into html formatted documents (can be found under `docs/output`).
This function uses [F# Formatting][fsformatting] to translate from `.fsx` or `.md` to `.html`.
In addition, project's API reference documents will be generated if XML document comments are provided.

To add Japanese documents, run the following command:

    build.cmd AddLangDocs ja

> If we need to create documents for more languages, add more language names to this command:
> 
>     build.cmd AddLangDocs ja de fr zh
> 

// WIP

 [fsadvent]: http://connpass.com/event/9758/
 [otf]: http://otf.hateblo.jp/entry/2014/12/02/221037
 [fsscaffold]: https://github.com/fsprojects/ProjectScaffold
 [gettingstarted]: http://fsprojects.github.io/ProjectScaffold/index.html#Getting-started
 [download]: https://github.com/fsprojects/ProjectScaffold/archive/master.zip
 [fsformatting]: http://tpetricek.github.io/FSharp.Formatting/

*)
