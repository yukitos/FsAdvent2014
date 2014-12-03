namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("FsAdvent2014")>]
[<assembly: AssemblyProductAttribute("FsAdvent2014")>]
[<assembly: AssemblyDescriptionAttribute("F# Advent Calendar 2014 project")>]
[<assembly: AssemblyVersionAttribute("1.0")>]
[<assembly: AssemblyFileVersionAttribute("1.0")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "1.0"
