using System.IO.IsolatedStorage;

namespace EyeMessage
{
    /// <summary>
    /// 共享类，保存全局变量
    /// </summary>
    public class MyShare
    {
        public static string UserName
        {
            get
            {
                return IsolatedStorageSettings.ApplicationSettings.Contains("UserName") ?
                    IsolatedStorageSettings.ApplicationSettings["UserName"] as string : "";
            }
            set
            {
                IsolatedStorageSettings.ApplicationSettings["UserName"] = value;
                IsolatedStorageSettings.ApplicationSettings.Save();
            }
        }

        //存保存的列表
        public static string[] MySaveMessage
        {
            get
            {
                return IsolatedStorageSettings.ApplicationSettings.Contains("MySaveMessage") ?
                    IsolatedStorageSettings.ApplicationSettings["MySaveMessage"] as string[] : new string[] {
                        "NOKIA", "Lumia", "不跟随", "不平凡", "张歆艺", "小妞儿", 
                        "张悬", "周杰伦", "I ♥ U", "我想你", 
                    };
            }
            set
            {
                IsolatedStorageSettings.ApplicationSettings["MySaveMessage"] = value;
                IsolatedStorageSettings.ApplicationSettings.Save();
            }
        }
    }
}