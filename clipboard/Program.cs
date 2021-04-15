using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Collections.Specialized;

namespace clipboard
{
    class Program
    {
        [DllImport("user32.dll")]
        static extern IntPtr CloseClipboard();
        [STAThread]
        static int Main(string[] args)
        {
            var audioP = new Option<FileInfo>("--audio", "filepath of audio");
            audioP.AddAlias("-a");
            var dataP = new Option<List<TypeData>>("--data", description: "list of data: format1,filepath1 [format2,filepath2 ...]");
            dataP.AddAlias("-d");
            var filesP = new Option<List<FileInfo>>("--files", "list of files: filepath1 [filepath2 ...]");
            filesP.AddAlias("-f");
            var imageP = new Option<FileInfo>("--image", "filepath of image, not set to dib default");
            imageP.AddAlias("-i");
            var textP = new Option<string>("--text", "text");
            textP.AddAlias("-t");
            // Create a root command with some options
            var rootCommand = new RootCommand
            {
                audioP,
                filesP,
                imageP,
                textP,
                dataP
            };

            rootCommand.Description = "copy to windows clipboard with command";

            rootCommand.Handler = CommandHandler.Create<FileInfo,List<FileInfo>, FileInfo, string, List<TypeData>>((audio, files, image, text, data) => {
                Clipboard.Clear();
                DataObject d = new DataObject();
                var audioPath = audio?.FullName ?? "";
                if (audioPath.Length > 0 && File.Exists(audioPath)) d.SetAudio(Helper.FileContent(audioPath));
                var imagePath = image?.FullName ?? "";
                if (imagePath.Length > 0 && File.Exists(imagePath)) d.SetImage(Image.FromFile(imagePath));
                var textLength=text?.Length??0;
                if (textLength > 0) d.SetText(text);
                if (files.Count > 0)
                {
                    StringCollection s = new StringCollection();
                    foreach (FileInfo file in files)
                    {
                        var filePath= file?.FullName ?? "";
                        if (filePath.Length > 0 && File.Exists(filePath)) s.Add(filePath);
                    }
                    if(s.Count>0)d.SetFileDropList(s);
                }
                if (data.Count > 0)
                {
                    foreach (TypeData item in data)
                    {
                        item.SetToData(d);
                    }
                }
                try
                {
                    CloseClipboard();
                    Clipboard.SetDataObject(d, true,3,200);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            });

            return rootCommand.InvokeAsync(args).Result;
        }
    }
}
