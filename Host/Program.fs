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
                    let bin = @"C:\Projects\Host\MySitelet"
                    
                    appB.UseStaticFiles(StaticFileOptions(FileSystem = PhysicalFileSystem(bin))) |> ignore
                    appB.UseSitelet(bin, MySitelet.Site.Main, binDirectory = bin) |> ignore
                )
                
                stdout.WriteLine("Serving {0}", url)
                stdin.ReadLine() |> ignore
                0
            | _ -> eprintfn "Failed to initialise sitelet"
                   1
        | _ ->
            eprintfn "Usage: SiteletFsx ROOT_DIRECTORY URL"
            1
