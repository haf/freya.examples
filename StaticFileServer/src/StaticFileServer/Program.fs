module StaticFileServer.Program

open Freya.Core
open Suave.Logging
open Suave.Web
open Suave.Types
open Suave.Owin

// App

let app =
    Stage4.files

// Suave

let config =
    { defaultConfig with
        bindings = [ HttpBinding.mk' HTTP "0.0.0.0" 7000 ]
        logger = Loggers.saneDefaultsFor LogLevel.Verbose }

// Main

[<EntryPoint>]
let main _ =
    printfn "Listening on port 7000 and looking in %s" Prelude.root.FullName
    let owin = OwinApp.ofAppFunc "/" (OwinAppFunc.ofFreya app)
    startWebServer config owin
    0