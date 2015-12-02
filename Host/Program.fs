module OwinHost
    
    open global.Owin
    open Microsoft.Owin.Hosting
    open Microsoft.Owin.StaticFiles
    open Microsoft.Owin.FileSystems
    open WebSharper.Owin
    open MySitelet

    [<EntryPoint>]
    let Main = function
        | [| rootDirectory; url |] ->
                use server = WebApp.Start(url, fun appB ->
                    let root = @"C:\Projects\Host\MySitelet"
                    let bin = @"C:\Projects\Host\Host\bin\Debug"
                    appB.UseStaticFiles(StaticFileOptions(FileSystem = PhysicalFileSystem(root))) |> ignore
                    appB.UseSitelet(root, Site.sitelet, binDirectory = bin) |> ignore
                )
                
                stdout.WriteLine("Serving {0}", url)
                stdin.ReadLine() |> ignore
                0
            | _ -> eprintfn "Failed to initialise sitelet"
                   1
        | _ ->
            eprintfn "Usage: SiteletFsx ROOT_DIRECTORY URL"
            1
