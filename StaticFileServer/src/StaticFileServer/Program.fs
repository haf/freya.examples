﻿module StaticFileServer.Program

open Freya.Core
open Freya.Inspector
open Freya.Machine.Inspector
open Freya.Pipeline.Operators
open Microsoft.Owin.Hosting

// App

let config =
    { Inspectors = 
        [ freyaRequestInspector
          freyaMachineInspector ] }

let app =
    freyaInspector config >?= Stage1.files

// Katana

type FileServer () =
    member __.Configuration () =
        OwinAppFunc.ofFreya (app)

// Main

[<EntryPoint>]
let main _ = 
    let _ = WebApp.Start<FileServer> ("http://localhost:7000")
    let _ = System.Console.ReadLine ()
    0