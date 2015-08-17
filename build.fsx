// include Fake lib
#r @".packages/FAKE/tools/FakeLib.dll"
open Fake

// Properties
let workingDir = ".working"
let version = "5.0.13"
let downloadUrl = "https://github.com/seiyria/bootstrap-slider/archive/v" + version + ".zip"
let zipFile = workingDir + "/" + version + ".zip"
let publish = true

// Targets
Target "Clean" (fun _ ->
  CleanDir workingDir
)

Target "PublishNuGet" (fun _ ->
  NuGet (fun p ->
    {p with
      Project = "bootstrap-slider"
      Authors = ["seiyria";"ChrisMissal"]
      Description = "Bootstrap-slider is developed and maintained by Seiyria. This NuGet package is maintained by ChrisMissal."
      Summary = "https://github.com/seiyria/bootstrap-slider"
      WorkingDir = (workingDir @@ version)
      OutputPath = (workingDir @@ version)
      Version = version
      Files = [
        ("bootstrap-slider-" + version + "/dist/bootstrap-slider.min.js", Some "/content/scripts", None)
        ("bootstrap-slider-" + version + "/dist/css/bootstrap-slider.min.css", Some "/content/css", None)
      ]
      Publish = publish })
      "bootstrap-slider.nuspec"
)

Target "UnzipRelease" (fun _ ->
  let target = workingDir @@ version
  Unzip target zipFile
)

Target "DownloadRelease" (fun _ ->
  CreateDir workingDir
  let client = new System.Net.WebClient()
  client.DownloadFile(downloadUrl, zipFile)
)

Target "Default" (fun _ ->
  trace "Publishing bootstrap-slider to NuGet..."
)

// Dependencies
"DownloadRelease"
  ==> "UnzipRelease"
  ==> "PublishNuGet"
  ==> "Default"

// start build
RunTargetOrDefault "Default"
