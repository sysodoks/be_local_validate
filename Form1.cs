using System;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace aspx_app  // 确保命名空间与项目名称一致
{
    public partial class Form1 : Form
    {
        // 控件声明
        private TextBox txtInput;
        private Button btnEncrypt;
        private TextBox txtCiphertext;
        private Button btnDecrypt;
        private TextBox txtPlaintext;
        private RichTextBox rtbEncryptFunc;
        private RichTextBox rtbDecryptFunc;

        public Form1()
        {
            // 手动初始化（不依赖设计器生成的InitializeComponent）
            this.SuspendLayout();
            InitControls();  // 自定义控件初始化
            this.ResumeLayout(false);
        }

        // 初始化所有控件（替代设计器生成的代码）
        private void InitControls()
        {
            // 窗体设置
            this.Text = "加解密协议验证工具";
            this.Size = new System.Drawing.Size(1000, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            // 左边加密函数展示框
            rtbEncryptFunc = new RichTextBox
            {
                Location = new System.Drawing.Point(10, 10),
                Size = new System.Drawing.Size(470, 200),
                ReadOnly = false,  // 允许用户编辑加密协议
                Font = new System.Drawing.Font("Consolas", 9F),
                Text = @"// 加密协议（可修改）
private byte[] Encrypt(byte[] data)
{
    string upload = ""-----------------------------7e6103b1815de Content-Disposition:form-data;name=\""uploadFile\"";filename=\""test.png\"" Content-Type:application/octet-stream  DaYer0 -----------------------------7e6103b1815de--"";
    string base64Data = Convert.ToBase64String(data).Replace(""+"", ""<"").Replace(""/"", ""> "");
    upload = upload.Replace(""DaYer0"", base64Data);
    return Encoding.UTF8.GetBytes(upload);
}"
            };
            this.Controls.Add(rtbEncryptFunc);

            // 右边解密函数展示框
            rtbDecryptFunc = new RichTextBox
            {
                Location = new System.Drawing.Point(500, 10),
                Size = new System.Drawing.Size(470, 200),
                ReadOnly = false,  // 允许用户编辑解密协议
                Font = new System.Drawing.Font("Consolas", 9F),
                Text = @"// 解密协议（可修改）
private byte[] Decrypt(byte[] data)
{
    using (MemoryStream ms = new MemoryStream())
    {
        ms.Write(data, 150, data.Length - 195);
        byte[] validBytes = ms.ToArray();
        string base64Str = Encoding.UTF8.GetString(validBytes).Replace(""<"", ""+"").Replace(""> "",""/"");
        return Convert.FromBase64String(base64Str);
    }
}"
            };
            this.Controls.Add(rtbDecryptFunc);

            // 输入框
            txtInput = new TextBox
            {
                Location = new System.Drawing.Point(10, 240),
                Size = new System.Drawing.Size(250, 80),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                PlaceholderText = "请输入要加密的内容"
            };
            this.Controls.Add(txtInput);

            // 加密按钮
            btnEncrypt = new Button
            {
                Location = new System.Drawing.Point(270, 240),
                Size = new System.Drawing.Size(80, 40),
                Text = "加密"
            };
            btnEncrypt.Click += BtnEncrypt_Click;
            this.Controls.Add(btnEncrypt);

            // 密文输出框
            txtCiphertext = new TextBox
            {
                Location = new System.Drawing.Point(360, 240),
                Size = new System.Drawing.Size(250, 80),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                ReadOnly = true
            };
            this.Controls.Add(txtCiphertext);

            // 解密按钮
            btnDecrypt = new Button
            {
                Location = new System.Drawing.Point(620, 240),
                Size = new System.Drawing.Size(80, 40),
                Text = "解密"
            };
            btnDecrypt.Click += BtnDecrypt_Click;
            this.Controls.Add(btnDecrypt);

            // 明文输出框
            txtPlaintext = new TextBox
            {
                Location = new System.Drawing.Point(710, 240),
                Size = new System.Drawing.Size(250, 80),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                ReadOnly = true
            };
            this.Controls.Add(txtPlaintext);
        }

        // 加密按钮点击事件
        private void BtnEncrypt_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtInput.Text))
            {
                MessageBox.Show("请输入要加密的内容", "提示");
                return;
            }
            try
            {
                byte[] inputData = Encoding.UTF8.GetBytes(txtInput.Text);
                byte[] cipherData = Encrypt(inputData);
                txtCiphertext.Text = Encoding.UTF8.GetString(cipherData);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加密失败: {ex.Message}", "错误");
            }
        }

        // 解密按钮点击事件
        private void BtnDecrypt_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCiphertext.Text))
            {
                MessageBox.Show("请先执行加密操作", "提示");
                return;
            }
            try
            {
                byte[] cipherData = Encoding.UTF8.GetBytes(txtCiphertext.Text);
                byte[] plainData = Decrypt(cipherData);
                txtPlaintext.Text = Encoding.UTF8.GetString(plainData);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"解密失败: {ex.Message}", "错误");
            }
        }

        // 加密协议实现（JSP转换版）
        private byte[] Encrypt(byte[] data)
        {
            string upload = "-----------------------------7e6103b1815de Content-Disposition:form-data;name=\"uploadFile\";filename=\"test.png\" Content-Type:application/octet-stream  DaYer0 -----------------------------7e6103b1815de--";
            string base64Data = Convert.ToBase64String(data).Replace("+", "<").Replace("/", ">");
            upload = upload.Replace("DaYer0", base64Data);
            return Encoding.UTF8.GetBytes(upload);
        }

        // 解密协议实现（JSP转换版）
        private byte[] Decrypt(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(data, 150, data.Length - 195);
                byte[] validBytes = ms.ToArray();
                string base64Str = Encoding.UTF8.GetString(validBytes).Replace("<", "+").Replace(">", "/");
                return Convert.FromBase64String(base64Str);
            }
        }
    }
}