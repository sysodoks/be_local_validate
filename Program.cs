using System;
using System.Windows.Forms;

namespace aspx_app  // 必须与Form1的命名空间一致
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());  // 启动主窗体
        }
    }
}