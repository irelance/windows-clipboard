using System;
using System.IO;
using System.Windows.Forms;

namespace clipboard
{
    class Helper
    {
        static public byte[] FileContent(string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                try
                {
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, (int)fs.Length);
                    return buffer;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        static public void SupportDataType()
        {
            Console.WriteLine(DataFormats.Bitmap);
            Console.WriteLine(DataFormats.CommaSeparatedValue);
            Console.WriteLine(DataFormats.Dib);
            Console.WriteLine(DataFormats.Dif);
            Console.WriteLine(DataFormats.EnhancedMetafile);
            Console.WriteLine(DataFormats.FileDrop);
            Console.WriteLine(DataFormats.Html);
            Console.WriteLine(DataFormats.Locale);
            Console.WriteLine(DataFormats.MetafilePict);
            Console.WriteLine(DataFormats.OemText);
            Console.WriteLine(DataFormats.Palette);
            Console.WriteLine(DataFormats.PenData);
            Console.WriteLine(DataFormats.Riff);
            Console.WriteLine(DataFormats.Rtf);
            Console.WriteLine(DataFormats.Serializable);
            Console.WriteLine(DataFormats.StringFormat);
            Console.WriteLine(DataFormats.SymbolicLink);
            Console.WriteLine(DataFormats.Text);
            Console.WriteLine(DataFormats.Tiff);
            Console.WriteLine(DataFormats.UnicodeText);
            Console.WriteLine(DataFormats.WaveAudio);
        }
    }
}
