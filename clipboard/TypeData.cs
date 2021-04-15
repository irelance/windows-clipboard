using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace clipboard
{
    public class TypeData
    {
        public DataFormats.Format Type;
        public string Path;
        public byte[] Data;
        public TypeData(string s)
        {
            var list = s.Split(',');
            try
            {
                Type = DataFormats.GetFormat(list[0]);
                var tmp = "";
                for (var i = 1; i < list.Length; i++)
                {
                    tmp += list[i];
                }
                if (tmp.Length > 0)
                {
                    var info = new FileInfo(tmp);
                    Path = info?.FullName ?? "";
                }
            }
            catch { }
        }

        public bool IsValidate()
        {
            if (null == Type) return false;
            if (null == Path) return false;
            if (Path.Length <= 0 || !File.Exists(Path)) return false;
            return true;
        }

        public void SetToData(DataObject data)
        {
            if (IsValidate())
            {
                switch (Type.Name)
                {
                    case "Bitmap":
                    case "DeviceIndependentBitmap":
                        data.SetData(Type.Name, (Bitmap)Image.FromFile(Path));
                        break;
                    case "HTML Format":
                    case "Text":
                    case "UnicodeText":
                    case "Rich Text Format":
                        Data = Helper.FileContent(Path);
                        Console.WriteLine($"The value for Data is: {System.Text.Encoding.UTF8.GetString(Data)}");
                        data.SetData(Type.Name, System.Text.Encoding.UTF8.GetString(Data));
                        break;
                    /*case "Csv":// todo
                        Data = Helper.FileContent(Path);
                        var stream = new MemoryStream(Data);
                        data.SetData(Type.Name, stream);
                        break;*/
                    default:
                        Data = Helper.FileContent(Path);
                        data.SetData(Type.Name, Data);
                        break;
                }
            }
        }
    }
}
